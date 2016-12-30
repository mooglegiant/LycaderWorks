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
    using Lycader.Audio;
    using Lycader.Graphics.Collision;

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
        /// <param name="position">Current position location</param>
        public Asteroid(int size, float speed, Vector3 position)
            : base()
        {
            this.Init(size, speed);
            this.Position = position;
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
                    if (Collision2D.IsColliding(bullet.Texture.GetTextureCollision(bullet.Position), new CircleCollidable(new Vector2(this.Center.X, this.Center.Y), this.Texture.Width / 2)))                   
                    {
                        bullet.IsDeleted = true;
                        Collided();
                        break;
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
                if (Collision2D.IsColliding(player.Texture.GetTextureCollision(player.Position), new CircleCollidable(new Vector2(this.Center.X, this.Center.Y), (this.Texture.Width / 2) - 5)))
                {
                    if (player.DeadCounter == 0)
                    {
                       // player.Crash();
                        //Collided();
                        break;
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
                x = random.Next(-LycaderEngine.Resolution.Width / 3, LycaderEngine.Resolution.Width / 4);
            }
            else
            {
                x = random.Next(3 * (LycaderEngine.Resolution.Width / 4), 3 * (LycaderEngine.Resolution.Width / 3));
            }

            y = random.Next(-(int)Texture.Height, LycaderEngine.Resolution.Height);

            this.Position = new OpenTK.Vector3(x, y, 1);

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

        private void Collided()
        {
            if (this.Size >= 3) { SoundPlayer.PlaySound("bangLarge"); }
            else if (this.Size == 2) { SoundPlayer.PlaySound("bangMedium"); }
            else { SoundPlayer.PlaySound("bangSmall"); }

            this.IsDeleted = true;
            Globals.Score += (1000 * this.Size);
        }

        /// <summary>
        /// Warps the sprite to other side of screen
        /// </summary>
        private void ScreenWrap()
        {
            if (this.Position.X < -Texture.Width)
            {
                this.Position += new Vector3(LycaderEngine.Resolution.Width + Texture.Width, 0, 0);
            }
            else if (this.Position.X > LycaderEngine.Resolution.Width)
            {
                this.Position -= new Vector3(LycaderEngine.Resolution.Width + Texture.Width, 0, 0);
            }

            if (this.Position.Y < -Texture.Height)
            {
                this.Position += new Vector3(0, LycaderEngine.Resolution.Height + Texture.Height, 0);
            }
            else if (this.Position.Y > LycaderEngine.Resolution.Height)
            {
                this.Position -= new Vector3(0, LycaderEngine.Resolution.Height + Texture.Height, 0);
            }
        }
    }
}
