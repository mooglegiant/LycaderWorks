
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
            LycaderEngine.ScreenHeight = 600;
            LycaderEngine.ScreenWidth = 800;
            LycaderEngine.Fps = 30.0;

            LycaderEngine.Initalize(new MainScene());

            using (LycaderEngine.Game)
            {
                LycaderEngine.Game.Run(LycaderEngine.Fps);
            }
        }
    }
}
