//-----------------------------------------------------------------------
// <copyright file="Program.cs" company="Mooglegiant" >
//      Copyright (c) Mooglegiant. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Animation
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

            Engine.Initalize(800, 600, "Animation Test");
            Engine.Run(new PlayingScreen(), 30.0);
        }
    }
}
