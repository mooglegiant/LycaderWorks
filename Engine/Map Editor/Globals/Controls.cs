//-----------------------------------------------------------------------
// <copyright file="Controls.cs" company="Mooglegiant" >
//      Copyright (c) Mooglegiant. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace MapEditor
{
    using MapEditor.Controls;

    /// <summary>
    /// Static instance of all the controls used by the map editor
    /// </summary>
    public static class GlobalControls
    {
        /// <summary>
        /// Gets or sets the Menu control
        /// </summary>
        public static ControlMenu Menu { get; set; }

        /// <summary>
        /// Gets or sets the Project control
        /// </summary>
        public static ControlProject Project { get; set; }

        /// <summary>
        /// Gets or sets the ToolBar control
        /// </summary>
        public static ControlToolBar ToolBar { get; set; }

        /// <summary>
        /// Gets or sets the Map control
        /// </summary>
        public static ControlMap Map { get; set; }

        /// <summary>
        /// Gets or sets the Tile control
        /// </summary>
        public static ControlTiles Tiles { get; set; }

        /// <summary>
        /// Initializes all the static members of the GlobalControls class
        /// </summary>
        public static void Initialize()
        {
            Menu = new ControlMenu();
            ToolBar = new ControlToolBar();
            Map = new ControlMap();
            Tiles = new ControlTiles();
            Project = new ControlProject();
        }

        /// <summary>
        /// Calls the control update function to rerender them
        /// </summary>
        public static void UpdateControls()
        {
            Tiles.ControlUpdate();
            Map.ControlUpdate();
        }
    }
}
