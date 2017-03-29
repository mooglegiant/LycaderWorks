//-----------------------------------------------------------------------
// <copyright file="Asteroid.cs" company="Mooglegiant" >
//      Copyright (c) Mooglegiant. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Asteroids
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using OpenTK;

    using Lycader;
    using Lycader.Audio;
    using Lycader.Collision;
    using Lycader.Entities;
    using Lycader.Graphics;

    /// <summary>
    /// Asteroid Sprite
    /// </summary>
    public class Asteroid : SpriteEntity
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
        /// Gets or sets the rotational speed for the asteroid
        /// </summary>
        public int RotationSpeed { get; set; }

        /// <summary>
        /// Updates the sprite
        /// </summary>
        public override void Update()
        {
            this.Position += new OpenTK.Vector3(this.AngleX * this.Speed, this.AngleY * this.Speed, 0);
            this.Rotation += this.RotationSpeed;

            Helper.ScreenWrap(this);
        }

        public void Collision(List<IEntity> entities)
        {
            foreach (Bullet bullet in entities.OfType<Bullet>().Where(x => x.Owner == "player"))
            {
                if (!bullet.IsDeleted)
                {
                    if (Collision2D.IsColliding(bullet.GetTextureInfo().GetTextureCollision(bullet.Position), new CircleCollidable(new Vector2(this.Position.X, this.Position.Y), this.GetTextureInfo().Width / 2)))                   
                    {
                        bullet.IsDeleted = true;
                        this.Collided();
                        break;
                    }
                }
            }

            foreach (Player player in entities.OfType<Player>())
            {
                if (Collision2D.IsColliding(player.GetTextureInfo().GetTextureCollision(player.Position), new CircleCollidable(new Vector2(this.Position.X, this.Position.Y), (this.GetTextureInfo().Width / 2) - 7)))
                {
                    if (player.DeadCounter == 0)
                    {
                        player.Crash();
                        this.Collided();
                        break;
                    }
                    else
                    {
                        player.DeadCounter += 2;
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
            Random random = new Random();

            this.Texture = string.Format("asteroid{0}-{1}", size.ToString(), random.Next(1, 4));

            this.Size = size;
            this.Speed = speed;

            int x, y;

            if (random.Next(1, 3) == 1)
            {
                x = random.Next(-Engine.Resolution.Width / 3, Engine.Resolution.Width / 4);
            }
            else
            {
                x = random.Next(3 * (Engine.Resolution.Width / 4), 3 * (Engine.Resolution.Width / 3));
            }

            y = random.Next(-(int)TextureManager.Find(this.Texture).Height, Engine.Resolution.Height);

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
            if (this.Size >= 3) {
                SoundManager.Find("bangLarge.wav").Play();
                Globals.Score += 20;
            }
            else if (this.Size == 2) {
                SoundManager.Find("bangMedium.wav").Play();
                Globals.Score += 50;
            }
            else {
                SoundManager.Find("bangSmall.wav").Play();
                Globals.Score += 100;
            }

            this.IsDeleted = true;            
        }
    }
}
