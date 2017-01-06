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
    public class Particle : Sprite
    {

        private int life = 10;

        /// <summary>
        /// Initializes a new instance of the Bullet class
        /// </summary>
        /// <param name="position">Current world position</param>
        /// <param name="angleX">Angle of X</param>
        /// <param name="angleY">Angle of Y</param>
        public Particle(Vector3 position, float angleX, float angleY)
            : base()
        {
            Texture = TextureContent.Get("bullet");

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
            this.Position += new OpenTK.Vector3(this.AngleX * 3, this.AngleY * 3, 0);

            life--;

            if (life == 0)
            {
                this.IsDeleted = true;
            }
        }
    }
}
