//-----------------------------------------------------------------------
// <copyright file="Ship.cs" company="Mooglegiant" >
//      Copyright (c) Mooglegiant. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Asteroids
{
    using System;

    using Lycader;
    using Lycader.Audio;
    using Lycader.Graphics;
    using OpenTK;
    using Lycader.Scenes;
    using Lycader.Math;

    /// <summary>
    /// The ship sprite
    /// </summary>
    public class Ship : Sprite
    {
        private int timer = 0;
        private int xSpeed = 0;
        private int thrustY = 0;
        /// <summary>
        /// Initializes a new instance of the Player class
        /// </summary>
        public Ship(Vector3 playerPosition)
            : base()
        {
            this.Rotation = 0;
            this.Texture = TextureContent.Find("ship");

            int spawnX, spawnY;

            if (playerPosition.X > LycaderEngine.Resolution.Width / 2)
            {
                spawnX = LycaderEngine.Resolution.Width;
                xSpeed = -4;
            }
            else
            {
                spawnX = -32;
                xSpeed = 4;
            }

            if (playerPosition.Y < LycaderEngine.Resolution.Height / 2)
            {
                spawnY = LycaderEngine.Resolution.Height - (LycaderEngine.Resolution.Height / 4);
            }
            else
            {
                spawnY = 0 + (LycaderEngine.Resolution.Height / 4);
            }
            timer = new Random().Next(10, 20);

            this.Position = new Vector3(spawnX, spawnY, playerPosition.Z);
        }

        /// <summary>
        /// Updates the sprite
        /// </summary>
        public override void Update()
        {
            this.Position += new Vector3(xSpeed, 0, 0);
           
            AudioContent.Find("saucer").Play();

            // Kill at screen border
            if (this.Position.X > LycaderEngine.Resolution.Width)
            {
                this.IsDeleted = true;
            }
            else if (this.Position.X < 0 - Texture.Width)
            {
                this.IsDeleted = true;
            }

            this.Position += new Vector3(0, thrustY, 0);
            Helper.ScreenWrap(this);
        }

        public void Fire(Vector3 playerPosition, ref SceneManager manager)
        {
            timer--;
            if (timer == 0)
            {
                //Change Y thrust every fire
                thrustY = (new Random().Next(0, 3) - 1) * 3;

                // the calculated angle gives a direct value to find the player. we add a random +5 to it to make it shoot randomly
                // but anytime the random is +0, the bullet will fire directly at the player's co-ordinates
                // so you can't just avoid the bullet by sitting still :) 
                double angle = Calc.DirectionTo(new Vector2(this.Position.X, this.Position.Y), new Vector2(playerPosition.X, playerPosition.Y));

                angle += new Random().Next(0, 5); 
                Vector2 vec = new Vector2((float)System.Math.Cos((double)angle), (float)System.Math.Sin((double)angle));   
                Bullet bullet = new Bullet(this.Center, vec.X, vec.Y);

                AudioContent.Find("sound").Play();

                manager.Add(bullet);
                timer = 35;
            }
        }
    }
}
