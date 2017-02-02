﻿//-----------------------------------------------------------------------
// <copyright file="SoundManager.cs" company="Mooglegiant" >
//      Copyright (c) Mooglegiant. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Lycader
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    using OpenTK.Audio;
    using OpenTK.Audio.OpenAL;

    using System.Linq;

    /// <summary>
    /// Loads and manages all the sounds avaiable for playing
    /// </summary>
    public static class SoundManager
    {
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

        /// <summary>
        /// Private collection of audio buffers
        /// </summary>
        private static Dictionary<string, Sound> collection = new Dictionary<string, Sound>();

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
    }
}