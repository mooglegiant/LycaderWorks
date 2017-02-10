//-----------------------------------------------------------------------
// <copyright file="Entity.cs" company="Mooglegiant" >
//      Copyright (c) Mooglegiant. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Lycader.Entities
{
    using OpenTK;
    using Lycader.Collision;

    public abstract class Entity
    {
        private int rotation;

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

        public int Rotation
        {
            get
            {
                return this.rotation;
            }

            set
            {
                this.rotation = Math.Calculate.Wrap(value, 0, 360);
            }
        }

        public bool IsDeleted { get; set; }

        /// <summary>
        /// Initializes a new instance of the Entity class
        /// </summary>
        public Entity(Vector3 position, float zoom = 1f, int rotation = 0)
        {
            this.Position = position;
            this.Zoom = zoom;
            this.Rotation = rotation;
        }

        public abstract void Draw(Camera camera);

        public abstract void Update();

        public abstract bool IsOnScreen(Camera camera);
    }
}
