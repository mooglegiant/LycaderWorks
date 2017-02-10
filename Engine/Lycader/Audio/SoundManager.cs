//-----------------------------------------------------------------------
// <copyright file="SoundManager.cs" company="Mooglegiant" >
//      Copyright (c) Mooglegiant. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Lycader
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Loads and manages all the sounds available for playing
    /// </summary>
    public static class SoundManager
    {
        /// <summary>
        /// Private collection of audio buffers
        /// </summary>
        private static Dictionary<string, Sound> collection = new Dictionary<string, Sound>();
        private static Dictionary<string, int> buffer = new Dictionary<string, int>();

        #region Sound Settings
        /// <summary>
        /// Gets or sets a value indicating whether sound is enabled or not
        /// </summary>
        public static bool Enabled
        {
            get
            {
                return AllowSoundPlayed;
            }

            set
            {
                // If no sound driver is loaded, don't allow sound to be enabled
                if (!HasSoundDevice)
                {
                    AllowSoundPlayed = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether a sound drive is available or not
        /// </summary>
        internal static bool HasSoundDevice { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the sound manager will allow sounds to be played
        /// </summary>
        internal static bool AllowSoundPlayed { get; set; }
        #endregion

        public static void Queue(string key, int loops)
        {
            if (!buffer.ContainsKey(key))
            {
                buffer.Add(key, loops);
            }
        }

        /// <summary>
        /// Loads a sound file into memory
        /// </summary>
        /// <param name="key">Name to store the sprite under</param>
        /// <param name="filePath">location of the file to load</param>
        public static void Load(string key, string filePath)
        {
            if (!collection.ContainsKey(key))
            {
                Sound sound = new Sound(filePath);

                if (sound != null)
                {
                    Unload(key);
                    collection.Add(key, sound);
                }
            }
        }

        /// <summary>
        /// Removes the sound from memory
        /// </summary>
        /// <param name="key">Lookup name for the sprite</param>
        public static void Unload(string key)
        {
            if (collection.ContainsKey(key))
            {
                collection[key].Delete();
                collection.Remove(key);
            }
        }

        public static void Unload()
        {
            collection.Values.ToList().ForEach(i => i.Delete());
            collection.Clear();
        }

        /// <summary>
        /// Returns a sound from the collection
        /// </summary>
        /// <param name="key">Lookup name for the sound</param>
        /// <returns>The requested sound</returns>
        public static Sound Find(string key)
        {
            if (!collection.ContainsKey(key))
            {
                throw new Exception("Sound: " + key + " not found");
            }

            return collection[key];
        }

        public static void StopAll()
        {
            collection.Values.Where(i => i.IsPlaying()).ToList().ForEach(i => i.Stop());
        }

        internal static void ProcessQueue()
        {
            List<string> processed = new List<string>();

            foreach (string key in buffer.Keys)
            {
                if (collection[key].IsStopped())
                {
                    collection[key].Play();
                    processed.Add(key);
                }
            }

            foreach (string key in processed)
            {
                buffer[key] = buffer[key] - 1;

                if (buffer[key] == 0)
                {
                    buffer.Remove(key);
                }
            }
        }
    }
}
