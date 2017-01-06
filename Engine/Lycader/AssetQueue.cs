using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lycader.Audio;
using Lycader.Graphics;

namespace Lycader
{
    static public class AssetQueue
    {
        static AssetQueue()
        {
            audioQueue = new Dictionary<string, string>();
            textureQueue = new Dictionary<string, string>();
        }

        static private Dictionary<string, string> audioQueue { get; set; }

        static private Dictionary<string, string> textureQueue { get; set; }

        static public void Audio(string key, string file)
        {
            if (!audioQueue.ContainsKey(key))
            {
                audioQueue.Add(key, file);
            }
        }

        static public void Texture(string key, string file)
        {
            if (!textureQueue.ContainsKey(key))
            {
                textureQueue.Add(key, file);
            }
        }

        static public void Process(int items)
        {
            for (int i = 0; i < items; i++)
            {
                if (audioQueue.Count > 0)
                {
                    AudioContent.Load(audioQueue.First().Key, audioQueue.First().Value);
                    audioQueue.Remove(audioQueue.First().Key);
                }

                if (textureQueue.Count > 0)
                {
                    TextureContent.Load(textureQueue.First().Key, textureQueue.First().Value);
                    textureQueue.Remove(textureQueue.First().Key);
                }
            }
        }

        static public bool IsQueueEmpty()
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

        static public int Remaining()
        {
            return textureQueue.Count() + audioQueue.Count();
        }
    }
}
