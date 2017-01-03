
namespace ThePit
{
    using System;
    using Lycader;
    using Scenes;

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
            LycaderEngine.Initalize(800, 600, "The Pit");
            LycaderEngine.Run(new MainScene(), 30.0);
        }
    }
}
