﻿
namespace PrimitiveTest
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
            LycaderEngine.Initalize(new MainScene(), 800, 600, "Primitives Test", 30.0);
        }
    }
}
