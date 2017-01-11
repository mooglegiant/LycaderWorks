﻿//-----------------------------------------------------------------------
// <copyright file="Player.cs" company="Mooglegiant" >
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
    using Lycader.Graphics.Collision;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// The player sprite
    /// </summary>
    public class Player : Sprite
    {
        /// <summary>
        /// The player's X velocity
        /// </summary>
        private float velocityX;

        /// <summary>
        /// The player's Y velocity
        /// </summary>
        private float velocityY;

        /// <summary>
        /// The player's X thrust
        /// </summary>
        private float thrustX;

        /// <summary>
        /// The player's Y thrust
        /// </summary>
        private float thrustY;

        /// <summary>
        /// The player's time until can fire again
        /// </summary>
        private int fireRate;

        /// <summary>
        /// Initializes a new instance of the Player class
        /// </summary>
        public Player()
            : base()
        {
            this.Rotation = 0;

            this.Animations.Add((int)State.Idle, new Animation(false));
            this.Animations[(int)State.Idle].Add("player", 1);

            this.Animations.Add((int)State.Thrust, new Animation(true));
            this.Animations[(int)State.Thrust].Add("player_thrust1", 5);
            this.Animations[(int)State.Thrust].Add("player_thrust2", 5);

            this.Init();
          
        }

        public enum State { Idle = 0, Thrust = 1 }

        /// <summary>
        /// Gets or sets the player's lives remaining
        /// </summary>
        public int Lives { get; set; }

        /// <summary>
        /// Gets or sets the player's counter to bring alive
        /// </summary>
        public int DeadCounter { get; set; }

        /// <summary>
        /// Rotate the ship to the left
        /// </summary>
        public void PressingLeft()
        {
            this.Rotation += 5;

            if (this.Rotation > 359)
            {
                this.Rotation -= 360;
            }
        }

        /// <summary>
        /// Rotate the ship to the right
        /// </summary>
        public void PressingRight()
        {
            this.Rotation -= 5;           
            if (this.Rotation < 0)
            {
                this.Rotation += 360;
            }
        }

        /// <summary>
        /// Apply thrust
        /// </summary>
        public void PressingUp(bool pressingUp)
        {
            if (pressingUp)
            {
                this.thrustX = (float)Math.Cos((double)(this.Rotation * (Math.PI / 180)));
                this.thrustY = (float)Math.Sin((double)(this.Rotation * (Math.PI / 180)));

                AudioContent.Find("thrust").Play();
                this.CurrentAnimation = (int)State.Thrust;
            }
            else
            {
                AudioContent.Find("thrust").Stop();
                this.CurrentAnimation = (int)State.Idle;
            }
        }

        /// <summary>
        /// Fires a bullet
        /// </summary>
        /// <param name="isKeyDown">Indicates if the key is being pressed</param>
        public bool Fire(bool isKeyDown)
        {
            if (this.fireRate <= 0 && isKeyDown)
            {
                // Time to wait until firing again
                this.fireRate = 60;

                return true;
            }
            else if (isKeyDown)
            {
                this.fireRate--;
            }
            else
            {
                if (this.fireRate > 0)
                {
                    // Allow faster fire rate if button is tapped vs held
                    this.fireRate -= 5;
                }
            }

            return false;
        }

        /// <summary>
        /// Updates the sprite
        /// </summary>
        public override void Update()
        {
            this.Animations[this.CurrentAnimation].Animate();
            this.Texture = this.Animations[this.CurrentAnimation].GetTexture();

            this.ApplyVelocity();
            Helper.ScreenWrap(this);

            this.Position += new Vector3(this.velocityX / 5, this.velocityY / 5, 0);
        }

        public override void Draw(Camera camera)
        {
            if (this.DeadCounter % 2 == 0)
            {
                base.Draw(camera);
            }
        }

        /// <summary>
        /// Ship crashed into an asteroid
        /// </summary>
        public void Crash()
        {          
            this.Lives--;

            if (this.Lives == 0)
            {
                LycaderEngine.ChangeScene(new Scenes.GameOver());
            }

            this.DeadCounter = 100;
        }

        /// <summary>
        /// Initializes all the settings for the player
        /// </summary>
        public void Init()
        {
            this.CurrentAnimation = (int)State.Idle;
            this.Texture = this.Animations[this.CurrentAnimation].GetTexture();

            this.Position = new Vector3((LycaderEngine.Resolution.Width / 2) - (Texture.Width / 2), (LycaderEngine.Resolution.Height / 2) - (Texture.Height / 2), 2);

            this.velocityX = 0;
            this.velocityY = 0;
            this.thrustX = 0;
            this.thrustY = 0;
            this.fireRate = 0;
            this.Lives = 3;
        }

        public void Collision(List<IEntity> entities)
        {
            foreach (Bullet bullet in entities.OfType<Bullet>())
            {
                if (!bullet.IsDeleted)
                {
                    if (Collision2D.IsColliding(bullet.Texture.GetTextureCollision(bullet.Position), this.Texture.GetTextureCollision(this.Position)))
                    {
                        bullet.IsDeleted = true;
                        Crash();
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Apply velocity to the player
        /// </summary>
        private void ApplyVelocity()
        {
            this.velocityX += this.thrustX;
            this.velocityY += this.thrustY;

            // Clear thrust
            this.thrustX = 0f;
            this.thrustY = 0f;

            this.velocityX = MathHelper.Clamp(this.velocityX, -50f, 50f);
            this.velocityY = MathHelper.Clamp(this.velocityY, -50f, 50f);

            // Move Velocities closer to zero
            if (this.velocityX < 0)
            {
                this.velocityX += .2f;
            }
            else if (this.velocityX > 0)
            {
                this.velocityX -= .2f;
            }

            if (this.velocityY < 0)
            {
                this.velocityY += .2f;
            }
            else if (this.velocityY > 0)
            {
                this.velocityY -= .2f;
            }
        }
    }
}
