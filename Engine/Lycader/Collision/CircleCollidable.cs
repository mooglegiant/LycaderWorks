//-----------------------------------------------------------------------
// <copyright file="CircleCollidable.cs" company="Mooglegiant" >
//      Copyright (c) Mooglegiant. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Lycader.Collision
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using OpenTK;

    public class CircleCollidable : ICollidable
    {
        public string Name { get; set; }

        public Vector2 Position { get; set; }

        public float Radius { get; set; }

        public Vector2 Center
        {
            get
            {
                return new Vector2(this.Position.X + this.Radius, this.Position.Y + this.Radius);
            }
        }

        /// <summary>
        /// Initializes a new instance of the CircleCollidable class
        /// </summary>
        public CircleCollidable(Vector2 position, float radius)
        {
            this.Position = position;
            this.Radius = radius;
            this.Name = string.Empty;
        }
    }
}
