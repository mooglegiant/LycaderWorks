//-----------------------------------------------------------------------
// <copyright file="GameOver.cs" company="Mooglegiant" >
//      Copyright (c) Mooglegiant. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Asteroids.Scenes
{
    using Lycader;
    using Lycader.Entities;
    using OpenTK;
    using OpenTK.Input;

    /// <summary>
    /// Game Over screen
    /// </summary>
    public class GameOver : IScene
    {
        /// <summary>
        /// Gameover text
        /// </summary>
        private FontEntity gameOver;

        /// <summary>
        /// Display a text note
        /// </summary>
        private FontEntity note;

        private Camera camera = new Camera();

        /// <summary>
        /// Initializes a new instance of the GameOver class
        /// </summary>
        public GameOver()
        {
        }

        public void Load()
        {
            this.gameOver = new FontEntity("font", 50, new Vector3(210, 200, 100), .75f, "Game Over");
            this.note = new FontEntity("font", 30, new Vector3(120, 400, 100), .75f, "Press ENTER for new game");
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
        }

        /// <summary>
        /// Implements OnRender
        /// </summary>
        /// <param name="e">event args</param>
        public void Draw(FrameEventArgs e)
        {
            this.gameOver.Draw(this.camera);
            this.note.Draw(this.camera);
        }
    }
}
