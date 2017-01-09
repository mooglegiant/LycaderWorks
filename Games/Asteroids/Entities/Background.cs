//-----------------------------------------------------------------------
// <copyright file="Background.cs" company="Mooglegiant" >
//      Copyright (c) Mooglegiant. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Asteroids
{
    using Lycader;
    using Lycader.Graphics;

    /// <summary>
    /// Draws a background of stars
    /// </summary>
    public class Background : Sprite
    {
        /// <summary>
        /// Initializes static members of the Background class.
        /// </summary>
        public Background()
        {
            this.Texture = TextureContent.Find("background");
            this.Position = new OpenTK.Vector3(0, -424, 0);
        }
    }
}
