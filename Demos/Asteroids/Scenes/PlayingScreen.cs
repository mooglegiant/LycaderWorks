﻿//-----------------------------------------------------------------------
// <copyright file="PlayingScreen.cs" company="Mooglegiant" >
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
    public class PlayingScreen : IScene
    {
        /// <summary>
        /// The screens text class
        /// </summary>
        private SpriteFont font;

        private SceneManager manager = new SceneManager();

        /// <summary>
        /// Counter to switch from playing screen to level screen when all asteroids are destroyed
        /// </summary>
        private int counter;

        /// <summary>
        /// Initializes a new instance of the PlayingScreen class
        /// </summary>
        public PlayingScreen()
        {       
        }

        public void Load()
        {


            this.font = new SpriteFont(TextureContent.Get("font"), 15);
            this.font.Y = LycaderEngine.ScreenHeight - 15;
            this.counter = 0;

            for (int i = 0; i < Globals.Level + 4; i++)
            {
                manager.Add(new Asteroid(new Random().Next(3, 5), 2));
                System.Threading.Thread.Sleep(100);
            }

            manager.Add(new Player());
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

            if (manager.Entities.OfType<Asteroid>().Count() == 0)
            {
                this.counter++;

                if (this.counter == 100)
                {
                    Globals.Level++;
                    Globals.Score += 5000;
                    LycaderEngine.Game.QueueScene(new Scenes.LevelStart());
                }
            }

            manager.Update();

            foreach (Player player in manager.Entities.OfType<Player>())
            {

                if (player.DeadCounter == 0)
                {
                    if (KeyboardHelper.IsKeyPressed(Key.Left))
                    {
                        player.PressingLeft();
                    }

                    if (KeyboardHelper.IsKeyPressed(Key.Right))
                    {
                        player.PressingRight();
                    }

                    if (KeyboardHelper.IsKeyPressed(Key.Up))
                    {
                        player.PressingUp();
                    }

                    if (player.Fire(KeyboardHelper.IsKeyPressed(Key.Space)))
                    {
                        Bullet bullet = new Bullet(
                            player.CenterX,
                            player.CenterY,
                            (float)Math.Cos((double)(player.ShipRotation * (Math.PI / 180))),
                            (float)Math.Sin((double)(player.ShipRotation * (Math.PI / 180))));

                        manager.Add(bullet);
                        SoundPlayer.PlaySound("sound");
                    }
                }
                else
                {
                    player.DeadCounter--;
                }
            }

            foreach (Asteroid asteroid in manager.Entities.OfType<Asteroid>())
            {
                asteroid.Collision(manager.Entities.OfType<Bullet>().ToList());
                asteroid.Collision(manager.Entities.OfType<Player>().ToList());

                if (asteroid.IsDeleted && asteroid.Size > 1)
                {
                    manager.Add(new Asteroid(asteroid.Size - 1, asteroid.Speed * 1.3f, asteroid.CenterX, asteroid.CenterY));
                    System.Threading.Thread.Sleep(1);
                    manager.Add(new Asteroid(asteroid.Size - 1, asteroid.Speed * 1.3f, asteroid.CenterX, asteroid.CenterY));
                }
            }
           
            this.font.Text = "Score: " + Globals.Score.ToString("d7");
            this.font.Text += " Asteroids: " + manager.Entities.OfType<Asteroid>().Count().ToString("d2");

            foreach (Player player in manager.Entities.OfType<Player>())
            {
                this.font.Text += " Lives: " + player.Lives.ToString("d2");
            }
        }

        /// <summary>
        /// Implements OnRender
        /// </summary>
        /// <param name="e">event args</param>
        public void Draw(FrameEventArgs e)
        {
            manager.Render();
            this.font.Blit();
        }
    }
}
