//-----------------------------------------------------------------------
// <copyright file="PlayingScreen.cs" company="Mooglegiant" >
//      Copyright (c) Mooglegiant. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Asteroids.Scenes
{
    using System;
    using System.Linq;

    using OpenTK;
    using OpenTK.Input;

    using Lycader;
    using Lycader.Audio;
    using Lycader.Entities;

    /// <summary>
    /// Playing screen
    /// </summary>
    public class PlayingScreen : IScene
    {
        private EntityManager manager = new EntityManager();

        private int songTimer = 60;
        private int shipTimer = 200;
        private int beep = 1;
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
                this.manager.Add(new Asteroid(this.random.Next(1, 4), this.random.Next(1, 4)));
                System.Threading.Thread.Sleep(10);
            }

            this.manager.Add(new Player());
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
            if (InputManager.IsKeyPressed(Key.Escape))
            {
                Engine.Screen.Exit();
            }

            if (InputManager.IsKeyPressed(Key.Q))
            {               
                foreach (Player player in this.manager.Entities.OfType<Player>())
                {
                    this.manager.Add(new Ship(player.Position));
                }
            }

            this.manager.Update();

            if (this.manager.Entities.OfType<Asteroid>().Count() == 0)
            {
                this.counter++;

                if (this.counter == 100)
                {
                    Globals.Level++;
                   SceneManager.ChangeScene(new Scenes.LevelScreen());
                }
            }

            foreach (Player player in this.manager.Entities.OfType<Player>())
            {
                if (player.DeadCounter == 0)
                {
                    if (InputManager.IsKeyDown(Key.Left))
                    {
                        player.Rotation += 5;
                    }

                    if (InputManager.IsKeyDown(Key.Right))
                    {
                        player.Rotation -= 5;
                    }

                    player.PressingUp(InputManager.IsKeyDown(Key.Up)); 

                    if (player.Fire(InputManager.IsKeyPressed(Key.Space)))
                    {
                         Bullet bullet = new Bullet(
                            "player",
                            player.Center,
                            new Vector3(
                                (float)Math.Cos((double)(player.Rotation * (Math.PI / 180))),
                                (float)Math.Sin((double)(player.Rotation * (Math.PI / 180))),
                                0));

                        this.manager.Add(bullet);
                        SoundManager.Find("boop.wav").Play();
                    }

                    if (player.Fire(InputManager.IsKeyPressed(Key.V)))
                    {
                        for (int i = 0; i < 360; i += 10)
                        {
                            Bullet bullet = new Bullet(
                                "player",
                                player.Center,
                                new Vector3(
                                    (float)Math.Cos((double)(i * (Math.PI / 180))),
                                    (float)Math.Sin((double)(i * (Math.PI / 180))),
                                    0));

                            this.manager.Add(bullet);
                        }

                        SoundManager.Find("boop.wav").Play();
                    }
                }
                else
                {
                    player.DeadCounter--;
                }
            }

            foreach (Asteroid asteroid in this.manager.Entities.OfType<Asteroid>())
            {
                asteroid.Collision(this.manager.Entities);

                if (asteroid.IsDeleted && asteroid.Size > 1)
                {
                    this.manager.Add(new Asteroid(asteroid.Size - 1, asteroid.Speed * 1.3f, asteroid.Center));
                    System.Threading.Thread.Sleep(2);
                    this.manager.Add(new Asteroid(asteroid.Size - 1, asteroid.Speed * 1.3f, asteroid.Center));
                }

                if (asteroid.IsDeleted)
                {
                    this.Explosion(asteroid.Center);
                }
            }

            foreach (Player player in this.manager.Entities.OfType<Player>())
            {
                player.Collision(this.manager.Entities);

                if (player.DeadCounter == 100)
                {
                    this.Explosion(player.Center);
                    player.Init();
                }

                this.shipTimer--;
                if (this.shipTimer == 0)
                {
                    this.manager.Add(new Ship(player.Position));
                }
            }

            foreach (Ship ship in this.manager.Entities.OfType<Ship>())
            {
                foreach (Player player in this.manager.Entities.OfType<Player>())
                {
                    ship.Fire(player.Position, ref manager);
                }

                ship.Collision(this.manager.Entities);

                if (ship.IsDeleted)
                {
                    this.Explosion(ship.Center);
                }
            }

            this.songTimer--;
            if (this.songTimer == 0)
            {
                this.beep = Lycader.Math.Calculate.Wrap(this.beep + 1, 1, 2);
                SoundManager.Find(string.Format("beat{0}.wav", this.beep)).Play();
                this.songTimer = ((this.manager.Entities.OfType<Asteroid>().Select(x => x.Size).Sum() / 2) + 1) * 15;
            }
        }

        private void Explosion(Vector3 position)
        {
            for (int temp = 0; temp < 360; temp += 30)
            {
                Particle particle = new Particle(
                    position,
                    (float)Math.Cos((double)(this.random.Next(360) * (Math.PI / 180))),
                    (float)Math.Sin((double)(this.random.Next(360) * (Math.PI / 180))));

                this.manager.Add(particle);
            }
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
