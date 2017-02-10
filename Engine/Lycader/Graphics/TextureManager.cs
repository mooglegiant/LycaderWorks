//-----------------------------------------------------------------------
// <copyright file="TextureManager.cs" company="Mooglegiant" >
//      Copyright (c) Mooglegiant. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Lycader
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Loads and manages all the textures available for rendering
    /// </summary>
    public static class TextureManager
    {
        /// <summary>
        /// Private collection of textures
        /// </summary>
        private static Dictionary<string, Texture> collection = new Dictionary<string, Texture>();

        /// <summary>
        /// Returns a texture from the collection
        /// </summary>
        /// <param name="key">Lookup name for the texture</param>
        /// <returns>The requested sprite</returns>
        public static Texture Find(string key)
        {
            if (!collection.ContainsKey(key))
            {
                throw new Exception("Image: " + key + " not found");
            }

            return collection[key];
        }

        /// <summary>
        /// Create an OpenGL texture (translucent or opaque) by loading a bitmap from a file
        /// </summary>
        /// <param name="key">Name to store the sprite under</param>
        /// <param name="filePath">location of the file to load</param>
        public static Texture Load(string key, string filePath)
        {
            if (!collection.ContainsKey(key))
            {
                Texture texture = new Texture(filePath);
                collection.Add(key, texture);
            }

            return collection[key];
        }

        /// <summary>
        /// Removes the texture from memory
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

        /// <summary>
        /// Removes all texture from memory
        /// </summary>
        public static void Unload()
        {           
            collection.Values.ToList().ForEach(i => i.Delete());
            collection.Clear();
        }  
    }
}
