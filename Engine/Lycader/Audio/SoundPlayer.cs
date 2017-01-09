//-----------------------------------------------------------------------
// <copyright file="SoundPlayer.cs" company="Mooglegiant" >
//      Copyright (c) Mooglegiant. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Lycader.Audio
{
    using OpenTK.Audio.OpenAL;

    /// <summary>
    /// Handles playing the sounds and does our OpenAL source managing
    /// </summary>
    public static class SoundPlayer
    {
        /// <summary>
        /// Source array for playing sounds
        /// </summary>
        private static int[] channels;

        /// <summary>
        /// Last source used for playing a sound
        /// </summary>
        private static int nextChannel;

        /// <summary>
        /// Number of channels avaiable for playing sounds
        /// </summary>
        private static int sourceCount = 256;

        /// <summary>
        /// Initializes static members of the SoundPlayer class
        /// /// </summary>
        static SoundPlayer()
        {
            channels = AL.GenSources(sourceCount);
            nextChannel = 1;
        }

        /// <summary>
        /// Gets or sets the key of the current song playing (passed to PlaySong function)
        /// </summary>
        public static string CurrentSong { get; set; }

        /// <summary>
        /// Plays a sound from the SoundManager
        /// </summary>
        /// <param name="key">the sound's key</param>
        public static void PlaySound(string key)
        {
            if (LycaderEngine.SoundEnabled)
            {
                AL.Source(channels[nextChannel], ALSourcei.Buffer, AudioContent.Get(key).Buffer);
                AL.SourcePlay(channels[nextChannel]);

                nextChannel++;
                if (nextChannel > channels.Length - 1)
                {
                    nextChannel = 1;
                }
            }
        }

        /// <summary>
        /// Plays a song from the SoundManager
        /// </summary>
        /// <param name="key">the sound's key</param>
        /// <param name="loop">a value indicating whether to loop the song or not</param>
        public static void PlaySong(string key, bool loop)
        {
            if (LycaderEngine.SoundEnabled)
            {
                AL.Source(channels[0], ALSourcei.Buffer, AudioContent.Get(key).Buffer);
                AL.Source(channels[0], ALSourceb.Looping, loop);
                AL.SourcePlay(channels[0]);
                CurrentSong = key;
            }
        }

        /// <summary>
        /// Queue a song to play after the current one is finished
        /// </summary>
        /// <param name="key">the sound's key</param>
        public static void QueueSong(string key)
        {
            if (LycaderEngine.SoundEnabled)
            {
                AL.SourceQueueBuffer(channels[0], AudioContent.Get(key).Buffer);
            }
        }

        /// <summary>
        /// Pauses the current song
        /// </summary>
        public static void Pause()
        {
            if (LycaderEngine.SoundEnabled && CurrentSong != null)
            {
                AL.Source(channels[0], ALSourcei.Buffer, AudioContent.Get(CurrentSong).Buffer);
                AL.SourcePause(channels[0]);
            }
        }

        /// <summary>
        /// Stops the current song
        /// </summary>
        public static void Stop()
        {
            if (LycaderEngine.SoundEnabled && CurrentSong != null)
            {
                AL.Source(channels[0], ALSourcei.Buffer, AudioContent.Get(CurrentSong).Buffer);
                AL.SourceStop(channels[0]);
                AL.SourceRewind(channels[0]);
            }
        }

        /// <summary>
        /// Unloads all the OpenAL sources
        /// </summary>
        public static void Unload()
        {
            AL.DeleteSources(channels);
        }
    }
}
