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
            Engine.Initalize(256, 224, "TMNT");
            Engine.Screen.Width = 600;
            Engine.Screen.Height = 480;

            Engine.Screen.Icon = new System.Drawing.Icon("Assets/icon.ico");
            Engine.Run(new CharacterSelectScene(), 30.0);
        }
    }
}
