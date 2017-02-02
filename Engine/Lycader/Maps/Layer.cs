//-----------------------------------------------------------------------
// <copyright file="Layer.cs" company="Mooglegiant" >
//      Copyright (c) Mooglegiant. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Lycader.Maps
{
    using OpenTK;
    using OpenTK.Graphics;
    using OpenTK.Graphics.OpenGL;

    /// <summary>
    /// Map Tile data
    /// </summary>
    public class Layer
    {
        /// <summary>
        /// Initializes a new instance of the Layer class
        /// </summary>
        /// <param name="order">Current layer index</param>
        /// <param name="width">tile count width</param>
        /// <param name="height">tile count height</param>
        public Layer(int order, int width, int height)
        {
            this.InitializeTiles(width, height);
            this.ScrollX = 0.0f;
            this.ScrollY = 0.0f;
            this.RepeatX = false;
            this.RepeatY = false;
            this.ScrollSpeedX = 1.0f;
            this.ScrollSpeedY = 1.0f;
            this.Order = order;
        }

        /// <summary>
        /// Gets or sets the Layer's X offset
        /// </summary>
        public float ScrollX { get; set; }

        /// <summary>
        /// Gets or sets the Layer's Y offset
        /// </summary>
        public float ScrollY { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to repeat when scrolling on X Coordinate
        /// </summary>
        public bool RepeatX { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to repeat when scrolling on Y Coordinate
        /// </summary>
        public bool RepeatY { get; set; }

        /// <summary>
        /// Gets or sets the X Scroll Speed
        /// </summary>
        public float ScrollSpeedY { get; set; }

        /// <summary>
        /// Gets or sets the Y Scroll Speed
        /// </summary>
        public float ScrollSpeedX { get; set; }

        /// <summary>
        /// Gets or sets the Layer's Width
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// Gets or sets the Layer's Height
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        /// Gets or sets the Layer's Tile data
        /// </summary>
        public int[,] Tiles { get; set; }

        /// <summary>
        /// Gets or sets the Layer's order #
        /// </summary>
        public int Order { get; set; }

        /// <summary>
        /// Resizes the layer
        /// </summary>
        /// <param name="newWidth">the tile count width</param>
        /// <param name="newHeight">the tile count height</param>
        public void Resize(int newWidth, int newHeight)
        {
            int[,] temp = (int[,])this.Tiles.Clone();
            int smallerWidth = (this.Width < newWidth) ? this.Width : newWidth;
            int smallerHeight = (this.Height < newHeight) ? this.Height : newHeight;
            this.InitializeTiles(newWidth, newHeight);

            for (int x = 0; x < smallerWidth; x++)
            {
                for (int y = 0; y < smallerHeight; y++)
                {
                    this.Tiles[x, y] = temp[x, y];
                }
            }

            this.Width = newWidth;
            this.Height = newHeight;
        }

        public void Blit(int tileSize, Camera camera, Texture texture)
        {
            // Maps always start from (0,0) world space           
            Vector3 screenPosition = camera.GetScreenPosition(new Vector3(0, 0, this.Order));

            float aspectY = camera.Zoom;
            float aspectX = camera.Zoom;

            int tileWidthCount = (LycaderEngine.Resolution.Width / tileSize) + 1;
            int tileHeightCount = (LycaderEngine.Resolution.Height / tileSize) + 1;

            int startX = 0;
            int startY = 0;
            int offsetX = 0;
            int offsetY = 0;
            int endX = this.Width;
            int endY = this.Height;

            // Finds tile array start
            if (this.RepeatX || this.RepeatY)
            {
                startX = ((int)(this.ScrollX * -1) / tileSize);
                startY = ((int)(this.ScrollY * -1) / tileSize);

                //Calculate parallax render offset
                offsetX = ((int)this.ScrollX % tileSize);
                offsetY = ((int)this.ScrollY % tileSize);
            }
            else
            {
                startX = ((int)(screenPosition.X * -1) / tileSize);
                startY = ((int)(screenPosition.Y * -1) / tileSize);

                offsetX = ((int)screenPosition.X % tileSize);
                offsetY = ((int)screenPosition.Y % tileSize);
            }

            texture.Bind();

            GL.PushMatrix();
            {
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)All.Nearest);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)All.Nearest);

                camera.SetViewport();
                camera.SetOrtho();

                // Loop for enough tiles to do screen and one tilesize padding around
                for (int i = -1; i <= tileWidthCount; i++)
                {
                    for (int j = -1; j <= tileHeightCount; j++)
                    {
                        int indexX = i + startX;
                        int indexY = j + startY;

                        if (this.RepeatX)
                        {
                            while (indexX >= this.Width)
                            {
                                indexX = indexX - this.Width;
                            }

                            while (indexX < 0)
                            {
                                indexX += this.Width;
                            }
                        }

                        if (this.RepeatY)
                        {
                            while (indexY >= this.Height)
                            {
                                indexY = indexY - this.Height;
                            }

                            while (indexY < 0)
                            {
                                indexY += this.Height;
                            }
                        }


                        if (indexX > -1 && indexX < this.Width && indexY > -1 && indexY < this.Height)
                        {
                            if (this.Tiles[indexX, indexY] != -1)
                            {
                                GL.Begin(PrimitiveType.Quads);
                                {
                                    float countX = texture.Width / tileSize;
                                    float countY = texture.Height / tileSize;

                                    int rowY = 0;

                                    int tile = this.Tiles[indexX, indexY];
                                    while (tile >= countX)
                                    {
                                        rowY++;
                                        tile -= (int)countX;
                                    }

                                    float left = tile / countX;
                                    float right = left + (1 / countX);

                                    float top = rowY * (1 / countY);
                                    float bottom = top + (1 / countY);

                                    GL.TexCoord2(left, bottom);
                                    //GL.Vertex3(offsetX + (tileSize * i) * camera.Zoom, offsetY + (tileSize * j) * camera.Zoom, screenPosition.Z);
                                    GL.Vertex3(offsetX + (tileSize * i) * camera.Zoom, offsetY + (tileSize * j) * camera.Zoom, this.Order);

                                    GL.TexCoord2(left, top);
                                    //GL.Vertex3(offsetX + (tileSize * i) * camera.Zoom, offsetY + (tileSize * (j + 1)) * camera.Zoom, screenPosition.Z);
                                    GL.Vertex3(offsetX + (tileSize * i) * camera.Zoom, offsetY + (tileSize * (j + 1)) * camera.Zoom, this.Order);

                                    GL.TexCoord2(right, top);
                                    //GL.Vertex3(offsetX + (tileSize * (i + 1)) * camera.Zoom, offsetY + (tileSize * (j + 1)) * camera.Zoom, screenPosition.Z);
                                    GL.Vertex3(offsetX + (tileSize * (i + 1)) * camera.Zoom, offsetY + (tileSize * (j + 1)) * camera.Zoom, this.Order);

                                    GL.TexCoord2(right, bottom);
                                    //GL.Vertex3(offsetX + (tileSize * (i + 1)) * camera.Zoom, offsetY + (tileSize * j) * camera.Zoom, screenPosition.Z);
                                    GL.Vertex3(offsetX + (tileSize * (i + 1)) * camera.Zoom, offsetY + (tileSize * j) * camera.Zoom, this.Order);
                                }
                            }
                        }
                    }
                }


                GL.End();
            }
            GL.PopMatrix();
        }

        /// <summary>
        /// Initializes the layer
        /// </summary>
        /// <param name="width">the tile count width</param>
        /// <param name="height">the tile count height</param>
        private void InitializeTiles(int width, int height)
        {
            this.Tiles = new int[width, height];
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    this.Tiles[x, y] = -1;
                }
            }

            this.Width = width;
            this.Height = height;
        }
    }
}
