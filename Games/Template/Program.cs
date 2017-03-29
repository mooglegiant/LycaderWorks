//-----------------------------------------------------------------------
// <copyright file="Program.cs" company="Mooglegiant" >
//      Copyright (c) Mooglegiant. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Template
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
            Engine.Initalize(640, 480, "Template");
            Engine.Run(new PreloaderScene(), 30.0);
        }
    }
}
