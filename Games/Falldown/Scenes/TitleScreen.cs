//-----------------------------------------------------------------------
// <copyright file="TitleScreen.cs" company="Mooglegiant" >
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
    /// Title screen
    /// </summary>
    public class TitleScreen : IScene
    {
        /// <summary>
        /// The screens text class
        /// </summary>
        private FontEntity pressStart;

        private SceneManager manager = new SceneManager();

        /// <summary>
        /// Initializes a new instance of the TitleScreen class
        /// </summary>
        public TitleScreen()
        {       
        }

        public void Load()
        {
            this.pressStart = new FontEntity("pixel.png", 40, new Vector3(240, 300, 100), "Press Start");
            this.manager.Add(this.pressStart);

            new OggStreamer();

            OggStream stream = new OggStream("Assets/Music/menu.ogg");
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
            this.manager.Render();
        }
    }
}
