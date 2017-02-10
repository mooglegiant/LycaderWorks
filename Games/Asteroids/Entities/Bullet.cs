//-----------------------------------------------------------------------
// <copyright file="Bullet.cs" company="Mooglegiant" >
//      Copyright (c) Mooglegiant. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Asteroids
{
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
        /// <param name="owner">Name of the parent owner</param>
        /// <param name="position">Current world position</param>
        /// <param name="direction">Direction to move</param> 
        public Bullet(string owner, Vector3 position, Vector3 direction)
            : base()
        {
            this.Texture = "bullet";

            this.Owner = owner;
            this.Position = position;
            this.Direction = direction;
        }

        /// <summary>
        /// Updates the sprite
        /// </summary>
        public override void Update()
        {
            this.Position += this.Direction * this.Speed;
            this.timer--;

            if (this.timer <= 0)
            {
                this.IsDeleted = true;
            }

            Helper.ScreenWrap(this);
        }
    }
}
