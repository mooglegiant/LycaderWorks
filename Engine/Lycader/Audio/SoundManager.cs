//-----------------------------------------------------------------------
// <copyright file="SoundManager.cs" company="Mooglegiant" >
//      Copyright (c) Mooglegiant. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Lycader.Audio
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
        private static Dictionary<string, SoundClip> collection = new Dictionary<string, SoundClip>();

        /// <summary>
        /// Loads a sound file into memory
        /// </summary>
        /// <param name="key">Name to store the sprite under</param>
        /// <param name="filePath">location of the file to load</param>
        public static void Load(string key, string filePath)
        {
            if (!collection.ContainsKey(key))
            {
                SoundClip sound = new SoundClip(filePath);

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
        public static SoundClip Find(string key)
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
    }
}
