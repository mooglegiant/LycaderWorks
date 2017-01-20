
namespace Lycader.Entities
{
    using OpenTK;
    using OpenTK.Graphics;
    using OpenTK.Graphics.OpenGL;
    using Lycader.Math;

    public class QuadEntity : Entity, IEntity
    {
        public float Width { get; set; }

        public float Height { get; set; }

        public Color4 Color { get; set; }

        public DrawType DrawType { get; set; }

        public float LineWidth { get; set; }

        public override Vector3 Center
        {
            get
            {
                return new Vector3(
                        this.Width != 0 ? this.Position.X + (this.Width / 2) : this.Position.X,
                        this.Height != 0 ? this.Position.Y + (this.Height / 2) : this.Position.Y,
                        this.Position.Z
                    );
            }
        }

        public QuadEntity(Vector3 position, float width, float height, Color4 color, DrawType drawtype, float lineWidth)   
            :base(position, 1f, 1)
        {     
            this.Width = width;
            this.Height = height;
            this.Color = color;
            this.DrawType = drawtype;
            this.LineWidth = LineWidth;
        }

        public override void Draw(Camera camera)
        {
            GL.Disable(EnableCap.Texture2D);       

            GL.PushMatrix();
            {
                GL.Color4(this.Color);
                GL.LineWidth(this.LineWidth);

                camera.SetViewport();
                camera.SetOrtho();

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

        public override void Update()
        {

        }

        public override bool IsOnScreen(Camera camera)
        {

            Vector3 screenPosition = camera.GetScreenPosition(this.Position);

            return (screenPosition.X < camera.WorldView.Right
                    || screenPosition.Y < camera.WorldView.Top
                    || screenPosition.X + this.Width > camera.WorldView.Left
                    || screenPosition.Y + this.Height > camera.WorldView.Bottom);
        }
    }
}
