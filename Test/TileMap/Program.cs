//-----------------------------------------------------------------------
// <copyright file="Program.cs" company="Mooglegiant" >
//      Copyright (c) Mooglegiant. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace TileMap
{
    using System;
    using MogAssist;

    /// <summary>
    /// The main program entry point
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Programing starting point
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Config.ScreenWidth = 640;
            Config.ScreenHeight = 512;
            Config.Fps = 30.0;

            using (Game game = new Game())
            {
                game.Run(Config.Fps);
            }
        }
    }
}
