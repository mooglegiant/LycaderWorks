//-----------------------------------------------------------------------
// <copyright file="Forms.cs" company="Mooglegiant" >
//      Copyright (c) Mooglegiant. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace MapEditor
{
    using MapEditor.Forms;

    /// <summary>
    /// Static instance of all the forms used by the map editor
    /// </summary>
    public static class GlobalForms
    {
        /// <summary>
        /// Gets or sets the Master form
        /// </summary>
        public static Master Master { get; set; }

        /// <summary>
        /// Gets or sets the About form
        /// </summary>
        public static About About { get; set; }

        /// <summary>
        /// Gets or sets the MapProperties form
        /// </summary>
        public static MapProperties MapProperties { get; set; }

        /// <summary>
        /// Initializes static members of the GlobalForms class
        /// </summary>
        public static void Initialize()
        {
            Master = new Master();
            About = new About();
            MapProperties = new MapProperties();
        }
    }
}
