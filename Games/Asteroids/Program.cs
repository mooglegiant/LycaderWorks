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
            Globals.NewGame();

            LycaderEngine.Initalize(new Scenes.LevelStart(), 800, 600, "Asteroids", 30.0);
        }
    }
}
