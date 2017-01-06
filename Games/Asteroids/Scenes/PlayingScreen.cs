//-----------------------------------------------------------------------
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
        private SpriteFont score;

        private SceneManager manager = new SceneManager();

        private int songTimer = 60;
        private Random random = new Random(2);

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
            this.score = new SpriteFont(TextureContent.Get("font"), 20, new Vector3(20, LycaderEngine.Game.Height - 25, 100), Globals.Score.ToString("d7"));
            this.counter = 0;

          
            for (int i = 0; i < Globals.Level + 4; i++)
            {
                manager.Add(new Asteroid(random.Next(1, 4), random.Next(1, 4)));
                System.Threading.Thread.Sleep(10);
            }

            manager.Add(new Player());
            manager.Add(new Background());
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

            manager.Update();

            if (manager.Entities.OfType<Asteroid>().Count() == 0)
            {
                this.counter++;

                if (this.counter == 100)
                {
                    Globals.Level++;
                    Globals.Score += 5000;
                    LycaderEngine.ChangeScene(new Scenes.Preloader());
                }
            }

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

                    player.PressingUp(KeyboardHelper.IsKeyPressed(Key.Up)); 

                    if (player.Fire(KeyboardHelper.IsKeyPressed(Key.Space)))
                    {
                        Bullet bullet = new Bullet(
                            player.Center,
                            (float)Math.Cos((double)(player.Rotation * (Math.PI / 180))),
                            (float)Math.Sin((double)(player.Rotation * (Math.PI / 180))));

                        manager.Add(bullet);
                        SoundPlayer.PlaySound("sound");
                    }

                    if (player.Fire(KeyboardHelper.IsKeyPressed(Key.V)))
                    {
                        for (int i = 0; i < 360; i += 10)
                        {
                            Bullet bullet = new Bullet(
                                player.Center,
                                (float)Math.Cos((double)(i * (Math.PI / 180))),
                                (float)Math.Sin((double)(i * (Math.PI / 180))));

                            manager.Add(bullet);
                        }

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
                    manager.Add(new Asteroid(asteroid.Size - 1, asteroid.Speed * 1.3f, asteroid.Center));
                    System.Threading.Thread.Sleep(2);
                    manager.Add(new Asteroid(asteroid.Size - 1, asteroid.Speed * 1.3f, asteroid.Center));
                }

                if (asteroid.IsDeleted)
                {
                    Explosion(asteroid.Center);
                }
            }
           
            this.score.Text = "Score: " + Globals.Score.ToString("d7");
          //  this.score.Text += " Asteroids: " + manager.Entities.OfType<Asteroid>().Count().ToString("d2");

            foreach (Player player in manager.Entities.OfType<Player>())
            {
               // this.score.Text += " Lives: " + player.Lives.ToString("d2");

                if (player.DeadCounter == 100)
                {
                    Explosion(player.Center);
                }
            }

            songTimer--;
            if(songTimer == 0)
            {
                int next = random.Next(1, 3);
                SoundPlayer.PlaySong(string.Format("beat{0}", next), false);             
                songTimer = (manager.Entities.OfType<Asteroid>().Select(x => x.Size).Sum() / 2) * 15;
            }
        }

        private void Explosion(Vector3 position)
        {
            for (int temp = 0; temp < 360; temp += 30)
            {
                Particle particle = new Particle(
                    position,
                    (float)Math.Cos((double)(random.Next(360) * (Math.PI / 180))),
                    (float)Math.Sin((double)(random.Next(360) * (Math.PI / 180))));

                manager.Add(particle);
            }
        }

        /// <summary>
        /// Implements OnRender
        /// </summary>
        /// <param name="e">event args</param>
        public void Draw(FrameEventArgs e)
        {          
            manager.Render();

            foreach (Camera camera in manager.Cameras)
            {
                this.score.Draw(camera);
            }
        }
    }
}
