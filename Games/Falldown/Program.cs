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
            LycaderEngine.Initalize(640, 480, "Falldown");
            LycaderEngine.Run(new PreloaderScene(), 30.0);
        }
    }
}
