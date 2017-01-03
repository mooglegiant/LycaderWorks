
namespace Scrolling
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
            LycaderEngine.Initalize(800, 400, "TileMap");
            LycaderEngine.Run(new MainScene(), 30.0);
        }
    }
}
