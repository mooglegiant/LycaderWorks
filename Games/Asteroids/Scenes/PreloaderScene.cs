//-----------------------------------------------------------------------
// <copyright file="Preloader.cs" company="Mooglegiant" >
//      Copyright (c) Mooglegiant. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Asteroids.Scenes
{
    using System;
    using Lycader;
    using OpenTK;
    using OpenTK.Input;
    using Lycader.Scenes;
    using Lycader.Utilities;
    using System.Threading.Tasks;
    using Lycader.Entities;
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
            Lycader.ContentBuffer.AddTexture("font", FileFinder.Find("Assets", "Fonts", "defaultfont.png"));

            Lycader.ContentBuffer.AddTexture("background", FileFinder.Find("Assets", "Images", "background.png"));
            Lycader.ContentBuffer.AddTexture("player", FileFinder.Find("Assets", "Images", "player.png"));
            Lycader.ContentBuffer.AddTexture("player_thrust1", FileFinder.Find("Assets", "Images", "player_thrust1.png"));
            Lycader.ContentBuffer.AddTexture("player_thrust2", FileFinder.Find("Assets", "Images", "player_thrust2.png"));
            Lycader.ContentBuffer.AddTexture("bullet", FileFinder.Find("Assets", "Images", "bullet.png"));
            Lycader.ContentBuffer.AddTexture("ship", FileFinder.Find("Assets", "Images", "ship.png"));

            Lycader.ContentBuffer.AddTexture("asteroid1-1", FileFinder.Find("Assets", "Images", "asteroid-small1.png"));
            Lycader.ContentBuffer.AddTexture("asteroid1-2", FileFinder.Find("Assets", "Images", "asteroid-small2.png"));
            Lycader.ContentBuffer.AddTexture("asteroid1-3", FileFinder.Find("Assets", "Images", "asteroid-small3.png"));

            Lycader.ContentBuffer.AddTexture("asteroid2-1", FileFinder.Find("Assets", "Images", "asteroid-med1.png"));
            Lycader.ContentBuffer.AddTexture("asteroid2-2", FileFinder.Find("Assets", "Images", "asteroid-med2.png"));
            Lycader.ContentBuffer.AddTexture("asteroid2-3", FileFinder.Find("Assets", "Images", "asteroid-med3.png"));

            Lycader.ContentBuffer.AddTexture("asteroid3-1", FileFinder.Find("Assets", "Images", "asteroid-large1.png"));
            Lycader.ContentBuffer.AddTexture("asteroid3-2", FileFinder.Find("Assets", "Images", "asteroid-large2.png"));
            Lycader.ContentBuffer.AddTexture("asteroid3-3", FileFinder.Find("Assets", "Images", "asteroid-large3.png"));

            Lycader.ContentBuffer.AddAudio(FileFinder.Find("Assets", "Sounds"));
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
            Lycader.ContentBuffer.Process(10);
            if (Lycader.ContentBuffer.IsQueueEmpty())
            {
                Globals.InitializeHUD();
                LycaderEngine.ChangeScene(new TitleScreen());
            }

            if (InputManager.IsKeyPressed(Key.Escape))
            {
                LycaderEngine.Screen.Exit();
            }

            if (InputManager.IsKeyPressed(Key.F11))
            {
                if (LycaderEngine.Screen.WindowState == WindowState.Fullscreen)
                    LycaderEngine.Screen.WindowState = WindowState.Normal;
                else
                    LycaderEngine.Screen.WindowState = WindowState.Fullscreen;
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
