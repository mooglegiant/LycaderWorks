//-----------------------------------------------------------------------
// <copyright file="Program.cs" company="Mooglegiant" >
//      Copyright (c) Mooglegiant. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Falldown
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
            Globals.Scores.Load("Save.xml");
            Globals.Scores.AddInt("Mode1Count", 0);
            Globals.Scores.AddInt("Mode2Count", 0);
            Globals.Scores.AddInt("TotalScore1", 0);
            Globals.Scores.AddInt("HighScore1", 0);
            Globals.Scores.AddInt("TotalScore2", 0);
            Globals.Scores.AddInt("HighScore2", 0);

            LycaderEngine.Initalize(640, 480, "Falldown");
            LycaderEngine.Run(new PreloaderScene(), 30.0);
        }
    }
}
