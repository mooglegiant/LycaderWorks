using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lycader.Graphics;
using OpenTK;

namespace Lycader.Scenes
{
    public class SceneManager
    {
        public List<IEntity> Entities { get; private set; }
        public List<Camera> Cameras { get; private set; }

        private List<IEntity> Queue { get; set; }

        public SceneManager()
        {
            this.Entities = new List<IEntity>();
            this.Queue = new List<IEntity>();
            this.Cameras = new List<Camera>();
            this.Cameras.Add(new Camera(new Box2(0, LycaderEngine.ScreenHeight, LycaderEngine.ScreenWidth, 0)));
        }

        public void Add(IEntity entity)
        {
            this.Queue.Add(entity);
        }

        public void Add(Camera camera)
        {
            this.Cameras.Add(camera);
        }

        public void Render()
        {
            foreach (Camera camera in this.Cameras.OrderBy(c => c.Order))
            {
                foreach (IEntity entity in Entities)
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
