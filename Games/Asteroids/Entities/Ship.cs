//-----------------------------------------------------------------------
// <copyright file="Ship.cs" company="Mooglegiant" >
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
    using Lycader.Math;

    /// <summary>
    /// The ship sprite
    /// </summary>
    public class Ship : SpriteEntity
    {
        private int timer = 0;
        private int xSpeed = 0;
        private int thrustY = 0;

        /// <summary>
        /// Initializes a new instance of the Ship class
        /// </summary>
        public Ship(Vector3 playerPosition)
            : base()
        {
            this.Rotation = 0;
            this.Texture = "ship";

            int spawnX, spawnY;

            if (playerPosition.X > Engine.Resolution.Width / 2)
            {
                spawnX = Engine.Resolution.Width;
                this.xSpeed = -4;
            }
            else
            {
                spawnX = -32;
                this.xSpeed = 4;
            }

            if (playerPosition.Y < Engine.Resolution.Height / 2)
            {
                spawnY = Engine.Resolution.Height - (Engine.Resolution.Height / 4);
            }
            else
            {
                spawnY = 0 + (Engine.Resolution.Height / 4);
            }

            this.timer = new Random().Next(10, 20);
            this.Position = new Vector3(spawnX, spawnY, playerPosition.Z);
        }

        /// <summary>
        /// Updates the sprite
        /// </summary>
        public override void Update()
        {
            this.Position += new Vector3(this.xSpeed, 0, 0);
           
            SoundManager.Find("saucer.wav").Play();

            // Kill at screen border
            if (this.Position.X > Engine.Resolution.Width)
            {
                this.IsDeleted = true;
            }
            else if (this.Position.X < 0 - GetTextureInfo().Width)
            {
                this.IsDeleted = true;
            }

            this.Position += new Vector3(0, this.thrustY, 0);
            Helper.ScreenWrap(this);
        }

        public void Fire(Vector3 playerPosition, ref EntityManager manager)
        {
            this.timer--;
            if (this.timer == 0)
            {
                // Change Y thrust every fire
                this.thrustY = (new Random().Next(0, 3) - 1) * 3;

                // the calculated angle gives a direct value to find the player. we add a random +5 to it to make it shoot randomly
                // but anytime the random is +0, the bullet will fire directly at the player's co-ordinates
                // so you can't just avoid the bullet by sitting still :) 
                double angle = Calculate.DirectionTo(new Vector2(this.Position.X, this.Position.Y), new Vector2(playerPosition.X, playerPosition.Y));

                angle += new Random().Next(0, 5); 

                Vector2 vec = new Vector2((float)System.Math.Cos((double)angle), (float)System.Math.Sin((double)angle));

                // Better solution for finding directional vector
                // Vector2 norm = Vector2.Normalize(new Vector2(playerPosition.X - this.Position.X, playerPosition.Y - this.Position.Y));
                Bullet bullet = new Bullet("saucer", this.Center, new Vector3(vec.X, vec.Y, 0));

                SoundManager.Find("boop.wav").Play();

                manager.Add(bullet);
                this.timer = 35;
            }
        }

        public void Collision(List<IEntity> entities)
        {
            foreach (Bullet bullet in entities.OfType<Bullet>().Where(x => x.Owner != "saucer"))
            {
                if (!bullet.IsDeleted)
                {
                    if (Collision2D.IsColliding(bullet.GetTextureInfo().GetTextureCollision(bullet.Position), this.GetTextureInfo().GetTextureCollision(this.Position)))
                    {
                        bullet.IsDeleted = true;
                        this.IsDeleted = true;    
                        SoundManager.Find("bangSmall.wav").Play();
                        break;
                    }
                }
            }

            foreach (Player player in entities.OfType<Player>())
            {
                if (Collision2D.IsColliding(player.GetTextureInfo().GetTextureCollision(player.Position), this.GetTextureInfo().GetTextureCollision(this.Position)))
                {
                    if (player.DeadCounter == 0)
                    {
                        player.Crash();
                        this.IsDeleted = true;
                        break;
                    }
                }
            }
        }
    }
}
