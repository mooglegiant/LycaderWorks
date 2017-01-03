//-----------------------------------------------------------------------
// <copyright file="Program.cs" company="Mooglegiant" >
//      Copyright (c) Mooglegiant. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Asteroids
{
    using System;
    using Lycader;
    using Scenes;

    /// <summary>
    /// The main program entry point
    /// </summary>
    public class Program
    {
        [STAThread]
        private static void Main()
        {
            Globals.NewGame();

            LycaderEngine.Initalize(800, 600, "Asteroids");
            LycaderEngine.Run(new Preloader(), 30.0);
        }
    }
}
