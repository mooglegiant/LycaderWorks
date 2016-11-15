//-----------------------------------------------------------------------
// <copyright file="LevelStart.cs" company="Mooglegiant" >
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
    using Lycader.Input;
    using Lycader.Utilities;
    using Lycader.Audio;

    /// <summary>
    /// Level start screenlet
    /// </summary>
    public class LevelStart : IScene
    {
        /// <summary>
        /// Diplays the current level count
        /// </summary>
        private SpriteFont text;

        /// <summary>
        /// counter to change screens
        /// </summary>
        private int counter;

        /// <summary>
        /// Initializes a new instance of the LevelStart class
        /// </summary>
        public LevelStart()
        {
        }

        public void Load()
        {
            TextureContent.Load("background", FileFinder.Find("Resources", "Images", "background.png"));
            TextureContent.Load("ship", FileFinder.Find("Resources", "Images", "ship.png"));
            TextureContent.Load("bullet", FileFinder.Find("Resources", "Images", "bullet.png"));

            TextureContent.Load("asteroid1", FileFinder.Find("Resources", "Images", "asteroid-small.png"));
            TextureContent.Load("asteroid2", FileFinder.Find("Resources", "Images", "asteroid-med.png"));
            TextureContent.Load("asteroid3", FileFinder.Find("Resources", "Images", "asteroid-large.png"));
            TextureContent.Load("asteroid4", FileFinder.Find("Resources", "Images", "asteroid-mega.png"));

            TextureContent.Load("font", FileFinder.Find("Resources", "Images", "font.png"));
            TextureContent.Load("font1", FileFinder.Find("Resources", "Images", "font.png"));

            AudioContent.Load("sound", FileFinder.Find("Resources", "Sounds", "boop.wav"));

            this.text = new SpriteFont(TextureContent.Get("font"), 75);
            this.text.X = 200;
            this.text.Y = 300;
            this.text.Text = "Level: " + Globals.Level.ToString();

            this.counter = 5;
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
            this.counter--;

            if (this.counter == 0)
            {

                LycaderEngine.Game.QueueScene(new Scenes.PlayingScreen());
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
        }

        /// <summary>
        /// Implements OnRender
        /// </summary>
        /// <param name="e">event args</param>
        public void Draw(FrameEventArgs e)
        {
            this.text.Blit();
        }
    }
}
