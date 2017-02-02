//-----------------------------------------------------------------------
// <copyright file="TitleScreen.cs" company="Mooglegiant" >
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
    public class TitleScreen : IScene
    {
        /// <summary>
        /// The screens text class
        /// </summary>
        private SpriteFont pressStart;

        private SceneManager manager = new SceneManager();
        private Random random = new Random(2);

        /// <summary>
        /// Initializes a new instance of the PlayingScreen class
        /// </summary>
        public TitleScreen()
        {       
        }

        public void Load()
        {
            this.pressStart = new SpriteFont("font", 40, new Vector3(270, 300, 100), "Press Start");
            manager.Add(this.pressStart);

            for (int i = 0; i < 10; i++)
            {
                manager.Add(new Asteroid(random.Next(1, 4), random.Next(2, 6)));
                System.Threading.Thread.Sleep(10);
            }

            manager.Add(new Background());
            manager.Add(new HUD());
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
                Globals.Level = 1;
                Globals.Score = 0;
                Globals.Lives = 3;

                LycaderEngine.ChangeScene(new LevelScreen());
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
