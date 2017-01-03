using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lycader.Graphics
{
    using OpenTK;
    using Lycader.Math;
    using Collision;

    public abstract class Entity
    {
        public Vector3 Position { get; set; }

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

        public Vector2 GetScreenPosition(Camera camera)
        {
            Vector2 screenPosition;
            if (camera.IsHud)
            {
                screenPosition = new Vector2(this.Position.X, this.Position.Y);
            }
            else
            {
                screenPosition = new Vector2(this.Position.X - camera.WorldPosition.X, this.Position.Y - camera.WorldPosition.Y);
            }

            return screenPosition;
        }
    }
}
