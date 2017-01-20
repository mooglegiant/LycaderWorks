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
        private int bits;
        private int channels;
        private int rate;
        private byte[] soundData;
        private int sourceID;
        private int bufferID;

        /// <summary>
        /// Constructor method for sounds
        /// </summary>
        /// <param name="activebuffer"></param>
        /// <param name="filename"></param>
        public Sound(string filename)
        {
            this.bufferID = AL.GenBuffer();
            this.sourceID = AL.GenSource();
            int chunkSize;
            soundData = SoundManager.LoadWave(File.Open(filename, FileMode.Open), out channels, out bits, out rate, out chunkSize);
            AL.BufferData(bufferID, SoundManager.GetSoundFormat(channels, bits), soundData, chunkSize, rate);

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
            return state == ALSourceState.Stopped;
        }

        public void Play()
        {
            Play(false);
        }

        public void Play(bool loop)
        {
            if (SoundManager.Enabled && !IsPlaying())
            {
                AL.Source(sourceID, ALSourceb.Looping, loop);
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
    }
}

