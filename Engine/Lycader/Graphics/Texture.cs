//-----------------------------------------------------------------------
// <copyright file="Texture.cs" company="Mooglegiant" >
//      Copyright (c) Mooglegiant. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Lycader
{
    using System;
    using System.Drawing;
    using Img = System.Drawing.Imaging;

    using OpenTK;
    using OpenTK.Graphics.OpenGL;

    using Lycader.Collision;

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
        public Texture(string fileName, bool useNearestFilter = false)
        {  
            this.textureID = GL.GenTexture();
            Bitmap bitmap = new Bitmap(Bitmap.FromFile(fileName));

            GL.BindTexture(TextureTarget.Texture2D, this.textureID);

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

            this.Width = bitmap.Width;
            this.Height = bitmap.Height;

            bitmap.Dispose();                    
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
    }
}