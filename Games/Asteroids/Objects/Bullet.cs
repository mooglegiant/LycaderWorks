﻿//-----------------------------------------------------------------------
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
        /// <summary>
        /// Initializes a new instance of the Bullet class
        /// </summary>
        /// <param name="position">Current world position</param>
        /// <param name="angleX">Angle of X</param>
        /// <param name="angleY">Angle of Y</param>
        public Bullet(Vector3 position, float angleX, float angleY)
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
            this.Position += new OpenTK.Vector3(this.AngleX * 10, this.AngleY * 10, 0);

            if (this.Position.X > LycaderEngine.Game.Width)
            {
                this.IsDeleted = true;
            }
            else if (this.Position.X < Texture.Width)
            {
                this.IsDeleted = true;
            }
            else if (this.Position.Y > LycaderEngine.Game.Height)
            {
                this.IsDeleted = true;
            }
            else if (this.Position.Y < Texture.Height)
            {
                this.IsDeleted = true;
            }
        }
    }
}