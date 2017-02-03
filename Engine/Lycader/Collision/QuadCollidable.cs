using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public QuadCollidable(Vector2 position, float width, float height)
        {
            this.Position = position;
            this.Width = width;
            this.Height = height;
        }
    }
}
