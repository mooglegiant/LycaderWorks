using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lycader.Graphics.Collision
{
    using OpenTK;

    public class CircleCollidable : ICollidable
    {

        public string Name { get; set; }

        public Vector2 Offset { get; set; }

        public float Radius { get; set; }

        public CircleCollidable(Vector2 offset, float radius)
        {
            this.Offset = offset;
            this.Radius = radius;
            this.Name = string.Empty;        
        }
    }
}
