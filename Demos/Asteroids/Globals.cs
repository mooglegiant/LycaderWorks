//-----------------------------------------------------------------------
// <copyright file="Globals.cs" company="Mooglegiant" >
//      Copyright (c) Mooglegiant. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Asteroids
{
    using System.Collections.Generic;

    /// <summary>
    /// Game's global objects
    /// </summary>
    public static class Globals
    {
        /// <summary>
        /// Gets or sets the current level
        /// </summary>
        public static int Level { get; set; }

        /// <summary>
        /// Gets or sets the current score
        /// </summary>
        public static int Score { get; set; }

        /// <summary>
        /// Initializes a new game
        /// </summary>
        public static void NewGame()
        {
            Level = 1;
            Score = 0;
        }
    }
}
