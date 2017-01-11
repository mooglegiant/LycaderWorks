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
        private SceneManager manager = new SceneManager();

        private int songTimer = 60;
        private int shipTimer = 200;
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
            this.counter = 0;
            for (int i = 0; i < Globals.Level + 4; i++)
            {
                manager.Add(new Asteroid(random.Next(1, 4), random.Next(1, 4)));
                System.Threading.Thread.Sleep(10);
            }

            manager.Add(new Player());
            manager.Add(new Background());
         //   manager.Add(new Ship(new Vector3(100, 100, 1)));
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

            if (KeyboardHelper.IsKeyPressed(Key.Q))
            {               
                foreach (Player player in manager.Entities.OfType<Player>())
                {
                    manager.Add(new Ship(player.Position));
                }
            }

            manager.Update();

            if (manager.Entities.OfType<Asteroid>().Count() == 0)
            {
                this.counter++;

                if (this.counter == 100)
                {
                    Globals.Level++;
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
                        AudioContent.Find("sound").Play();
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

                        AudioContent.Find("sound").Play();
                    }
                }
                else
                {
                    player.DeadCounter--;
                }
            }

            foreach (Asteroid asteroid in manager.Entities.OfType<Asteroid>())
            {
                asteroid.Collision(manager.Entities);

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

            foreach (Player player in manager.Entities.OfType<Player>())
            {
                player.Collision(manager.Entities);


                if (player.DeadCounter == 100)
                {                   
                    Explosion(player.Center);
                    player.Init();
                }

                shipTimer--;
                if (shipTimer == 0)
                {
                    manager.Add(new Ship(player.Position));
                }
            }

            foreach (Ship ship in manager.Entities.OfType<Ship>())
            {
                foreach (Player player in manager.Entities.OfType<Player>())
                {
                    ship.Fire(player.Position, ref manager);
                }
            }

            songTimer--;
            if(songTimer == 0)
            {
                int next = random.Next(1, 3);

                AudioContent.Find(string.Format("beat{0}", next)).Play();                            
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
            Globals.HUDManager.Render();
        }
    }
}
