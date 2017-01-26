using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lycader.Entities
{
    using OpenTK;
    using Lycader.Math;
    using Collision;

    public abstract class Entity
    {
        public Vector3 Position { get; set; }

        public virtual Vector3 Center
        {
            get
            {
                return this.Position;
            }
        }

        public ICollidable CollisionShape { get; set; }

        public float Zoom { get; set; }

        private int _rotation;

        public int Rotation
        {
            get
            {
                return _rotation;
            }
            set
            {
                _rotation = Math.Calc.Wrap(value, 0, 360);
            }
        }

        public bool IsDeleted { get; set; }

        public Entity(Vector3 position, float zoom = 1f, int rotation = 0)
        {
            this.Position = position;
            this.Zoom = zoom;
            this.Rotation = rotation;
        }

        abstract public void Draw(Camera camera);

        abstract public void Update();

        abstract public bool IsOnScreen(Camera camera);
    }
}
