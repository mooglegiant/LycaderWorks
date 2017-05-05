//-----------------------------------------------------------------------
// <copyright file="GameoverScreen.cs" company="Mooglegiant" >
//      Copyright (c) Mooglegiant. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Falldown.Scenes
{
    using System;

    using OpenTK;
    using OpenTK.Input;

    using Lycader;
    using Lycader.Audio;
    using Lycader.Entities;

    /// <summary>
    /// Gameover screen
    /// </summary>
    public class GameoverScreen : IScene
    {
        /// <summary>
        /// The screens text class
        /// </summary>
        private FontEntity gameOver, totalScore;

        private EntityManager manager = new EntityManager();

        /// <summary>
        /// Initializes a new instance of the TitleScreen class
        /// </summary>
        public GameoverScreen()
        {       
        }

        public void Load()
        {          
            this.gameOver = new FontEntity("pixel.png", 35, new Vector3(170, 250, 100), 1, "Game Over");
            this.manager.Add(this.gameOver);

            this.totalScore = new FontEntity("pixel.png", 20, new Vector3(110, 180, 100), 1, "Final Score: " + Globals.Score.ToString());
            this.manager.Add(this.totalScore);

            MusicManager.Add("Assets/Music/gameover.ogg");
            MusicManager.Find("Assets/Music/gameover.ogg").Play();
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
               MusicManager.Unload();
               SceneManager.ChangeScene(new TitleScreen());               
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
            this.manager.Draw();
        }
    }
}
