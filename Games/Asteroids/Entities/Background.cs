//-----------------------------------------------------------------------
// <copyright file="Background.cs" company="Mooglegiant" >
//      Copyright (c) Mooglegiant. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Asteroids
{
    using Lycader.Entities;

    /// <summary>
    /// Draws a background of stars
    /// </summary>
    public class Background : SpriteEntity
    {
        /// <summary>
        /// Initializes a new instance of the Background class.
        /// </summary>
        public Background()
        {
            this.Texture = "background";
            this.Position = new OpenTK.Vector3(0, -424, 0);
        }
    }
}
