using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lycader.Graphics
{
    using OpenTK;
    using Lycader.Math;

    public abstract class Entity
    {
        public Vector3 Position { get; set; }

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
                _rotation = value;

                while (_rotation > 360)
                {
                    _rotation -= 360;
                }

                while (_rotation < -360)
                {
                    _rotation += 360;
                }
            }
        }

        public bool IsDeleted {get; set;}

        public Entity(Vector3 position, float zoom = 1f, int rotation = 1)
        {
            this.Position = position;
            this.Zoom = zoom;
            this.Rotation = Rotation;            
        }
    }
}
