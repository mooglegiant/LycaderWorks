//-----------------------------------------------------------------------
// <copyright file="Program.cs" company="Mooglegiant" >
//      Copyright (c) Mooglegiant. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Asteroids
{
    using System;
    using Lycader;

    /// <summary>
    /// The main program entry point
    /// </summary>
    public class Program
    {
        [STAThread]
        private static void Main()
        {
            LycaderEngine.ScreenHeight = 600;
            LycaderEngine.ScreenWidth = 800;
            LycaderEngine.Fps = 30.0;

            Globals.NewGame();

            LycaderEngine.Initalize(new Scenes.LevelStart());

            using (LycaderEngine.Game)
            {
                LycaderEngine.Game.Run(LycaderEngine.Fps);
            }
        }
    }
}
