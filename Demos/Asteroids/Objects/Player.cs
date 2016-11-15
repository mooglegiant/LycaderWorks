//-----------------------------------------------------------------------
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
            this.ShipRotation = 0;
            this.Init();
        }

        /// <summary>
        /// Gets or sets the player's lives remaining
        /// </summary>
        public int Lives { get; set; }

        /// <summary>
        /// Gets or sets the player's rotation
        /// </summary>
        public int ShipRotation { get; set; }

        /// <summary>
        /// Gets or sets the player's counter to bring alive
        /// </summary>
        public int DeadCounter { get; set; }

        /// <summary>
        /// Rotate the ship to the left
        /// </summary>
        public void PressingLeft()
        {
            this.ShipRotation += 5;

            if (this.ShipRotation > 359)
            {
                this.ShipRotation -= 360;
            }
        }

        /// <summary>
        /// Rotate the ship to the right
        /// </summary>
        public void PressingRight()
        {
            this.ShipRotation -= 5;

            if (this.ShipRotation < 0)
            {
                this.ShipRotation += 360;
            }
        }

        /// <summary>
        /// Apply thrust
        /// </summary>
        public void PressingUp()
        {
            this.thrustX = (float)Math.Cos((double)(this.ShipRotation * (Math.PI / 180)));
            this.thrustY = (float)Math.Sin((double)(this.ShipRotation * (Math.PI / 180)));
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
                this.fireRate = 20;

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
            this.Rotation = this.ShipRotation;

            this.ApplyVelocity();
            this.ScreenWrap();

            this.Position += new Vector3(this.velocityX / 5, this.velocityY / 5, 0);
            this.ReduceSpeed();
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
            this.Init();

            this.Lives--;

            if (this.Lives == 0)
            {
                LycaderEngine.Game.QueueScene(new Scenes.GameOver());
            }

            this.DeadCounter = 100;
        }

        /// <summary>
        /// Initializes all the settings for the player
        /// </summary>
        private void Init()
        {
            Texture = TextureContent.Get("ship");
            this.Position = new Vector3((LycaderEngine.ScreenWidth / 2) - (Texture.Width / 2), (LycaderEngine.ScreenHeight / 2) - (Texture.Height / 2), 0);

            this.velocityX = 0;
            this.velocityY = 0;
            this.thrustX = 0;
            this.thrustY = 0;
            this.fireRate = 0;
            this.Lives = 3;
        }

        /// <summary>
        /// Apply velocity to the player
        /// </summary>
        private void ApplyVelocity()
        {
            this.velocityX += this.thrustX;
            this.velocityY += this.thrustY;
        }

        /// <summary>
        /// Screen wrapping the sprite
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

        /// <summary>
        /// Reduces velocity ever so slowly
        /// </summary>
        private void ReduceSpeed()
        {
            // Clear thrust
            this.thrustX = 0f;
            this.thrustY = 0f;

            // Keep velocities in bounds
            if (this.velocityX > 75)
            {
                this.velocityX = 75f;
            }
            else if (this.velocityX < -75)
            {
                this.velocityX = -75f;
            }

            if (this.velocityY > 75)
            {
                this.velocityY = 75f;
            }
            else if (this.velocityY < -75)
            {
                this.velocityY = -75f;
            }

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
