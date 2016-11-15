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

    using Lycader.Graphics;

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
            this.X = 0.0f;
            this.Y = 0.0f;
            this.RepeatX = false;
            this.RepeatY = false;
            this.ScrollSpeedX = 1.0f;
            this.ScrollSpeedY = 1.0f;
            this.Order = order;
        }

        /// <summary>
        /// Gets or sets the Layer's X offset
        /// </summary>
        public float X { get; set; }

        /// <summary>
        /// Gets or sets the Layer's Y offset
        /// </summary>
        public float Y { get; set; }

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

        public void Blit(int tileSize, Camera camera, Texture2D texture)
        {
            Vector2 screenPosition = new Vector2(this.X - camera.Position.X, this.Y - camera.Position.Y);

            float aspectY = camera.Zoom;
            float aspectX = camera.Zoom;

            int tileWidthCount = (LycaderEngine.ScreenWidth / tileSize) + 1;
            int tileHeightCount = (LycaderEngine.ScreenHeight / tileSize) + 1;

            int startX = 0;
            int startY = 0;
            float offsetX = 0;
            float offsetY = 0;
            int endX = this.Width;
            int endY = this.Height;

            // Finds corner posistion and tilesize offset
            startX = (int)this.X / tileSize;
            startY = (int)this.Y / tileSize;
            offsetX = this.X % (float)tileSize;
            offsetY = this.Y % (float)tileSize;

            GL.Color4(Color4.White);
            GL.BindTexture(TextureTarget.Texture2D, texture.Handle);

            // Loop for enough tiles to do screen and one tilesize padding around
            for (int i = -1; i < tileWidthCount; i++)
            {
                for (int j = -1; j < tileHeightCount; j++)
                {
                    int x = startX + i;
                    int y = startY + j;

                    if (this.RepeatX)
                    {
                        while (x >= this.Width)
                        {
                            x = x - this.Width;
                        }

                        while (x < 0)
                        {
                            x += this.Width;
                        }
                    }

                    if (this.RepeatY)
                    {
                        while (y >= this.Height)
                        {
                            y = y - this.Height;
                        }

                        while (y < 0)
                        {
                            y += this.Height;
                        }
                    }

                    if (x > -1 && x < this.Width && y > -1 && y < this.Height)
                    {
                        if (this.Tiles[x, y] != -1)
                        {
                            GL.PushMatrix();
                            {
                                GL.Viewport((int)camera.ViewPort.Left, (int)camera.ViewPort.Bottom, (int)camera.ViewPort.Right, (int)camera.ViewPort.Top);
                                GL.Translate((i * tileSize) - offsetX, (j * tileSize) - offsetY, 0);
                                GL.Scale(tileSize, tileSize, 1);
                                GL.Begin(PrimitiveType.Quads);
                                {
                                    float countX = texture.Width / tileSize;
                                    float countY = texture.Height / tileSize;

                                    int rowY = 0;

                                    int tile = this.Tiles[x, y];
                                    while (tile >= countX)
                                    {
                                        rowY++;
                                        tile -= (int)countX;
                                    }

                                    double left = tile / countX;
                                    double right = left + (1 / countX);

                                    double top = rowY * (1 / countY);
                                    double bottom = top + (1 / countY);

                                    GL.TexCoord2(left, bottom);
                                    GL.Vertex3(0, 0, this.Order);
                                    GL.TexCoord2(right, bottom);
                                    GL.Vertex3(1, 0, this.Order);
                                    GL.TexCoord2(right, top);
                                    GL.Vertex3(1, 1, this.Order);
                                    GL.TexCoord2(left, top);
                                    GL.Vertex3(0, 1, this.Order);
                                }
                                GL.End();
                            }
                            GL.PopMatrix();
                        }
                    }
                }
            }
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
