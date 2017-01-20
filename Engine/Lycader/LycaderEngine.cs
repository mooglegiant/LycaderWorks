using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Lycader
{
    public static class LycaderEngine
    {

        public static Screen Screen { get; private set; }

        static public void Initalize(int width, int height, string title)
        {
            Screen = new Screen(width, height, title);
            Resolution = new Size(width, height);
        }

        static public void Run(IScene scene, double fps)
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

        static public IScene CurrentScene { get; internal set; }

        static private IScene NextScene { get; set; }

        static public bool IsSceneChanging { get; internal set; } = false;

        static public void ChangeScene(IScene next)
        {
            NextScene = next;
            IsSceneChanging = true;
        }

        static internal void ToggleScene()
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

        static public bool IsShuttingDown { get; internal set; } = false;
    }
}
