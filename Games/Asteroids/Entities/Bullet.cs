//-----------------------------------------------------------------------
// <copyright file="Bullet.cs" company="Mooglegiant" >
//      Copyright (c) Mooglegiant. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Asteroids
{
    using Lycader;
    using Lycader.Graphics;
    using OpenTK;

    /// <summary>
    /// A bullet class
    /// </summary>
    public class Bullet : Sprite
    {
        private int timer = 50;
       
        /// <summary>
        /// Initializes a new instance of the Bullet class
        /// </summary>
        /// <param name="position">Current world position</param>
        /// <param name="angleX">Angle of X</param>
        /// <param name="angleY">Angle of Y</param>
        public Bullet(Vector3 position, float angleX, float angleY)
            : base()
        {
            Texture = TextureContent.Find("bullet");

            this.Position = position;

            this.AngleX = angleX;
            this.AngleY = angleY;
        }

        /// <summary>
        /// Gets or sets the sprite's X angle
        /// </summary>
        public float AngleX { get; set; }

        /// <summary>
        /// Gets or sets the sprite's Y angle
        /// </summary>
        public float AngleY { get; set; }

        /// <summary>
        /// Updates the sprite
        /// </summary>
        public override void Update()
        {
            this.Position += new OpenTK.Vector3(this.AngleX * 10, this.AngleY * 10, 0);
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
