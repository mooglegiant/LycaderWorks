//-----------------------------------------------------------------------
// <copyright file="EntityManager.cs" company="Mooglegiant" >
//      Copyright (c) Mooglegiant. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Lycader.Entities
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Lycader.Entities;
    using OpenTK;
    using Lycader.Graphics;

    public class EntityManager
    {
        public List<IEntity> Entities { get; private set; }

        public List<Camera> Cameras { get; private set; }

        private List<IEntity> Queue { get; set; }

        /// <summary>
        /// Initializes a new instance of the EntityManager class
        /// </summary>
        public EntityManager()
        {
            this.Entities = new List<IEntity>();
            this.Queue = new List<IEntity>();
            this.Cameras = new List<Camera>();
            this.Cameras.Add(new Camera());
        }

        public void Add(IEntity entity)
        {
            this.Queue.Add(entity);
        }

        public void Render()
        {
            this.Entities.Sort((x, y) => x.Position.Z.CompareTo(y.Position.Z));

            foreach (Camera camera in this.Cameras.OrderBy(c => c.Order))
            {
                foreach (IEntity entity in this.Entities)
                {
                    if (entity.IsOnScreen(camera))
                    {
                        entity.Draw(camera);
                    }
                }
            }
        }

        public void Update()
        {
            this.Entities.RemoveAll(e => e.IsDeleted);
            this.Entities.AddRange(this.Queue);
            this.Queue.Clear();

            foreach (IEntity entity in this.Entities)
            {
                entity.Update();
            }
        }
    }
}
