//-----------------------------------------------------------------------
// <copyright file="GameOver.cs" company="Mooglegiant" >
//      Copyright (c) Mooglegiant. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Asteroids.Scenes
{
    using Lycader;
    using Lycader.Graphics;
    using OpenTK;
    using OpenTK.Input;

    /// <summary>
    /// Game Over screenlet
    /// </summary>
    public class GameOver : IScene
    {
        /// <summary>
        /// Gameover text
        /// </summary>
        private SpriteFont gameOver;

        /// <summary>
        /// Display a text note
        /// </summary>
        private SpriteFont note;


        private Camera camera = new Camera();

        /// <summary>
        /// Initializes a new instance of the GameOver class
        /// </summary>
        public GameOver()
        {
        }

        public void Load()
        {
            this.gameOver = new SpriteFont(TextureContent.Find("font"), 75, new Vector3(200, 200, 100), "Game Over");
            this.note = new SpriteFont(TextureContent.Find("font"), 20, new Vector3(250, 400, 100), "Press ENTER for new game");
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
            if (InputHelper.IsKeyPressed(Key.Enter))
            {
                Globals.NewGame();
            }


            if (InputHelper.IsKeyPressed(Key.Escape))
            {
                LycaderEngine.Game.Exit();
            }

            if (InputHelper.IsKeyPressed(Key.F11))
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
            this.gameOver.Draw(camera);
            this.note.Draw(camera);
        }
    }
}
