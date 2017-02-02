
namespace CameraTest.Sprites
{
    using System;

    using OpenTK;
    using Lycader;
    using Lycader.Entities;

    public class Ball : SpriteEntity
    {
        public Ball()
            :base()
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
