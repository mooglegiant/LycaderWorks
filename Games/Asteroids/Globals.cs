//-----------------------------------------------------------------------
// <copyright file="Globals.cs" company="Mooglegiant" >
//      Copyright (c) Mooglegiant. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Asteroids
{
    using Lycader;
    using Lycader.Graphics;
    using Lycader.Scenes;
    using OpenTK;
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

        public static SceneManager HUDManager { get; set; }

        public static void InitializeHUD()
        {
            HUDManager = new SceneManager();
            HUDManager.Add(new HUD(TextureContent.Find("font"), 20, new Vector3(20, LycaderEngine.Game.Height - 25, 100), Globals.Score.ToString("d7")));                          
        }

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
