//-----------------------------------------------------------------------
// <copyright file="Sound.cs" company="Mooglegiant" >
//      Copyright (c) Mooglegiant. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Lycader
{
    using System;

    using OpenTK.Audio.OpenAL;
    using OpenTK;
    using System.IO;

    /// <summary>
    /// Holds basic information about the a sound block
    /// </summary>
    public class Sound
    {
        private int sourceID;
        private int bufferID;

        /// <summary>
        /// Constructor method for sounds
        /// </summary>
        /// <param name="filename"></param>
        public Sound(string filename)
        {
            int bits;
            int channels;
            int rate;
            byte[] soundData;

            this.bufferID = AL.GenBuffer();
            this.sourceID = AL.GenSource();
            int chunkSize;
            soundData = LoadWave(File.Open(filename, FileMode.Open), out channels, out bits, out rate, out chunkSize);
            AL.BufferData(bufferID, GetSoundFormat(channels, bits), soundData, chunkSize, rate);

            ALError error = AL.GetError();
            if (error != ALError.NoError)
            {
                Console.WriteLine("error loading buffer: " + error);
            }

            AL.Source(sourceID, ALSourcei.Buffer, bufferID);
            var sourcePosition = new Vector3(0f, 0f, 0f);
            AL.Source(sourceID, ALSource3f.Position, ref sourcePosition);
            AL.Source(sourceID, ALSourcef.Gain, 0.85f);
            var listenerPosition = new Vector3(0, 0, 0);
            AL.Listener(ALListener3f.Position, ref listenerPosition);
        }

        public bool IsPlaying()
        {
            ALSourceState state = AL.GetSourceState(sourceID);
            return state == ALSourceState.Playing;
        }

        public bool IsPaused()
        {
            ALSourceState state = AL.GetSourceState(sourceID);
            return state == ALSourceState.Paused;
        }

        public bool IsStopped()
        {
            ALSourceState state = AL.GetSourceState(sourceID);
            return state == ALSourceState.Stopped || state == ALSourceState.Initial;
        }

        public void Play()
        {
            Play(false);
        }

        public void Play(bool repeat)
        {
            if (SoundManager.Enabled && !IsPlaying())
            {
                AL.Source(sourceID, ALSourceb.Looping, repeat);
                AL.SourcePlay(sourceID);
            }
        }

        public void Pause()
        {
            AL.SourcePause(sourceID);
        }

        public void Stop()
        {
            AL.SourceStop(sourceID);
        }

        public void Delete()
        {
            AL.DeleteBuffer(this.bufferID);
            AL.DeleteSource(this.sourceID);
        }

        #region Load File
        /// <summary>
        /// Loads a Wav/Riff audio file
        /// </summary>
        /// <param name="stream">the audio file stream</param>
        /// <param name="channels">returns the stream's channel count</param>
        /// <param name="bitsPerSample">returns the stream's bits per sample</param>
        /// <param name="sampleRate">returns the stream's sample rate</param>
        /// <param name="dataChunkSize">returns the stream's sample data chunk size</param>
        /// <returns>the sound data</returns>
        static private byte[] LoadWave(Stream stream, out int channels, out int bitsPerSample, out int sampleRate, out int dataChunkSize)
        {
            if (stream == null)
            {
                throw new ArgumentNullException("stream not loaded");
            }

            using (BinaryReader reader = new BinaryReader(stream))
            {
                // RIFF header
                string signature = new string(reader.ReadChars(4));
                if (signature != "RIFF")
                {
                    throw new NotSupportedException("Specified stream is not a wave file.");
                }

                int riffChunkSize = reader.ReadInt32();

                string format = new string(reader.ReadChars(4));
                if (format != "WAVE")
                {
                    throw new NotSupportedException("Specified stream is not a wave file.");
                }

                // WAVE header
                string formatSignature = new string(reader.ReadChars(4));
                if (formatSignature != "fmt ")
                {
                    throw new NotSupportedException("Specified wave file is not supported.");
                }

                int chunkSize = reader.ReadInt32();
                int audioFormat = reader.ReadInt16();
                channels = reader.ReadInt16();
                sampleRate = reader.ReadInt32();
                int byteRate = reader.ReadInt32();
                int blockAlign = reader.ReadInt16();
                bitsPerSample = reader.ReadInt16();

                string dataSignature = new string(reader.ReadChars(4));
                if (dataSignature != "data")
                {
                    throw new NotSupportedException("Specified wave file is not supported.");
                }

                dataChunkSize = reader.ReadInt32();
                return reader.ReadBytes((int)reader.BaseStream.Length);
            }
        }

        /// <summary>
        /// Gets the sound format
        /// </summary>
        /// <param name="channels">number of channels the file has</param>
        /// <param name="bitsPerSample">flag for bits per sample</param>
        /// <returns>the ALFormat base on the values passed in</returns>
        static private ALFormat GetSoundFormat(int channels, int bitsPerSample)
        {
            switch (channels)
            {
                case 1: return bitsPerSample == 8 ? ALFormat.Mono8 : ALFormat.Mono16;
                case 2: return bitsPerSample == 8 ? ALFormat.Stereo8 : ALFormat.Stereo16;
                default: throw new NotSupportedException("The specified sound format is not supported.");
            }
        }
        #endregion
    }
}

