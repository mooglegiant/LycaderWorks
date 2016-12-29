//-----------------------------------------------------------------------
// <copyright file="TextureManager.cs" company="Mooglegiant" >
//      Copyright (c) Mooglegiant. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Lycader.Graphics
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using System.IO;

    using OpenTK;
    using OpenTK.Graphics.OpenGL;
    using Img = System.Drawing.Imaging;

    using Lycader.Graphics;
    using Collision;

    /// <summary>
    /// Loads and manages all the textures avaiable for rendering
    /// </summary>
    public static class TextureContent
    {
        /// <summary>
        /// Private collection of textures
        /// </summary>
        private static Dictionary<string, Texture2D> collection = new Dictionary<string, Texture2D>();


        /// <summary>
        /// Returns a texture from the collection
        /// </summary>
        /// <param name="key">Lookup name for the texture</param>
        /// <returns>The requested sprite</returns>
        public static Texture2D Get(string key)
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
        public static Texture2D Load(string key, string filePath)
        {
            if (!collection.ContainsKey(key))
            {
                Texture2D texture = LoadTexture(filePath);
                collection.Add(key, texture);
            }

            return collection[key];
        }

        public static Texture2D Load(string key, Stream stream)
        {
            if (!collection.ContainsKey(key))
            {
                Texture2D texture = LoadTexture(stream);
                collection.Add(key, texture);
            }

            return collection[key];
        }

        /// <summary>
        /// Create an OpenGL texture (translucent or opaque) by loading a bitmap from a file
        /// </summary>
        /// <param name="key">Name to store the sprite under</param>
        /// <param name="filePath">location of the file to load</param>
        /// <param name="collsionBox">defined a collision box, otherwise the whole texture will be used for collision</param>
        public static void Load(string key, string filePath, Box2 collsionBox)
        {
            if (!collection.ContainsKey(key))
            {
                Texture2D texture = LoadTexture(filePath);   
                collection.Add(key, texture);
            }
        }

        /// <summary>
        /// Removes the texture from memory
        /// </summary>
        /// <param name="key">Lookup name for the sprite</param>
        public static void Unload(string key)
        {
            if (collection.ContainsKey(key))
            {
                GL.DeleteTexture(collection[key].Handle);
                collection.Remove(key);
            }
        }

        /// <summary>
        /// Removes all texture from memory
        /// </summary>
        public static void Unload()
        {           
            collection.Values.ToList().ForEach(i => GL.DeleteTexture(i.Handle));
            collection.Clear();
        }

        /// <summary>
        /// Loads the texture into memory
        /// </summary>
        /// <param name="filePath">the file to load</param>
        /// <returns>A texture class</returns>
        private static Texture2D LoadTexture(string filePath)
        {
            return ReadBits(new Bitmap(Bitmap.FromFile(filePath)));
        }

        private static Texture2D LoadTexture(Stream stream)
        {
            return ReadBits(new Bitmap(Bitmap.FromStream(stream)));
        }

        private static Texture2D ReadBits(Bitmap bitmap)
        {
            Texture2D texture = new Texture2D();

            texture.Handle = GL.GenTexture();
            GL.BindTexture(TextureTarget.Texture2D, texture.Handle);

            Img.BitmapData data = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), Img.ImageLockMode.ReadOnly, Img.PixelFormat.Format32bppArgb);

            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, data.Width, data.Height, 0, PixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0);
   
            bitmap.UnlockBits(data);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)All.ClampToBorder);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)All.ClampToBorder);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)All.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)All.Linear);

            texture.Width = bitmap.Width;
            texture.Height = bitmap.Height;

            bitmap.Dispose();
            return texture;
        }
    }
}
