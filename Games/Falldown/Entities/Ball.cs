//-----------------------------------------------------------------------
// <copyright file="Ball.cs" company="Mooglegiant" >
//      Copyright (c) Mooglegiant. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Falldown
{
    using Lycader;
    using Lycader.Entities;
    using OpenTK;
    using OpenTK.Input;

    /// <summary>
    /// A Ball class
    /// </summary>
    public class Ball : SpriteEntity
    {
        public float Radius { get; set; }

        public int MetalTimer { get; set; }

        public int ShoeTimer { get; set; }

        /// <summary>
        /// Initializes a new instance of the Ball class
        /// </summary>
        public Ball()
            : base()
        {
            this.Texture = "ball1.png";
            this.Position = new Vector3(LycaderEngine.Resolution.Width / 2, LycaderEngine.Resolution.Height - 32, 10);
            this.Radius = this.GetTextureInfo().Height / 2;
            this.MetalTimer = 0;
            this.ShoeTimer = 0;
        }

        /// <summary>
        /// Updates the sprite
        /// </summary>
        public override void Update()
        {

            float newY = this.Position.Y;
            float newX = this.Position.X;
            
            // Add gravity
            newY -= GetTotalFallSpeed();

            if (InputManager.IsKeyDown(Key.Left))
            {
                this.Rotation += 6;

                newX -= GetTotalMoveSpeed();
            }

            if (InputManager.IsKeyDown(Key.Right))
            {
                this.Rotation -= 6 ;

                newX += GetTotalMoveSpeed();
            }

            newY = MathHelper.Clamp(newY, 0, 600);
            newX = MathHelper.Clamp(newX, 32, 492);
            this.Position = new Vector3(newX, newY, this.Position.Z);

            if (this.Position.Y > LycaderEngine.Resolution.Height + 10)
            {
                this.IsDeleted = true;
            }
        }

        public float GetTotalFallSpeed()
        {
            if (this.MetalTimer > 0)
            {
                return Globals.BallFallSpeed + Globals.ExtraFallSpeed;
            }

            return Globals.BallFallSpeed;
        }

        public float GetTotalMoveSpeed()
        {
            if (this.ShoeTimer > 0)
            {
                return Globals.BallMoveSpeed + Globals.ExtraMoveSpeed;
            }

            return Globals.BallMoveSpeed;
        }
    }
}
