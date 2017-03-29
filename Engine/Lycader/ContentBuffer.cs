//-----------------------------------------------------------------------
// <copyright file="ContentBuffer.cs" company="Mooglegiant" >
//      Copyright (c) Mooglegiant. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Lycader
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    public static class ContentBuffer
    {
        static ContentBuffer()
        {
            audioQueue = new Dictionary<string, string>();
            textureQueue = new Dictionary<string, string>();
        }

        private static Dictionary<string, string> audioQueue;

        private static Dictionary<string, string> textureQueue;

        public static void AddAudio(string directory)
        {
            foreach (FileInfo file in Directory.EnumerateFiles(directory).Select(x => new FileInfo(x)))
            {
                AddAudio(file.Name, file.FullName);
            }
        }

        public static void AddAudio(string key, string file)
        {
            if (!audioQueue.ContainsKey(key))
            {
                audioQueue.Add(key, file);
            }
        }

        public static void AddTexture(string directory)
        {
            foreach (FileInfo file in Directory.EnumerateFiles(directory).Select(x => new FileInfo(x)))
            {
                AddTexture(file.Name, file.FullName);
            }
        }

        public static void AddTexture(string key, string file)
        {
            if (!textureQueue.ContainsKey(key))
            {
                textureQueue.Add(key, file);
            }
        }

        public static void Process(int items)
        {
            for (int i = 0; i < items; i++)
            {
                if (audioQueue.Count > 0)
                {
                    Audio.SoundManager.Load(audioQueue.First().Key, audioQueue.First().Value);
                    audioQueue.Remove(audioQueue.First().Key);
                }

                if (textureQueue.Count > 0)
                {
                    Graphics.TextureManager.Load(textureQueue.First().Key, textureQueue.First().Value);
                    textureQueue.Remove(textureQueue.First().Key);
                }
            }
        }

        public static bool IsQueueEmpty()
        {
            if (audioQueue.Count > 0)
            {
                return false;
            }

            if (textureQueue.Count > 0)
            {
                return false;
            }

            return true;
        }

        public static int Remaining()
        {
            return textureQueue.Count() + audioQueue.Count();
        }
    }
}
