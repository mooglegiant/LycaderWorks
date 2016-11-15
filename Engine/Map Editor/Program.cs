//-----------------------------------------------------------------------
// <copyright file="Program.cs" company="Mooglegiant" >
//      Copyright (c) Mooglegiant. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace MapEditor
{
    using System;
    using System.Windows.Forms;

    /// <summary>
    /// Program's main class
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Project.Initialize();
            GlobalControls.Initialize();
            GlobalForms.Initialize();

            Application.Run(GlobalForms.Master);
        }
    }
}
