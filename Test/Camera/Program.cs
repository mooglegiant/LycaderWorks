//-----------------------------------------------------------------------
// <copyright file="Program.cs" company="Mooglegiant" >
//      Copyright (c) Mooglegiant. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace CameraTest
{
    using System;
    using Lycader;

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
            Engine.Initalize(800, 600, "Camera Test");
            Engine.Run(new MainScene(), 30.0);
        }
    }
}
