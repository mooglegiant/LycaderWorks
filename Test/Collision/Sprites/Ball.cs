
namespace CollsionTest.Sprites
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using OpenTK;
    using Lycader;
    using Lycader.Graphics;

    public class Ball : Sprite, IEntity
    {

        private Random Rand = new Random();

        public Ball()
        {
            XSpeed = (1 + Rand.Next(2)) * (Rand.Next(2) == 0 ? -1 : 1);
            YSpeed = (1 + Rand.Next(2)) * (Rand.Next(2) == 0 ? -1 : 1);

            this.Position = new Vector3(Rand.Next(LycaderEngine.Game.Width), Rand.Next(LycaderEngine.Game.Height), 0f);

            this.Texture = TextureContent.Find("ball");
        }

        public int XSpeed;
        public int YSpeed;

        /// <summary>
        /// Updates the sprite
        /// </summary>
        public override void Update()
        {
            this.Position += new Vector3(XSpeed, YSpeed, 0);

            Rotation += XSpeed;

            this.ScreenWrap();
        }

        /// <summary>
        /// Warps the sprite to other side of screen
        /// </summary>
        private void ScreenWrap()
        {
            if (this.Position.X < -Texture.Width)
            {
                this.Position += new Vector3(LycaderEngine.Game.Width + Texture.Width, 0f, 0f);     
            }
            else if (this.Position.X > LycaderEngine.Game.Width)
            {
                this.Position -= new Vector3(LycaderEngine.Game.Width + Texture.Width, 0f, 0f);
            }

            if (this.Position.Y < -Texture.Height)
            {
                this.Position += new Vector3(0f, LycaderEngine.Game.Height + Texture.Height, 0f);
            }
            else if (this.Position.Y > LycaderEngine.Game.Height)
            {
                this.Position -= new Vector3(0f, LycaderEngine.Game.Height + Texture.Height, 0f);         
            }
        }

    }
}
