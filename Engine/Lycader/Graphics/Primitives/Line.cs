
namespace Lycader.Graphics.Primitives
{
    using OpenTK;
    using OpenTK.Graphics;
    using OpenTK.Graphics.OpenGL;
    using Lycader.Math;

    public class Line : Entity, IEntity
    {
        public Vector3 EndPoint { get; set; }

        public Color4 Color { get; set; }

        public float LineWidth { get; set; }


        public Line(Vector3 position, Vector3 endPoint, Color4 color, float lineWidth)   
            :base(position, 1f, 1)
        {
            this.EndPoint = endPoint;
            this.Color = color;     
            this.LineWidth = lineWidth;
        }

        public void Draw(Camera camera)
        {
            GL.Disable(EnableCap.Texture2D);       

            GL.PushMatrix();
            {
                GL.Color4(this.Color);
                GL.LineWidth(this.LineWidth);
                GL.Viewport((int)camera.ViewPort.Left, (int)camera.ViewPort.Bottom, (int)camera.ViewPort.Right, (int)camera.ViewPort.Top);

                GL.Begin(PrimitiveType.Lines);
                {
                    GL.Vertex2(this.Position.X, this.Position.Y);
                    GL.Vertex2(this.EndPoint.X, this.EndPoint.Y);
                }
                GL.End();

                GL.Color4(Color4.White);
            }
            GL.PopMatrix();

            GL.Enable(EnableCap.Texture2D);
        }

        public virtual void Update()
        {

        }

        public bool IsOnScreen(Camera camera)
        {
            Vector2 minPoint = new Vector2(System.Math.Min(this.Position.X, this.EndPoint.X), System.Math.Min(this.Position.Y, this.EndPoint.Y));
            Vector2 maxPoint = new Vector2(System.Math.Max(this.Position.X, this.EndPoint.X), System.Math.Max(this.Position.Y, this.EndPoint.Y));

            Vector2 screenMin = new Vector2(minPoint.X - camera.Position.X, minPoint.Y - camera.Position.Y);
            Vector2 screenMax = new Vector2(maxPoint.X - camera.Position.X, maxPoint.Y - camera.Position.Y);

            return (screenMax.X < camera.ViewPort.Right
                    || screenMax.Y < camera.ViewPort.Top
                    || screenMin.X > camera.ViewPort.Left                
                    || screenMin.Y > camera.ViewPort.Bottom);
        }
    }
}
