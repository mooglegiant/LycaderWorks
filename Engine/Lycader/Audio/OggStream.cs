//Modified from NVorbis project

namespace Lycader.Audio
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Threading;
    using NVorbis;
    using OpenTK.Audio.OpenAL;

    public class OggStream : IDisposable
    {
        private static readonly XRamExtension XRam = new XRamExtension();
        private static readonly EffectsExtension Efx = new EffectsExtension();

        const int DefaultBufferCount = 3;

        internal readonly object stopMutex = new object();
        internal readonly object prepareMutex = new object();

        internal int sourceID;
        internal int[] bufferIDs;
        internal int filterID;

        readonly Stream underlyingStream;

        internal VorbisReader Reader { get; private set; }

        public bool Ready { get; private set; }

        internal bool Preparing { get; private set; }

        public int BufferCount { get; private set; }

        private float lowPassHfGain;

        public float LowPassHFGain
        {
            get { return lowPassHfGain; }
            set
            {
                if (Efx.IsInitialized)
                {
                    Efx.Filter(filterID, EfxFilterf.LowpassGainHF, lowPassHfGain = value);
                    Efx.BindFilterToSource(sourceID, filterID);
                }
            }
        }

        public bool IsLooped { get; set; }

        public OggStream(string filename, int bufferCount = DefaultBufferCount) : this(File.OpenRead(filename), bufferCount) { }

        public OggStream(Stream stream, int bufferCount = DefaultBufferCount)
        {
            BufferCount = bufferCount;

            bufferIDs = AL.GenBuffers(bufferCount);
            sourceID = AL.GenSource();

            if (XRam.IsInitialized)
            {
                XRam.SetBufferMode(BufferCount, ref bufferIDs[0], XRamExtension.XRamStorage.Hardware);
            }

            if (Efx.IsInitialized)
            {
                filterID = Efx.GenFilter();
                Efx.Filter(filterID, EfxFilteri.FilterType, (int)EfxFilterType.Lowpass);
                Efx.Filter(filterID, EfxFilterf.LowpassGain, 1);

                LowPassHFGain = 1;
            }

            underlyingStream = stream;

          //  Logger = NullLogger.Default;
        }

        public void Prepare()
        {
            if (Preparing) return;

            var state = AL.GetSourceState(sourceID);

            lock (stopMutex)
            {
                switch (state)
                {
                    case ALSourceState.Playing:
                    case ALSourceState.Paused:
                        return;

                    case ALSourceState.Stopped:
                        lock (prepareMutex)
                        {
                            Reader.DecodedTime = TimeSpan.Zero;
                            Ready = false;
                            Empty();
                        }
                        break;
                }

                if (!Ready)
                {
                    lock (prepareMutex)
                    {
                        Preparing = true;
                        Open(precache: true);
                    }
                }
            }
        }

        public bool IsPlaying()
        {
            ALSourceState state = AL.GetSourceState(this.sourceID);
            return state == ALSourceState.Playing;
        }

        public bool IsPaused()
        {
            ALSourceState state = AL.GetSourceState(this.sourceID);
            return state == ALSourceState.Paused;
        }

        public bool IsStopped(bool checkUnplayed = false)
        {
            ALSourceState state = AL.GetSourceState(this.sourceID);

            if (checkUnplayed)
            {
                return state == ALSourceState.Stopped || state == ALSourceState.Initial;
            }
            else
            {
                return state == ALSourceState.Stopped;
            }
        }

        public void Play(bool repeat = false)
        {
            if (Audio.Settings.Enabled)
            {
                var state = AL.GetSourceState(sourceID);
                IsLooped = repeat;

                switch (state)
                {
                    case ALSourceState.Playing: return;
                    case ALSourceState.Paused:
                        Resume();
                        return;
                }

                Prepare();

                AL.Source(this.sourceID, ALSourcef.Gain, Audio.Settings.MusicVolume);     
                AL.SourcePlay(this.sourceID);

                Preparing = false;

                Music.AddStream(this);
            }
        }

        public void Pause()
        {
            if (AL.GetSourceState(sourceID) != ALSourceState.Playing)
            {
                return;
            }

            Music.RemoveStream(this);
            AL.SourcePause(sourceID);
        }

        public void Resume()
        {
            if (AL.GetSourceState(sourceID) != ALSourceState.Paused)
            {
                return;
            }

            AL.Source(this.sourceID, ALSourcef.Gain, Audio.Settings.MusicVolume);
            AL.SourcePlay(this.sourceID);

            Music.AddStream(this);   
        }

        public void Stop()
        {
            var state = AL.GetSourceState(sourceID);
            if (state == ALSourceState.Playing || state == ALSourceState.Paused)
            {
                StopPlayback();
            }

            lock (stopMutex)
            {
                Music.RemoveStream(this);
            }
        }

        public void Dispose()
        {
            var state = AL.GetSourceState(sourceID);
            if (state == ALSourceState.Playing || state == ALSourceState.Paused)
            {
                StopPlayback();
            }

            lock (prepareMutex)
            {
                Music.RemoveStream(this);

                if (state != ALSourceState.Initial)
                    Empty();

                Close();

                underlyingStream.Dispose();
            }

            AL.DeleteSource(sourceID);
            AL.DeleteBuffers(bufferIDs);

            if (Efx.IsInitialized)
            {
                Efx.DeleteFilter(filterID);
            }
        }

        public void StopPlayback()
        {
            AL.SourceStop(sourceID);
        }

        public void Empty()
        {
            int queued;
            AL.GetSource(sourceID, ALGetSourcei.BuffersQueued, out queued);
            if (queued > 0)
            {
                try
                {
                    AL.SourceUnqueueBuffers(sourceID, queued);
                }
                catch (InvalidOperationException)
                {
                    // This is a bug in the OpenAL implementation
                    // Salvage what we can
                    int processed;
                    AL.GetSource(sourceID, ALGetSourcei.BuffersProcessed, out processed);
                    var salvaged = new int[processed];
                    if (processed > 0)
                    {
                        AL.SourceUnqueueBuffers(sourceID, processed, salvaged);
                    }

                    // Try turning it off again?
                    AL.SourceStop(sourceID);

                    Empty();
                }
            }

            //Logger.Log(LogEvent.Empty, this);
        }

        internal void Open(bool precache = false)
        {
            underlyingStream.Seek(0, SeekOrigin.Begin);
            Reader = new VorbisReader(underlyingStream, false);

            if (precache)
            {
                // Fill first buffer synchronously
                Music.FillBuffer(this, bufferIDs[0]);
                AL.SourceQueueBuffer(sourceID, bufferIDs[0]);

                // Schedule the others asynchronously
                Music.AddStream(this);
            }

            Ready = true;
        }

        internal void Close()
        {
            if (Reader != null)
            {
                Reader.Dispose();
                Reader = null;
            }
            Ready = false;
        }
    }
}