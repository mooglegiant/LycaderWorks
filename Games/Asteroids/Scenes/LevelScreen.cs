//-----------------------------------------------------------------------
// <copyright file="LevelScreen.cs" company="Mooglegiant" >
//      Copyright (c) Mooglegiant. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Asteroids.Scenes
{
    using Lycader;
    using Lycader.Audio;
    using Lycader.Graphics;
    using Lycader.Input;
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
            this.score = new SpriteFont(TextureContent.Get("font"), 20, new Vector3(20, LycaderEngine.Game.Height - 25, 100), Globals.Score.ToString("d7"));
            manager.Add(this.score);

            this.levelDisplay = new SpriteFont(TextureContent.Get("font"), 40, new Vector3(270, 300, 100), string.Format("Level: {0}", Globals.Level));
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

            if (KeyboardHelper.IsKeyPressed(Key.Escape))
            {
                LycaderEngine.Game.Exit();
            }

            if (KeyboardHelper.IsKeyPressed(Key.F11))
            {
                if (LycaderEngine.Game.WindowState == WindowState.Fullscreen)
                    LycaderEngine.Game.WindowState = WindowState.Normal;
                else
                    LycaderEngine.Game.WindowState = WindowState.Fullscreen;
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
