//-----------------------------------------------------------------------
// <copyright file="Project.cs" company="Mooglegiant" >
//      Copyright (c) Mooglegiant. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace MapEditor
{
    using System.Drawing;
    using System.Windows.Forms;

    using MogAssist.Maps;

    /// <summary>
    /// Static class for different project variables instead of keeping them
    /// tied to a form or control
    /// </summary>
    public static class Project
    {
        /// <summary>
        /// Gets or sets the project's map
        /// </summary>
        public static Map Map { get; set; }

        /// <summary>
        /// Gets or sets the project's tile textures
        /// </summary>
        public static Bitmap[] Textures { get; set; }

        /// <summary>
        /// Gets or sets the project's selected tiles
        /// </summary>
        public static int[,] Selection { get; set; }

        /// <summary>
        /// Gets or sets the project's active layer to work with
        /// </summary>
        public static int ActiveLayer { get; set; }

        /// <summary>
        /// Gets or sets the project's tile textures - used to layout control
        /// </summary>
        public static PictureBox[] TileArray { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the current map is saved or not
        /// </summary>
        public static bool IsSaved { get; set; }

        /// <summary>
        /// Gets or sets the loaded map file location
        /// </summary>
        public static string MapFile { get; set; }

        /// <summary>
        /// Gets or sets the loaded texture file location
        /// </summary>
        public static string TileFile { get; set; }

        /// <summary>
        /// Gets or sets the selction box for the tile control
        /// </summary>
        public static Selection TileSelectionBox { get; set; }

        /// <summary>
        /// Gets or sets the selection box for the map control
        /// </summary>
        public static Selection MapSelectionBox { get; set; }

        /// <summary>
        /// Initializes the project's members
        /// </summary>
        public static void Initialize()
        {
            IsSaved = false;
            TileArray = new PictureBox[1];
            ActiveLayer = 0;
            Selection = new int[1, 1];
            Selection[0, 0] = -1;

            Textures = new Bitmap[1];
            Map = new Map();
            Map.TileSize = 16;
            Map.Name = "Untitled";
            MapFile = string.Empty;
            TileFile = string.Empty;

            TileSelectionBox = new Selection(0, 0);
            MapSelectionBox = new Selection(0, 0);

            PaintMap.MapImage = new Bitmap(1, 1);
            PaintMap.RenderAll = false;
        }
    }
}
