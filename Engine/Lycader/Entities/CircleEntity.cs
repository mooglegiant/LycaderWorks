
namespace Lycader.Entities
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

        public override Vector3 Center
        {
            get
            {
                return new Vector3(
                        this.Position.X - this.Radius,
                        this.Position.Y - this.Radius,
                        this.Position.Z
                    );
            }
        }

        public CircleEntity(Vector3 position, float radius, Color4 color, DrawType drawtype, float lineWidth)   
            :base(position, 1f, 1)
        {     
            this.Radius = radius;
            this.Color = color;
            this.DrawType = drawtype;
            this.LineWidth = LineWidth;
        }


        public override void Draw(Camera camera)
        {
            GL.Disable(EnableCap.Texture2D);
            int maxPoints = 0;
            Vector3 screenPosition = camera.GetScreenPosition(this.Position);

            GL.PushMatrix();
            {
                GL.Color4(this.Color);
                GL.LineWidth(this.LineWidth);              

                camera.SetViewport();
                camera.SetOrtho();

                if (this.DrawType == DrawType.Outline)
                {
                    GL.Begin(PrimitiveType.LineLoop);
                    {
                        maxPoints = 100;
                        for (int i = 0; i <= maxPoints; i++)
                        {
                            GL.Vertex3(screenPosition.X + (this.Radius * System.Math.Cos(i * Calc.TwoPi / maxPoints)), screenPosition.Y + (this.Radius * System.Math.Sin(i * Calc.TwoPi / maxPoints)), screenPosition.Z);
                        }
                    }
                    GL.End();
                }
                else if (this.DrawType == DrawType.Solid)
                {
                    GL.Begin(PrimitiveType.TriangleFan);
                    {
                        maxPoints = 50;
                        GL.Vertex3(screenPosition.X, screenPosition.Y, screenPosition.Z); //Center of circle
                        for (int i = 0; i <= maxPoints; i++)
                        {
                            GL.Vertex3(screenPosition.X + (this.Radius * System.Math.Cos(i * Calc.TwoPi / maxPoints)), screenPosition.Y + (this.Radius * System.Math.Sin(i * Calc.TwoPi / maxPoints)), screenPosition.Z);
                        }
                    }
                    GL.End();
                }          

                GL.Color4(Color4.White);
            }
            GL.PopMatrix();

            GL.Enable(EnableCap.Texture2D);
        }

        public override void Update()
        {
        }

        public override bool IsOnScreen(Camera camera)
        {
            Vector2 screenPosition = new Vector2(this.Position.X - camera.ScreenPosition.X, this.Position.Y - camera.ScreenPosition.Y);

            float diameter = (this.Radius * 2) * this.Zoom;

            return (screenPosition.X - diameter < camera.WorldView.Right
                    || screenPosition.Y - diameter < camera.WorldView.Top
                    || screenPosition.X + diameter > camera.WorldView.Left
                    || screenPosition.Y + diameter > camera.WorldView.Bottom);
        }
    }
}
