//----------------------------------------------------------------------
// <copyright file="Ball.cs" company="Mooglegiant" >
//      Copyright (c) Mooglegiant. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace CameraTest.Sprites
{
    using OpenTK;
    using Lycader.Entities;

    public class Ball : SpriteEntity
    {
        public Ball()
            : base()
        {
            this.Position = new Vector3(0f, 0f, 1f);
            this.Texture = "ball";
        }

        /// <summary>
        /// Updates the sprite
        /// </summary>
        public override void Update()
        {            
        }
    }
}
