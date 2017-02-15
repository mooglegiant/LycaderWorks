//-----------------------------------------------------------------------
// <copyright file="IntroScreen.cs" company="Mooglegiant" >
//      Copyright (c) Mooglegiant. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Falldown.Scenes
{
    using System;

    using OpenTK;
    using OpenTK.Input;

    using Lycader;
    using Lycader.Entities;
    using Lycader.Scenes;

    /// <summary>
    /// Intro screen
    /// </summary>
    public class IntroScreen : IScene
    {
        private SceneManager manager = new SceneManager();
        private SpriteEntity logo = new SpriteEntity();
        /// <summary>
        /// Initializes a new instance of the TitleScreen class
        /// </summary>
        public IntroScreen()
        {       
        }

        public void Load()
        {
            logo.Texture = "Logo.gif";
            logo.Alpha = 0;
            this.manager.Add(logo);

            OggStream stream = new OggStream("Assets/Music/PSX.ogg");
            stream.Play();
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
            if (InputManager.IsKeyPressed(Key.Enter))
            {
             //   Globals.NewGame();
           //''     LycaderEngine.ChangeScene(new LevelScreen());
            }

            if (InputManager.IsKeyPressed(Key.Escape))
            {
                LycaderEngine.Screen.Exit();
            }

            if (InputManager.IsKeyPressed(Key.F11))
            {
                LycaderEngine.Screen.ToggleFullScreen();
            }

            this.manager.Update();
        }

        /// <summary>
        /// Implements OnRender
        /// </summary>
        /// <param name="e">event args</param>
        public void Draw(FrameEventArgs e)
        {
            logo.Alpha++;
            this.manager.Render();
        }
    }
}
