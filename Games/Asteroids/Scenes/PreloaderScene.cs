//-----------------------------------------------------------------------
// <copyright file="Preloader.cs" company="Mooglegiant" >
//      Copyright (c) Mooglegiant. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Asteroids.Scenes
{
    using System;
    using Lycader;
    using Lycader.Graphics;
    using OpenTK;
    using OpenTK.Input;
    using Lycader.Scenes;
    using Lycader.Utilities;
    using Lycader.Audio;
    using System.Threading.Tasks;
    using Lycader.Graphics.Primitives;
    using OpenTK.Graphics;

    /// <summary>
    /// Level start screenlet
    /// </summary>
    public class PreloaderScene : IScene
    {
        /// <summary>
        /// Initializes a new instance of the preloader class
        /// </summary>
        public PreloaderScene()
        {
        }

        public void Load()
        {
            Preloader.Texture("font", FileFinder.Find("Assets", "Fonts", "defaultfont.png"));

            Preloader.Texture("background", FileFinder.Find("Assets", "Images", "background.png"));
            Preloader.Texture("player", FileFinder.Find("Assets", "Images", "player.png"));
            Preloader.Texture("player_thrust1", FileFinder.Find("Assets", "Images", "player_thrust1.png"));
            Preloader.Texture("player_thrust2", FileFinder.Find("Assets", "Images", "player_thrust2.png"));
            Preloader.Texture("bullet", FileFinder.Find("Assets", "Images", "bullet.png"));
            Preloader.Texture("ship", FileFinder.Find("Assets", "Images", "ship.png"));

            Preloader.Texture("asteroid1-1", FileFinder.Find("Assets", "Images", "asteroid-small1.png"));
            Preloader.Texture("asteroid1-2", FileFinder.Find("Assets", "Images", "asteroid-small2.png"));
            Preloader.Texture("asteroid1-3", FileFinder.Find("Assets", "Images", "asteroid-small3.png"));

            Preloader.Texture("asteroid2-1", FileFinder.Find("Assets", "Images", "asteroid-med1.png"));
            Preloader.Texture("asteroid2-2", FileFinder.Find("Assets", "Images", "asteroid-med2.png"));
            Preloader.Texture("asteroid2-3", FileFinder.Find("Assets", "Images", "asteroid-med3.png"));

            Preloader.Texture("asteroid3-1", FileFinder.Find("Assets", "Images", "asteroid-large1.png"));
            Preloader.Texture("asteroid3-2", FileFinder.Find("Assets", "Images", "asteroid-large2.png"));
            Preloader.Texture("asteroid3-3", FileFinder.Find("Assets", "Images", "asteroid-large3.png"));

            Preloader.Audio(FileFinder.Find("Assets", "Sounds"));
        }

        public void Unload()
        {
   
        }

        /// <summary>
        /// Implements OnUpdateFrame
        /// </summary>
        /// <param name="keyboard">current keyboard</param>
        /// <param name="e">event args</param>
        public void Update(FrameEventArgs e)
        {
            Preloader.Process(10);
            if (Preloader.IsQueueEmpty())
            {
                Globals.InitializeHUD();
                LycaderEngine.ChangeScene(new TitleScreen());
            }

            if (InputHelper.IsKeyPressed(Key.Escape))
            {
                LycaderEngine.Game.Exit();
            }

            if (InputHelper.IsKeyPressed(Key.F11))
            {
                if (LycaderEngine.Game.WindowState == WindowState.Fullscreen)
                    LycaderEngine.Game.WindowState = WindowState.Normal;
                else
                    LycaderEngine.Game.WindowState = WindowState.Fullscreen;
            }
        }

        /// <summary>
        /// Implements OnRender
        /// </summary>
        /// <param name="e">event args</param>
        public void Draw(FrameEventArgs e)
        {
        }
    }
}
