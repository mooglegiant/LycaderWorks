//-----------------------------------------------------------------------
// <copyright file="QuadCollidable.cs" company="Mooglegiant" >
//      Copyright (c) Mooglegiant. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Lycader.Collision
{
    using OpenTK;

    public class QuadCollidable : ICollidable
    {
        public string Name { get; set; }

        public Vector2 Position { get; set; }

        public float Width { get; set; }

        public float Height { get; set; }

        public Vector2 Center
        {
            get
            {
                return new Vector2(this.Position.X + (this.Width / 2), this.Position.Y + (this.Height / 2));
            }
        }

        /// <summary>
        /// Initializes a new instance of the QuadCollidable class
        /// </summary>
        public QuadCollidable(Vector2 position, float width, float height)
        {
            this.Position = position;
            this.Width = width;
            this.Height = height;
        }
    }
}
