//-----------------------------------------------------------------------
// <copyright file="Player.cs" company="Mooglegiant" >
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
    using Lycader.Collision;
    using Lycader.Entities;

    /// <summary>
    /// The player sprite
    /// </summary>
    public class Player : SpriteEntity
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

            this.Animations.Add((int)AnimationState.Idle, new Animation(false));
            this.Animations[(int)AnimationState.Idle].Add("player", 1);

            this.Animations.Add((int)AnimationState.Thrust, new Animation(true));
            this.Animations[(int)AnimationState.Thrust].Add("player_thrust1", 5);
            this.Animations[(int)AnimationState.Thrust].Add("player_thrust2", 5);

            this.Init();          
        }

        public enum AnimationState { Idle = 0, Thrust = 1 }

        /// <summary>
        /// Gets or sets the player's counter to bring alive
        /// </summary>
        public int DeadCounter { get; set; }

        /// <summary>
        /// Apply thrust
        /// </summary>
        public void PressingUp(bool pressingUp)
        {
            if (pressingUp)
            {
                this.thrustX = (float)Math.Cos((double)(this.Rotation * (Math.PI / 180))) / 5;
                this.thrustY = (float)Math.Sin((double)(this.Rotation * (Math.PI / 180))) / 5;

                SoundManager.Find("thrust.wav").Play();
                this.CurrentAnimation = (int)AnimationState.Thrust;
            }
            else
            {
                this.thrustX = 0;
                this.thrustY = 0;

                SoundManager.Find("thrust.wav").Stop();
                this.CurrentAnimation = (int)AnimationState.Idle;
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
            Globals.Lives--;

            if (Globals.Lives == 0)
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
            this.CurrentAnimation = (int)AnimationState.Idle;
            this.Texture = this.Animations[this.CurrentAnimation].GetTexture();

            this.Position = new Vector3((LycaderEngine.Resolution.Width / 2) - (GetTextureInfo().Width / 2), (LycaderEngine.Resolution.Height / 2) - (GetTextureInfo().Height / 2), 2);

            this.velocityX = 0;
            this.velocityY = 0;
            this.thrustX = 0;
            this.thrustY = 0;
            this.fireRate = 0;
        }

        public void Collision(List<IEntity> entities)
        {
            foreach (Bullet bullet in entities.OfType<Bullet>().Where(x => x.Owner == "saucer"))
            {
                if (!bullet.IsDeleted)
                {
                    if (Collision2D.IsColliding(bullet.GetTextureInfo().GetTextureCollision(bullet.Position), this.GetTextureInfo().GetTextureCollision(this.Position)))
                    {
                        bullet.IsDeleted = true;

                        if (this.DeadCounter == 0)
                        {
                            this.Crash();
                            SoundManager.Find("bangSmall.wav").Play();
                            break;
                        }
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

            this.velocityX = MathHelper.Clamp(this.velocityX, -5f, 5f);
            this.velocityY = MathHelper.Clamp(this.velocityY, -5f, 5f);

            this.velocityX -= this.velocityX * .02f;
            this.velocityY -= this.velocityY * .02f;

            this.Position += new Vector3(this.velocityX, this.velocityY, 0);
        }
    }
}
