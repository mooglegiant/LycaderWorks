//-----------------------------------------------------------------------
// <copyright file="TitleScreen.cs" company="Mooglegiant" >
//      Copyright (c) Mooglegiant. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Asteroids.Scenes
{
    using System;

    using OpenTK;
    using OpenTK.Input;

    using Lycader;
    using Lycader.Entities;

    /// <summary>
    /// Title screen
    /// </summary>
    public class TitleScreen : IScene
    {
        /// <summary>
        /// The screens text class
        /// </summary>
        private FontEntity pressStart;

        private EntityManager manager = new EntityManager();
        private Random random = new Random(2);

        /// <summary>
        /// Initializes a new instance of the TitleScreen class
        /// </summary>
        public TitleScreen()
        {       
        }

        public void Load()
        {
            this.pressStart = new FontEntity("font", 40, new Vector3(240, 300, 100), .75f, "Press Start");
            this.manager.Add(this.pressStart);

            for (int i = 0; i < 10; i++)
            {
                this.manager.Add(new Asteroid(this.random.Next(1, 4), this.random.Next(2, 6)));
                System.Threading.Thread.Sleep(10);
            }

            this.manager.Add(new Background());
            this.manager.Add(new HUD());
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
                Globals.NewGame();
               SceneManager.ChangeScene(new LevelScreen());
            }

            if (InputManager.IsKeyPressed(Key.Escape))
            {
                Engine.Screen.Exit();
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
