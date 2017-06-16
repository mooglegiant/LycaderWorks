//-----------------------------------------------------------------------
// <copyright file="LevelScreen.cs" company="Mooglegiant" >
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
    using System.Linq;

    /// <summary>
    /// Level screen
    /// </summary>
    public class LevelScreen : IScene
    {
        private EntityManager manager = new EntityManager();
        private OggStream stream;

        private float levelTimer = 0;
        string audioFile = "Assets/Music/song1.ogg";

        /// <summary>
        /// Initializes a new instance of the TitleScreen class
        /// </summary>
        public LevelScreen()
        {       

        }

        public void Load()
        {
            var background = new SpriteEntity()
            {
                Texture = "background.gif"
            };

            this.manager.Add(background);

            this.manager.Add(new Ball());
            this.manager.Add(new HUD());
           
            MusicManager.Add(audioFile);
            MusicManager.Find(audioFile).Play();
        }

        public void Unload()
        {
            Globals.Scores.Save();
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
                Globals.IsPaused = !Globals.IsPaused;

                if (Globals.IsPaused)
                {
                    MusicManager.Find(audioFile).Pause();
                }
                else
                {
                    MusicManager.Find(audioFile).Resume();
                }
            }
        
            if (InputManager.IsKeyPressed(Key.Escape))
            {
                Engine.Screen.Exit();
            }


            if (InputManager.IsKeyPressed(Key.Q))
            {
                Globals.LevelUp();
            }

            if (!Globals.IsPaused)
            {
                this.manager.Update();

                foreach (Ball ball in this.manager.Entities.OfType<Ball>())
                {
                    foreach (BlockRow row in this.manager.Entities.OfType<BlockRow>())
                    {
                        row.CheckCollision(ball);
                    }
                }

                if (this.manager.Entities.OfType<Ball>().Count() == 0)
                {
                    MusicManager.Unload();
                   SceneManager.ChangeScene(new GameoverScreen());
                }
            }

            this.levelTimer -= Globals.BlockSpeed;
            if (levelTimer < 0)
            {
                this.levelTimer = 100;
                this.manager.Add(new BlockRow(new Vector3(0, -10, 10)));
            }
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
