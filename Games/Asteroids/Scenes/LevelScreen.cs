//-----------------------------------------------------------------------
// <copyright file="LevelScreen.cs" company="Mooglegiant" >
//      Copyright (c) Mooglegiant. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Asteroids.Scenes
{
    using Lycader;
    using Lycader.Entities;
    using Lycader.Scenes;
    using OpenTK;
    using OpenTK.Input;
    using System;
    using System.Linq;

    /// <summary>
    /// Playing screenlet
    /// </summary>
    public class LevelScreen : IScene
    {
        /// <summary>
        /// The screens text class
        /// </summary>
        private SpriteFont score;
        private SpriteFont levelDisplay;
        private SceneManager manager = new SceneManager();

        private int timer = 100;

        /// <summary>
        /// Initializes a new instance of the PlayingScreen class
        /// </summary>
        public LevelScreen()
        {       
        }

        public void Load()
        {
            this.levelDisplay = new SpriteFont(TextureManager.Find("font"), 40, new Vector3(270, 300, 100), string.Format("Level: {0}", Globals.Level));
            manager.Add(this.levelDisplay);
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
            timer--;
            if (timer == 0)
            {
                LycaderEngine.ChangeScene(new PlayingScreen());
            }

            if (InputHelper.IsKeyPressed(Key.Escape))
            {
                LycaderEngine.Screen.Exit();
            }

            if (InputHelper.IsKeyPressed(Key.F11))
            {
                if (LycaderEngine.Screen.WindowState == WindowState.Fullscreen)
                    LycaderEngine.Screen.WindowState = WindowState.Normal;
                else
                    LycaderEngine.Screen.WindowState = WindowState.Fullscreen;
            }

            manager.Update();                    
        }

        /// <summary>
        /// Implements OnRender
        /// </summary>
        /// <param name="e">event args</param>
        public void Draw(FrameEventArgs e)
        {
            manager.Render();           
        }
    }
}
