//-----------------------------------------------------------------------
// <copyright file="Ball.cs" company="Mooglegiant" >
//      Copyright (c) Mooglegiant. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace CollsionTest.Sprites
{
    using System;

    using OpenTK;
    using Lycader;
    using Lycader.Entities;
    using Lycader.Collision;

    public class Ball : SpriteEntity
    {
        private Random random = new Random();
        private Vector3 velocity = new Vector3(0, 0, 0);
        private int rotationSpeed = 0;

        public Ball()
        {
            this.velocity = new Vector3(
                (1 + random.Next(2)) * (random.Next(2) == 0 ? -1 : 1),
                (1 + random.Next(2)) * (random.Next(2) == 0 ? -1 : 1),
                0);

            this.rotationSpeed = (1 + random.Next(2)) * (random.Next(2) == 0 ? -1 : 1);

            this.Position = new Vector3(random.Next(LycaderEngine.Screen.Width), random.Next(LycaderEngine.Screen.Height), 0f);
            this.Texture = "ball";
            this.CollisionShape = new CircleCollidable(new Vector2(this.Position.X, this.Position.Y), (this.GetTextureInfo().Width / 2) - 4);
        }

        public Ball(Vector3 position)
        {
            this.Position = position;
            this.Texture = "ball";
            this.CollisionShape = new CircleCollidable(new Vector2(this.Position.X, this.Position.Y), (this.GetTextureInfo().Width / 2) - 4);
        }

        /// <summary>
        /// Updates the sprite
        /// </summary>
        public override void Update()
        {
            this.Position += velocity;
            this.Rotation += this.rotationSpeed;

            this.CollisionShape.Position = new Vector2(this.Position.X, this.Position.Y);
            this.ScreenWrap();
        }

        /// <summary>
        /// Warps the sprite to other side of screen
        /// </summary>
        private void ScreenWrap()
        {
            if (this.Position.X < -GetTextureInfo().Width)
            {
                this.Position += new Vector3(LycaderEngine.Screen.Width + GetTextureInfo().Width, 0f, 0f);     
            }
            else if (this.Position.X > LycaderEngine.Screen.Width)
            {
                this.Position -= new Vector3(LycaderEngine.Screen.Width + GetTextureInfo().Width, 0f, 0f);
            }

            if (this.Position.Y < -GetTextureInfo().Height)
            {
                this.Position += new Vector3(0f, LycaderEngine.Screen.Height + GetTextureInfo().Height, 0f);
            }
            else if (this.Position.Y > LycaderEngine.Screen.Height)
            {
                this.Position -= new Vector3(0f, LycaderEngine.Screen.Height + GetTextureInfo().Height, 0f);         
            }
        }

    }
}
