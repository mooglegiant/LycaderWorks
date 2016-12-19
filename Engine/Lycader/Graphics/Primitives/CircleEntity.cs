
namespace Lycader.Graphics.Primitives
{
    using OpenTK;
    using OpenTK.Graphics;
    using OpenTK.Graphics.OpenGL;
    using Lycader.Math;

    public class CircleEntity : Entity, IEntity
    {    
        public float Radius { get; set; }

        public Color4 Color { get; set; }

        public DrawType DrawType { get; set; }

        public float LineWidth { get; set; }


        public CircleEntity(Vector3 position, float radius, Color4 color, DrawType drawtype, float lineWidth)   
            :base(position, 1f, 1)
        {     
            this.Radius = radius;
            this.Color = color;
            this.DrawType = drawtype;
            this.LineWidth = LineWidth;
        }

        public void Draw(Camera camera)
        {
            GL.Disable(EnableCap.Texture2D);
            int maxPoints = 0;

            GL.PushMatrix();
            {
                GL.Color4(this.Color);
                GL.LineWidth(this.LineWidth);              
                GL.Viewport(camera.ScreenPosition, camera.WindowSize);

                if (this.DrawType == DrawType.Outline)
                {
                    GL.Begin(PrimitiveType.LineLoop);
                    {
                        maxPoints = 100;
                        for (int i = 0; i <= maxPoints; i++)
                        {
                            GL.Vertex3(this.Position.X + (this.Radius * System.Math.Cos(i * Calc.TwoPi / maxPoints)), this.Position.Y + (this.Radius * System.Math.Sin(i * Calc.TwoPi / maxPoints)), this.Position.Z);
                        }
                    }
                    GL.End();
                }
                else if (this.DrawType == DrawType.Solid)
                {
                    GL.Begin(PrimitiveType.TriangleFan);
                    {
                        maxPoints = 50;
                        GL.Vertex3(this.Position.X, this.Position.Y, this.Position.Z); //Center of circle
                        for (int i = 0; i <= maxPoints; i++)
                        {
                            GL.Vertex3(this.Position.X + (this.Radius * System.Math.Cos(i * Calc.TwoPi / maxPoints)), this.Position.Y + (this.Radius * System.Math.Sin(i * Calc.TwoPi / maxPoints)), this.Position.Z);
                        }
                    }
                    GL.End();
                }          

                GL.Color4(Color4.White);
            }
            GL.PopMatrix();

            GL.Enable(EnableCap.Texture2D);
        }

        public void Update()
        {
        }

        public bool IsOnScreen(Camera camera)
        {
            Vector2 screenPosition = new Vector2(this.Position.X - camera.ScreenPosition.X, this.Position.Y - camera.ScreenPosition.Y);

            float rad = this.Radius * this.Zoom;

            return (screenPosition.X - rad < camera.ViewPort.Right
                    || screenPosition.Y - rad < camera.ViewPort.Top
                    || screenPosition.X + rad > camera.ViewPort.Left
                    || screenPosition.Y + rad > camera.ViewPort.Bottom);
        }
    }
}
