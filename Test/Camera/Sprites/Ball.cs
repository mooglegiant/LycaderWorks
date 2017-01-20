
namespace CameraTest.Sprites
{
    using System;

    using OpenTK;
    using Lycader;
    using Lycader.Graphics;

    public class Ball : Sprite
    {
        public Ball()
        {
            this.Position = new Vector3(0f, 0f, 0f);
            this.Texture = TextureManager.Find("ball");
        }

        /// <summary>
        /// Updates the sprite
        /// </summary>
        public override void Update()
        {            
        }
    }
}
