//-----------------------------------------------------------------------
// <copyright file="AudioContent.cs" company="Mooglegiant" >
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
    public static class SoundContent
    {
        /// <summary>
        /// Private collection of audio buffers
        /// </summary>
        private static Dictionary<string, Sound> collection = new Dictionary<string, Sound>();

        /// <summary>
        /// The application's AudioContext
        /// </summary>
        private static AudioContext audioContext;

        /// <summary>
        /// Initializes static members of the SoundManager class
        /// </summary>
        static SoundContent()
        {
            LycaderEngine.AllowSoundPlayed = false;
            LycaderEngine.HasSoundDevice = false;
            LycaderEngine.SoundEnabled = false;

            // Make sure we have a sound device available.  If not, do not allow playing of sounds :)
            if (AudioContext.AvailableDevices.Count > 0)
            {
                if (!string.IsNullOrEmpty(AudioContext.AvailableDevices[0]))
                {
                    audioContext = new AudioContext();
                    LycaderEngine.AllowSoundPlayed = true;
                    LycaderEngine.HasSoundDevice = true;
                    LycaderEngine.SoundEnabled = true;
                }
            }
        }

        /// <summary>
        /// Loads a sound file into memory
        /// </summary>
        /// <param name="key">Name to store the sprite under</param>
        /// <param name="filePath">location of the file to load</param>
        public static void Load(string key, string filePath)
        {
            Sound sound = new Sound(filePath);

            if (sound != null)
            {
                Unload(key);
                collection.Add(key, sound);
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
                AL.DeleteBuffer(collection[key].Handle);
                collection.Remove(key);
            }
        }

        public static void Unload()
        {
            collection.Values.ToList().ForEach(i => AL.DeleteBuffer(i.Handle));
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

        #region Load File

        private static int bufferSize = 2;
        private static int[] buffers = new int[bufferSize];

        /// <summary>
        /// Loads a Wav/Riff audio file
        /// </summary>
        /// <param name="stream">the audio file stream</param>
        /// <param name="channels">returns the stream's channel count</param>
        /// <param name="bitsPerSample">returns the stream's bits per sample</param>
        /// <param name="sampleRate">returns the stream's sample rate</param>
        /// <param name="dataChunkSize">returns the stream's sample data chunk size</param>
        /// <returns>the sound data</returns>
        public static byte[] LoadWave(Stream stream, out int channels, out int bitsPerSample, out int sampleRate, out int dataChunkSize)
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
        public static ALFormat GetSoundFormat(int channels, int bitsPerSample)
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
