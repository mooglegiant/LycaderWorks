//-----------------------------------------------------------------------
// <copyright file="SoundManager.cs" company="Mooglegiant" >
//      Copyright (c) Mooglegiant. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Lycader.Audio
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    using OpenTK.Audio;
    using OpenTK.Audio.OpenAL;

    using Lycader.Audio;

    /// <summary>
    /// Loads and manages all the sounds avaiable for playing
    /// </summary>
    public static class AudioContent
    {
        /// <summary>
        /// Private collection of audio buffers
        /// </summary>
        private static Dictionary<string, SoundBuffer> collection = new Dictionary<string, SoundBuffer>();

        /// <summary>
        /// The application's AudioContext
        /// </summary>
        private static AudioContext audioContext;

        /// <summary>
        /// Initializes static members of the SoundManager class
        /// </summary>
        static AudioContent()
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
            SoundBuffer sound = new SoundBuffer();

            int channels, bitsPerSample, sampleRate;
            byte[] soundDate = LoadWave(File.Open(filePath, FileMode.Open), out channels, out bitsPerSample, out sampleRate);
            AL.BufferData(sound.Buffer, GetSoundFormat(channels, bitsPerSample), soundDate, soundDate.Length, sampleRate);

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
                SoundBuffer sound = collection[key];
                collection.Remove(key);
                sound.Dispose();
            }
        }

        /// <summary>
        /// Removes all sound from memory
        /// </summary>
        public static void Unload()
        {
            List<string> keys = new List<string>();
            foreach (string key in collection.Keys)
            {
                keys.Add(key);
            }

            foreach (string key in keys)
            {
                SoundBuffer sound = collection[key];
                collection.Remove(key);
                sound.Dispose();
            }
        }

        /// <summary>
        /// Returns a sound from the collection
        /// </summary>
        /// <param name="key">Lookup name for the sound</param>
        /// <returns>The requested sound</returns>
        internal static SoundBuffer Get(string key)
        {
            if (!collection.ContainsKey(key))
            {
                throw new Exception("Sound: " + key + " not found");
            }

            return collection[key];
        }

        #region Load File

        /// <summary>
        /// Loads a Wav/Riff audio file
        /// </summary>
        /// <param name="stream">the audio file stream</param>
        /// <param name="channels">returns the stream's channel count</param>
        /// <param name="bitsPerSample">returns the stream's bits per sample</param>
        /// <param name="sampleRate">returns the stream's sample rate</param>
        /// <returns>the sound data</returns>
        private static byte[] LoadWave(Stream stream, out int channels, out int bitsPerSample, out int sampleRate)
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

                int dataChunkSize = reader.ReadInt32();
                return reader.ReadBytes((int)reader.BaseStream.Length);
            }
        }

        /// <summary>
        /// Gets the sound format
        /// </summary>
        /// <param name="channels">number of channels the file has</param>
        /// <param name="bitsPerSample">flag for bits per sample</param>
        /// <returns>the ALFormat base on the values passed in</returns>
        private static ALFormat GetSoundFormat(int channels, int bitsPerSample)
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
