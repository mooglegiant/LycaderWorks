//-----------------------------------------------------------------------
// <copyright file="Asteroid.cs" company="Mooglegiant" >
//      Copyright (c) Mooglegiant. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Asteroids
{
    using System;

    using Lycader;
    using Lycader.Graphics;
    using OpenTK;
    using System.Collections.Generic;

    /// <summary>
    /// Asteroid Sprite
    /// </summary>
    public class Asteroid : Sprite
    {
        /// <summary>
        /// Initializes a new instance of the Asteroid class
        /// </summary>
        /// <param name="size">Size of the asteroid</param>
        /// <param name="speed">Speed of the asteroid</param>
        public Asteroid(int size, float speed)
            : base()
        {
            this.Init(size, speed);
        }

        /// <summary>
        /// Initializes a new instance of the Asteroid class
        /// </summary>
        /// <param name="size">Size of the asteroid</param>
        /// <param name="speed">Speed of the asteroid</param>
        /// <param name="x">Current X location</param>
        /// <param name="y">Current Y location</param>
        public Asteroid(int size, float speed, float x, float y)
            : base()
        {
            this.Init(size, speed);
            this.Position = new OpenTK.Vector3(x, y, 0);
        }

        /// <summary>
        /// Gets or sets the sprite's X angle
        /// </summary>
        public float AngleX { get; set; }

        /// <summary>
        /// Gets or sets the sprite's Y angle
        /// </summary>
        public float AngleY { get; set; }

        /// <summary>
        /// Gets or sets the size of the asteroid
        /// </summary>
        public int Size { get; set; }

        /// <summary>
        /// Gets or sets the speed of the asteroid (force)
        /// </summary>
        public float Speed { get; set; }

        /// <summary>
        /// Gets or sets the rotational speed fo the asteroid
        /// </summary>
        public int RotationSpeed { get; set; }

        /// <summary>
        /// Updates the sprite
        /// </summary>
        public override void Update()
        {
            this.Position += new OpenTK.Vector3(this.AngleX * this.Speed, this.AngleY * this.Speed, 0);
            this.Rotation += this.RotationSpeed;

            if (this.Rotation > 360)
            {
                this.Rotation -= 360;
            }

            this.ScreenWrap();
        }

        public void Collision(List<Bullet> bullets)
        {
            foreach (Bullet bullet in bullets)
            {
                if (!bullet.IsDeleted)
                {
                    if (this.Collide(bullet.Position.X, bullet.Position.Y))
                    {
                        if (IsColliding(bullet))
                        {
                            bullet.IsDeleted = true;
                        }
                    }

                }
            }
        }

        /// <summary>
        /// Does collision checking against a player
        /// </summary>
        /// <param name="player">the player to check against</param>
        /// <returns>a value indicating whether there was a collision or not</returns>
        public void Collision(List<Player> players)
        {
            foreach (Player player in players)
            {
                if (this.Collide(player.Position.X, player.Position.Y))
                {
                    if (player.DeadCounter == 0)
                    {
                        player.Crash();
                    }
                }
            }
        }

        /// <summary>
        /// Initializes the asteroid
        /// </summary>
        /// <param name="size">Current size of the asteroid</param>
        /// <param name="speed">Current speed of the asteroid</param>
        private void Init(int size, float speed)
        {
            Texture = Texture = TextureContent.Get("asteroid" + size.ToString());

            Random random = new Random();

            this.Size = size;
            this.Speed = speed;

            int x, y;

            if (random.Next(1, 3) == 1)
            {
                x = random.Next(-LycaderEngine.ScreenWidth / 3, LycaderEngine.ScreenWidth / 4);
            }
            else
            {
                x = random.Next(3 * (LycaderEngine.ScreenWidth / 4), 3 * (LycaderEngine.ScreenWidth / 3));
            }

            y = random.Next(-(int)Texture.Height, LycaderEngine.ScreenHeight);

            this.Position = new OpenTK.Vector3(x, y, 0);

            this.AngleX = (float)random.NextDouble();

            // Sleep a little since random will likely return the same number otherwise
            System.Threading.Thread.Sleep(10);
            this.AngleY = (float)random.NextDouble();

            if (random.Next(1, 3) == 1)
            {
                this.AngleX *= -1;
            }

            if (random.Next(1, 3) == 1)
            {
                this.AngleY *= -1;
            }

            this.RotationSpeed = random.Next(0, 3);
            this.Rotation = random.Next(0, 359);

            this.IsDeleted = false;
        }

        /// <summary>
        /// Does collision checking against an object
        /// </summary>
        /// <param name="x">object's X coordinate</param>
        /// <param name="y">object's Y coordinate</param>
        /// <returns>a value indicating whether there was a collision or not</returns>
        private bool Collide(float x, float y)
        {
            if (x > this.Position.X + (3 * this.Size) && x < this.Position.X + Texture.Width - (3 * this.Size))
            {
                if (y > this.Position.Y + (3 * this.Size) && y < this.Position.Y + Texture.Height - (3 * this.Size))
                {
                    this.IsDeleted = true;

                    if (this.Size == 1)
                    {
                        return true;
                    }

                    Globals.Score += 1000;
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Warps the sprite to other side of screen
        /// </summary>
        private void ScreenWrap()
        {
            if (this.Position.X < -Texture.Width)
            {
                this.Position += new Vector3(LycaderEngine.ScreenWidth + Texture.Width, 0, 0);
            }
            else if (this.Position.X > LycaderEngine.ScreenWidth)
            {
                this.Position -= new Vector3(LycaderEngine.ScreenWidth + Texture.Width, 0, 0);
            }

            if (this.Position.Y < -Texture.Height)
            {
                this.Position += new Vector3(0, LycaderEngine.ScreenHeight + Texture.Height, 0);
            }
            else if (this.Position.Y > LycaderEngine.ScreenHeight)
            {
                this.Position -= new Vector3(0, LycaderEngine.ScreenHeight + Texture.Height, 0);
            }
        }
    }
}
