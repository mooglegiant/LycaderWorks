//-----------------------------------------------------------------------
// <copyright file="Globals.cs" company="Mooglegiant" >
//      Copyright (c) Mooglegiant. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Animation
{
    using System.Collections.Generic;

    /// <summary>
    /// Game's global objects
    /// </summary>
    public static class Globals
    {
        /// <summary>
        /// Gets or sets the current screen
        /// </summary>
        public static Screens.IScreen CurrentScreen { get; set; }

        /// <summary>
        /// Initializes a new game
        /// </summary>
        public static void NewGame()
        {
            CurrentScreen = new Screens.PlayingScreen();
        }
    }
}
