
namespace Lycader.Graphics.Primitives
{
    using OpenTK;
    using OpenTK.Graphics;
    using OpenTK.Graphics.OpenGL;
    using Lycader.Math;

    public class Rectangle : Entity, IEntity
    {
        public float Width { get; set; }

        public float Height { get; set; }

        public Color4 Color { get; set; }

        public DrawType DrawType { get; set; }

        public float LineWidth { get; set; }


        public Rectangle(Vector3 position, float width, float height, Color4 color, DrawType drawtype, float lineWidth)   
            :base(position, 1f, 1)
        {     
            this.Width = width;
            this.Height = height;
            this.Color = color;
            this.DrawType = drawtype;
            this.LineWidth = LineWidth;
        }

        public void Draw(Camera camera)
        {
            GL.Disable(EnableCap.Texture2D);       

            GL.PushMatrix();
            {
                GL.Color4(this.Color);
                GL.LineWidth(this.LineWidth);
                GL.Viewport((int)camera.ViewPort.Left, (int)camera.ViewPort.Bottom, (int)camera.ViewPort.Right, (int)camera.ViewPort.Top);

                if (this.DrawType == DrawType.Outline)
                {
                    GL.Begin(PrimitiveType.Lines);
                }
                else if (this.DrawType == DrawType.Solid)
                {
                    GL.Begin(PrimitiveType.Quads);
                }

                GL.Vertex3(this.Position.X, this.Position.Y, this.Position.Z);
                GL.Vertex3(this.Position.X + Width, this.Position.Y, this.Position.Z);
                GL.Vertex3(this.Position.X + Width, this.Position.Y + Height, this.Position.Z);
                GL.Vertex3(this.Position.X, this.Position.Y + Height, this.Position.Z);

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
            Vector2 screenPosition = new Vector2(this.Position.X - camera.Position.X, this.Position.Y - camera.Position.Y);

            return (screenPosition.X < camera.ViewPort.Right
                    || screenPosition.Y < camera.ViewPort.Top
                    || screenPosition.X + this.Width > camera.ViewPort.Left
                    || screenPosition.Y + this.Height > camera.ViewPort.Bottom);
        }
    }
}
