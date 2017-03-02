//-----------------------------------------------------------------------
// <copyright file="Program.cs" company="Mooglegiant" >
//      Copyright (c) Mooglegiant. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace TMNT
{
    using System;
    using Lycader;
    using TMNT.Scenes;

    /// <summary>
    /// The main program entry point
    /// </summary>
    public class Program
    {
        [STAThread]
        private static void Main()
        {
            LycaderEngine.Initalize(256, 224, "TMNT");
            LycaderEngine.Screen.Width = 600;
            LycaderEngine.Screen.Height = 480;

            LycaderEngine.Screen.Icon = new System.Drawing.Icon("Assets/icon.ico");
            LycaderEngine.Run(new PreloaderScene(), 30.0);
        }
    }
}
