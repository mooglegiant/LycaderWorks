//-----------------------------------------------------------------------
// <copyright file="Bullet.cs" company="Mooglegiant" >
//      Copyright (c) Mooglegiant. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Asteroids
{
    using Lycader;
    using Lycader.Entities;
    using OpenTK;

    /// <summary>
    /// A bullet class
    /// </summary>
    public class Bullet : SpriteEntity
    {
        private int timer = 50;

        public float Speed = 10;

        public Vector3 Direction;

        public string Owner;

        /// <summary>
        /// Initializes a new instance of the Bullet class
        /// </summary>
        /// <param name="position">Current world position</param>
        /// <param name="angleX">Angle of X</param>
        /// <param name="angleY">Angle of Y</param>
        public Bullet(string owner, Vector3 position, Vector3 direction)
            : base()
        {
            Texture = TextureManager.Find("bullet");

            this.Owner = owner;
            this.Position = position;
            this.Direction = direction;
        }

        /// <summary>
        /// Updates the sprite
        /// </summary>
        public override void Update()
        {
            this.Position += (Direction * Speed);
            this.timer--;

            if (this.timer <= 0)
            {
                this.IsDeleted = true;
            }

            // Kill at screen border
            //if (this.Position.X > LycaderEngine.Resolution.Width)
            //{
            //    this.IsDeleted = true;
            //}
            //else if (this.Position.X < Texture.Width)
            //{
            //    this.IsDeleted = true;
            //}
            //else if (this.Position.Y > LycaderEngine.Resolution.Height)
            //{
            //    this.IsDeleted = true;
            //}
            //else if (this.Position.Y < Texture.Height)
            //{
            //    this.IsDeleted = true;
            //}

            Helper.ScreenWrap(this);
        }
    }
}
