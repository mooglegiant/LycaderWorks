//-----------------------------------------------------------------------
// <copyright file="Texture.cs" company="Mooglegiant" >
//      Copyright (c) Mooglegiant. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Lycader.Graphics
{
    using System;
    using System.Drawing;
    using Img = System.Drawing.Imaging;

    using OpenTK;
    using OpenTK.Graphics.OpenGL;

    using Lycader.Collision;
    using System.Collections.Generic;

    /// <summary>
    /// Holds basic information about the Texture
    /// </summary>
    public class Texture 
    {
        private int textureID;

        /// <summary>
        /// Gets the height of the texture
        /// </summary>
        public float Height { get; internal set; }

        /// <summary>
        /// Gets the width of the texture
        /// </summary>
        public float Width { get; internal set; }

        /// <summary>
        /// Initializes a new instance of the Texture class
        /// </summary>
        public Texture()
        {                       
        }

        public void Bind()
        {
            GL.BindTexture(TextureTarget.Texture2D, this.textureID);
        }

        public void Delete()
        {
            GL.DeleteTexture(this.textureID);
        }

        public ICollidable GetTextureCollision(Vector3 position)
        {
            return new QuadCollidable(new Vector2(position.X, position.Y), this.Width, this.Height);
        }

        static public Texture CreateTexture(string fileName, bool useNearestFilter = false)
        {
            Texture texture = new Texture();

            texture.textureID = GL.GenTexture();
            Bitmap bitmap = new Bitmap(Bitmap.FromFile(fileName));

            GL.BindTexture(TextureTarget.Texture2D, texture.textureID);

            Img.BitmapData data = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), Img.ImageLockMode.ReadOnly, Img.PixelFormat.Format32bppArgb);

            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, data.Width, data.Height, 0, PixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0);
            bitmap.UnlockBits(data);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)All.ClampToEdge);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)All.ClampToEdge);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)All.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)All.Linear);

            if (useNearestFilter)
            {
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Nearest);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Nearest);
            }

            texture.Width = bitmap.Width;
            texture.Height = bitmap.Height;

            bitmap.Dispose();

            return texture;
        }

        static public List<Texture> CreateTextures(string fileName, int frameWidth, int frameHeight, bool useNearestFilter = false)
        {
            List<Texture> textures = new List<Texture>();
            Bitmap bitmap = new Bitmap(Bitmap.FromFile(fileName));

            int columnCount = bitmap.Width / frameWidth;
            int rowCount = bitmap.Height / frameHeight;

            for(int i = 0; i < columnCount; i++)
            {
                for (int j = 0; j < rowCount; j++)
                {
                    Texture texture = new Texture();
                    texture.textureID = GL.GenTexture();

                    GL.BindTexture(TextureTarget.Texture2D, texture.textureID);

                    Bitmap tile = bitmap.Clone(new Rectangle(i * frameWidth, j * frameHeight, frameWidth, frameHeight), Img.PixelFormat.Format32bppArgb);

                    Img.BitmapData data = tile.LockBits(new Rectangle(0, 0, frameWidth, frameHeight), Img.ImageLockMode.ReadOnly, Img.PixelFormat.Format32bppArgb);

                    GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, data.Width, data.Height, 0, PixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0);
                    tile.UnlockBits(data);

                    GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)All.ClampToEdge);
                    GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)All.ClampToEdge);

                    GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)All.Linear);
                    GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)All.Linear);

                    if (useNearestFilter)
                    {
                        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Nearest);
                        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Nearest);
                    }

                    texture.Width = frameWidth;
                    texture.Height = frameHeight;

                    textures.Add(texture);
                }
            }
          
            bitmap.Dispose();

            return textures;
        }
    }
}