//Taken from NVorbis github project (offical package has wrong OpenTK dependency)

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

    public static class MusicManager
    {
        const float DefaultUpdateRate = 10;
        const int DefaultBufferSize = 44100;

        static readonly object singletonMutex = new object();
        static readonly object iterationMutex = new object();
        static readonly object readMutex = new object();

        static readonly float[] readSampleBuffer;
        static readonly short[] castBuffer;

        static readonly HashSet<OggStream> streams = new HashSet<OggStream>();


        static Thread underlyingThread;
        static volatile bool cancelled;

        static public float UpdateRate { get; private set; }
        static public int BufferSize { get; private set; }

        //  public ILogger Logger { private get; set; }

        /// <summary>
        /// Constructs an OggStreamer that plays ogg files in the background
        /// </summary>
        /// <param name="bufferSize">Buffer size</param>
        /// <param name="updateRate">Number of times per second to update</param>
        /// <param name="internalThread">True to use an internal thread, false to use your own thread, in which case use must call EnsureBuffersFilled periodically</param>
        static MusicManager()
        {
            int bufferSize = DefaultBufferSize;
            float updateRate = DefaultUpdateRate;
            bool internalThread = true;

            lock (singletonMutex)
            {
                if (internalThread)
                {
                    underlyingThread = new Thread(EnsureBuffersFilled) { Priority = ThreadPriority.Lowest };
                    underlyingThread.Start();
                }
                else
                {
                    // no need for this, user is in charge
                    updateRate = 0;
                }
            }

            UpdateRate = updateRate;
            BufferSize = bufferSize;

            readSampleBuffer = new float[bufferSize];
            castBuffer = new short[bufferSize];
        }

        internal static bool AddStream(OggStream stream)
        {
            lock (iterationMutex)
            {
                return streams.Add(stream);
            }
        }

        internal static bool RemoveStream(OggStream stream)
        {
            lock (iterationMutex)
            {
                return streams.Remove(stream);
            }
        }

        public static void Unload()
        {
            lock (iterationMutex)
            {
                while (streams.Count() > 0)
                {
                    streams.ElementAt(0).Dispose();
                }
            }
        }

        public static void Dispose()
        {
            lock (singletonMutex)
            {
                cancelled = true;
                lock (iterationMutex)
                {
                    while (streams.Count() > 0)
                    {
                        streams.ElementAt(0).Dispose();
                    }
                }

                underlyingThread = null;
            }
        }

        public static bool FillBuffer(OggStream stream, int bufferId)
        {
            int readSamples;
            lock (readMutex)
            {
                readSamples = stream.Reader.ReadSamples(readSampleBuffer, 0, BufferSize);
                CastBuffer(readSampleBuffer, castBuffer, readSamples);
            }

            AL.BufferData(bufferId, stream.Reader.Channels == 1 ? ALFormat.Mono16 : ALFormat.Stereo16, castBuffer, readSamples * sizeof(short), stream.Reader.SampleRate);

            return readSamples != BufferSize;
        }

        public static void CastBuffer(float[] inBuffer, short[] outBuffer, int length)
        {
            for (int i = 0; i < length; i++)
            {
                var temp = (int)(32767f * inBuffer[i]);
                if (temp > short.MaxValue) temp = short.MaxValue;
                else if (temp < short.MinValue) temp = short.MinValue;
                outBuffer[i] = (short)temp;
            }
        }

        public static void EnsureBuffersFilled()
        {
            do
            {              
                List<OggStream> threadLocalStreams = new List<OggStream>();
                lock (iterationMutex) threadLocalStreams.AddRange(streams);

                foreach (var stream in threadLocalStreams)
                {
                    lock (stream.prepareMutex)
                    {
                        lock (iterationMutex)
                            if (!streams.Contains(stream))
                                continue;

                        bool finished = false;

                        int queued;
                        AL.GetSource(stream.sourceID, ALGetSourcei.BuffersQueued, out queued);

                        int processed;
                        AL.GetSource(stream.sourceID, ALGetSourcei.BuffersProcessed, out processed);

                        if (processed == 0 && queued == stream.BufferCount) continue;

                        int[] tempBuffers;
                        if (processed > 0)
                            tempBuffers = AL.SourceUnqueueBuffers(stream.sourceID, processed);
                        else
                            tempBuffers = stream.bufferIDs.Skip(queued).ToArray();

                        int bufIdx = 0;
                        for (; bufIdx < tempBuffers.Length; bufIdx++)
                        {
                            finished |= FillBuffer(stream, tempBuffers[bufIdx]);

                            if (finished)
                            {
                                if (stream.IsLooped)
                                {
                                    stream.Reader.DecodedTime = TimeSpan.Zero;
                                    if (bufIdx == 0)
                                    {
                                        // we didn't have any buffers left over, so let's start from the beginning on the next cycle...
                                        continue;
                                    }
                                }
                                else
                                {
                                    streams.Remove(stream);
                                    break;
                                }
                            }
                        }

                        AL.SourceQueueBuffers(stream.sourceID, bufIdx, tempBuffers);

                        if (finished && !stream.IsLooped)
                            continue;
                    }

                    lock (stream.stopMutex)
                    {
                        if (stream.Preparing) continue;

                        lock (iterationMutex)
                            if (!streams.Contains(stream))
                                continue;

                        var state = AL.GetSourceState(stream.sourceID);
                        if (state == ALSourceState.Stopped)
                        {
                           // Logger.Log(LogEvent.BufferUnderrun, stream);
                            AL.SourcePlay(stream.sourceID);
                        }
                    }
                }

                if (UpdateRate > 0)
                {
                    Thread.Sleep((int)(1000 / UpdateRate));
                }
            }
            while (underlyingThread != null && !cancelled);
        }
    }
}