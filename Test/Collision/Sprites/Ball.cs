
namespace CollsionTest.Sprites
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using OpenTK;
    using Lycader;
    using Lycader.Entities;
    using Lycader.Collision;

    public class Ball : SpriteEntity
    {

        private Random Rand = new Random();

        public Ball()
        {
            XSpeed = (1 + Rand.Next(2)) * (Rand.Next(2) == 0 ? -1 : 1);
            YSpeed = (1 + Rand.Next(2)) * (Rand.Next(2) == 0 ? -1 : 1);

            this.Position = new Vector3(Rand.Next(LycaderEngine.Screen.Width), Rand.Next(LycaderEngine.Screen.Height), 0f);

            this.Texture = "ball";

            this.CollisionShape = new CircleCollidable(new Vector2(this.Center.X, this.Center.Y), (this.GetTextureInfo().Width / 2));
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
            if (this.Position.X < -GetTextureInfo().Width)
            {
                this.Position += new Vector3(LycaderEngine.Screen.Width + GetTextureInfo().Width, 0f, 0f);     
            }
            else if (this.Position.X > LycaderEngine.Screen.Width)
            {
                this.Position -= new Vector3(LycaderEngine.Screen.Width + GetTextureInfo().Width, 0f, 0f);
            }

            if (this.Position.Y < -GetTextureInfo().Height)
            {
                this.Position += new Vector3(0f, LycaderEngine.Screen.Height + GetTextureInfo().Height, 0f);
            }
            else if (this.Position.Y > LycaderEngine.Screen.Height)
            {
                this.Position -= new Vector3(0f, LycaderEngine.Screen.Height + GetTextureInfo().Height, 0f);         
            }
        }

    }
}
