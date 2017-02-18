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
        /// <summary>
        /// Initializes a new instance of the Ball class
        /// </summary>
        public Ball()
            : base()
        {
            this.Texture = "ball1.png";
            this.Position = new Vector3(42, 32, 10);
        }

        /// <summary>
        /// Updates the sprite
        /// </summary>
        public override void Update()
        {

            float newY = this.Position.Y;
            float newX = this.Position.X;
            
            // Add gravity
            newY -= (short)(Globals.BallFallSpeed + Globals.ExtraFallSpeed);

            if (InputManager.IsKeyDown(Key.Left))
            {
                this.Rotation += 6 + Globals.ExtraMoveSpeed;

                newX -= (short)(Globals.BallMoveSpeed + Globals.ExtraMoveSpeed);
            }

            if (InputManager.IsKeyDown(Key.Right))
            {
                this.Rotation -= (6 + Globals.ExtraMoveSpeed);

                newX += (short)(Globals.BallMoveSpeed + Globals.ExtraMoveSpeed);
            }

            newY = MathHelper.Clamp(newY, 0, 600);
            newX = MathHelper.Clamp(newX, 42, 492);
            this.Position = new Vector3(newX, newY, this.Position.Z);
        }
    }
}
