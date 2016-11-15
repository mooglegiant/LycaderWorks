//-----------------------------------------------------------------------
// <copyright file="GameOver.cs" company="Mooglegiant" >
//      Copyright (c) Mooglegiant. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Asteroids.Scenes
{
    using Lycader;
    using Lycader.Graphics;
    using Lycader.Input;
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

        /// <summary>
        /// Initializes a new instance of the GameOver class
        /// </summary>
        public GameOver()
        {
        }

        public void Load()
        {
            this.gameOver = new SpriteFont(TextureContent.Get("font"), 75);
            this.note = new SpriteFont(TextureContent.Get("font"), 20);

            this.gameOver.X = 200;
            this.gameOver.Y = 200;
            this.gameOver.Text = "Game Over";

            this.note.X = 250;
            this.note.Y = 400;
            this.note.Text = "Press ENTER for new game";
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
            if (KeyboardHelper.IsKeyPressed(Key.Enter))
            {
                Globals.NewGame();
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
            this.gameOver.Blit();
            this.note.Blit();
        }
    }
}
