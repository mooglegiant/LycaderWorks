//-----------------------------------------------------------------------
// <copyright file="LycaderEngine.cs" company="Mooglegiant" >
//      Copyright (c) Mooglegiant. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Lycader
{
    using System.Drawing;

    public static class LycaderEngine
    {
        public static Screen Screen { get; private set; }

        public static void Initalize(int width, int height, string title)
        {
            Screen = new Screen(width, height, title);
            Resolution = new Size(width, height);
        }

        public static void Run(IScene scene, double fps)
        {
            ChangeScene(scene);
            using (Screen)
            {
                Screen.Run(fps);
            }
        }

        #region Screen Settings
        public static Color BackgroundColor { get; set; } = Color.Black;

        public static Size Resolution { get; internal set; } = new Size(0, 0);

        public static SizeF WindowAdjustment { get; internal set; } = new SizeF(0, 0);
        #endregion

        #region "Scene Management"

        public static IScene CurrentScene { get; internal set; }

        private static IScene NextScene { get; set; }

        public static bool IsSceneChanging { get; internal set; } = false;

        public static void ChangeScene(IScene next)
        {
            NextScene = next;
            IsSceneChanging = true;
        }

        internal static void ToggleScene()
        {
            if (IsSceneChanging)
            {
                CurrentScene.Unload();
                NextScene.Load();

                CurrentScene = NextScene;
                IsSceneChanging = false;
            }
        }
        #endregion  

        public static bool IsShuttingDown { get; internal set; } = false;
    }
}
