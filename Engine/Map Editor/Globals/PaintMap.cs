//-----------------------------------------------------------------------
// <copyright file="PaintMap.cs" company="Mooglegiant" >
//      Copyright (c) Mooglegiant. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace MapEditor
{
    using System.Drawing;

    /// <summary>
    /// Generates an image of the current map
    /// </summary>
    public static class PaintMap
    {
        /// <summary>
        /// Current starting layer to draw
        /// </summary>
        private static int startingLayer = 0;

        /// <summary>
        /// Current ending layer to draw
        /// </summary>
        private static int endingLayer = 0;

        /// <summary>
        /// visible layers max tile width
        /// </summary>
        private static int maxX = 0;

        /// <summary>
        /// visible layers max tile height
        /// </summary>
        private static int maxY = 0;

        /// <summary>
        /// Gets or sets a value indicating whether to render all layers or not
        /// </summary>
        public static bool RenderAll { get; set; }

        /// <summary>
        /// Gets or sets a pre-drawn image of all the visible tiles
        /// </summary>
        public static Bitmap MapImage { get; set; }

        /// <summary>
        /// Redraws the entire map
        /// </summary>
        public static void RenderMap()
        {
            GetSize();
            NewImage();

            for (int x = 0; x < maxX; x++)
            {
                for (int y = 0; y < maxY; y++)
                {
                    RenderTile(x, y);
                }
            }
        }

        /// <summary>
        /// Redraws a given tile on the map
        /// </summary>
        /// <param name="x">X tile offset</param>
        /// <param name="y">Y tile offset</param>
        public static void RenderTile(int x, int y)
        {
            Graphics g = Graphics.FromImage(MapImage);

            // Erase the area completely since some pixels may be transparent
            g.FillRectangle(
                new SolidBrush(Project.Map.Background),
                x * (Project.Map.TileSize + 1),
                y * (Project.Map.TileSize + 1),
                Project.Map.TileSize + 1,
                Project.Map.TileSize + 1);

            // Redraw each visible layer in that tile spot
            for (int index = startingLayer; index <= endingLayer; index++)
            {
                if (x < Project.Map.Layers[index].Width && y < Project.Map.Layers[index].Height)
                {
                    int frame = Project.Map.Layers[index].Tiles[x, y];
                    if (frame != -1)
                    {
                        g.DrawImageUnscaled(
                            Project.TileArray[frame].Image,
                            (x * (Project.Map.TileSize + 1)) + 1,
                            (y * (Project.Map.TileSize + 1)) + 1);
                    }
                }
            }

            // Redraw the map grid around the tile
            g.DrawRectangle(
                new Pen(Color.Black),
                x * (Project.Map.TileSize + 1),
                y * (Project.Map.TileSize + 1),
                Project.Map.TileSize + 1,
                Project.Map.TileSize + 1);
        }

        /// <summary>
        /// Finds the currently visible layers
        /// </summary>
        private static void GetSize()
        {
            startingLayer = Project.ActiveLayer;
            endingLayer = Project.ActiveLayer;

            if (RenderAll)
            {
                startingLayer = 0;
                endingLayer = Project.Map.Layers.Count - 1;
            }
        }

        /// <summary>
        /// Creates a new image for drawing the map on, finds the max width/height
        /// </summary>
        private static void NewImage()
        {
            maxX = 0;
            maxY = 0;
            for (int index = startingLayer; index <= endingLayer; index++)
            {
                maxX = (maxX > Project.Map.Layers[index].Width) ? maxX : Project.Map.Layers[index].Width;
                maxY = (maxY > Project.Map.Layers[index].Height) ? maxY : Project.Map.Layers[index].Height;
            }

            int width = (maxX * Project.Map.TileSize) + maxX + 1;
            int height = (maxY * Project.Map.TileSize) + maxY + 1;
            MapImage = new Bitmap(width, height);
        }
    }
}
