//-----------------------------------------------------------------------
// <copyright file="Tools.cs" company="Mooglegiant" >
//      Copyright (c) Mooglegiant. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace MapEditor
{
    using System.Drawing;

    /// <summary>
    /// Different Brush Modes
    /// </summary>
    public enum BrushMode
    {
        /// <summary>
        /// Draws a single tile
        /// </summary>
        Brush,

        /// <summary>
        /// Fills a single tile in all connecting matching tiles
        /// </summary>
        Paint,

        /// <summary>
        /// Selects a group of tiles
        /// </summary>
        Selection,

        /// <summary>
        /// Pases the selected tiles from the Selection Mode
        /// </summary>
        Paste
    }

    /// <summary>
    /// Handles changing map tile values
    /// </summary>
    public static class Tools
    {
        /// <summary>
        /// Initializes static members of the Tools class
        /// </summary>
        static Tools()
        {
            BrushMode = BrushMode.Brush;
        }

        /// <summary>
        /// Gets or sets the current BrushMode
        /// </summary>
        public static BrushMode BrushMode { get; set; }

        /// <summary>
        /// Processing a mouse click event on the map
        /// </summary>
        /// <param name="leftPressed">A value indicating whether the left button is pressed or not</param>
        /// <param name="rightPressed">A value indicating whether the right button is pressed or not</param>
        /// <param name="x">X offset on the control</param>
        /// <param name="y">Y offset on the control</param>
        public static void Click(bool leftPressed, bool rightPressed, int x, int y)
        {
            Process(leftPressed, rightPressed, x, y);
        }

        /// <summary>
        /// Processing a mouse move event on the map
        /// </summary>
        /// <param name="leftPressed">A value indicating whether the left button is pressed or not</param>
        /// <param name="rightPressed">A value indicating whether the right button is pressed or not</param>
        /// <param name="x">X offset on the control</param>
        /// <param name="y">Y offset on the control</param>
        public static void Drag(bool leftPressed, bool rightPressed, int x, int y)
        {
            Process(leftPressed, rightPressed, x, y);
        }

        /// <summary>
        /// Does the tile changing after a click or mouse movement
        /// </summary>
        /// <param name="leftPressed">A value indicating whether the left button is pressed or not</param>
        /// <param name="rightPressed">A value indicating whether the right button is pressed or not</param>
        /// <param name="x">X offset on the control</param>
        /// <param name="y">Y offset on the control</param>
        private static void Process(bool leftPressed, bool rightPressed, int x, int y)
        {
            if (IsValidLocation(x, y))
            {
                switch (BrushMode)
                {
                    case BrushMode.Brush:
                        {
                            if (leftPressed)
                            {
                                SetTile(x, y, Project.Map.Layers[Project.ActiveLayer].Tiles[x, y], Project.Selection[0, 0], false);
                            }
                            else if (rightPressed)
                            {
                                SetTile(x, y, Project.Map.Layers[Project.ActiveLayer].Tiles[x, y], -1, false);
                            }

                            break;
                        }

                    case BrushMode.Paint:
                        {
                            if (leftPressed)
                            {
                                SetTile(x, y, Project.Map.Layers[Project.ActiveLayer].Tiles[x, y], Project.Selection[0, 0], true);
                            }
                            else if (rightPressed)
                            {
                                SetTile(x, y, Project.Map.Layers[Project.ActiveLayer].Tiles[x, y], -1, true);
                            }

                            break;
                        }

                    case BrushMode.Selection:
                        {
                            if (rightPressed)
                            {
                                for (int i = 0; i < Project.Selection.GetLength(0); i++)
                                {
                                    for (int j = 0; j < Project.Selection.GetLength(1); j++)
                                    {
                                        if (IsValidLocation(x + i, y + j))
                                        {
                                            SetTile(x + i, y + j, Project.Map.Layers[Project.ActiveLayer].Tiles[x + i, y + j], Project.Selection[i, j], false);
                                        }
                                    }
                                }
                            }

                            break;
                        }
                }
            }
        }

        /// <summary>
        /// Changes the current tile in the map tile array to a new value, loops if in fill mode
        /// </summary>
        /// <param name="x">selected layer's x index</param>
        /// <param name="y">selected layer's y index</param>
        /// <param name="currentTile">tile of the index before change</param>
        /// <param name="newTile">tile of the index to change to</param>
        /// <param name="fill">a value indicating whether to fill matching neighbor tiles</param>
        private static void SetTile(int x, int y, int currentTile, int newTile, bool fill)
        {
            if (IsValidLocation(x, y))
            {
                if (Project.Map.Layers[Project.ActiveLayer].Tiles[x, y] == newTile)
                {
                    return;
                }

                if (Project.Map.Layers[Project.ActiveLayer].Tiles[x, y] == currentTile)
                {
                    Project.Map.Layers[Project.ActiveLayer].Tiles[x, y] = newTile;

                    if (fill)
                    {
                        SetTile(x, y - 1, currentTile, newTile, fill);
                        SetTile(x, y + 1, currentTile, newTile, fill);
                        SetTile(x + 1, y, currentTile, newTile, fill);
                        SetTile(x - 1, y, currentTile, newTile, fill);
                    }

                    PaintMap.RenderTile(x, y);
                }
            }
        }

        /// <summary>
        /// Checks if the given indexs are within the layer's bounds
        /// </summary>
        /// <param name="x">selected layer's x index</param>
        /// <param name="y">selected layer's y index</param>
        /// <returns>A value indicating whether the indexs are in bounds or not</returns>
        private static bool IsValidLocation(int x, int y)
        {
            if (x < 0 || x >= Project.Map.Layers[Project.ActiveLayer].Width)
            {
                return false;
            }

            if (y < 0 || y >= Project.Map.Layers[Project.ActiveLayer].Height)
            {
                return false;
            }

            return true;
        }
    }
}
