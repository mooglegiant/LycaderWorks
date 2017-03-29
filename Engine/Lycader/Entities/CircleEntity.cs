//-----------------------------------------------------------------------
// <copyright file="CircleEntity.cs" company="Mooglegiant" >
//      Copyright (c) Mooglegiant. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Lycader.Entities
{
    using OpenTK;
    using OpenTK.Graphics;
    using Lycader.Graphics;

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
                        this.Position.Z);
            }
        }

        /// <summary>
        /// Initializes a new instance of the CircleEntity class
        /// </summary>
        public CircleEntity(Vector3 position, float radius, Color4 color, DrawType drawtype, float lineWidth)
            : base(position, 1f, 1)
        {
            this.Radius = radius;
            this.Color = color;
            this.DrawType = drawtype;
            this.LineWidth = lineWidth;
        }

        public override void Draw(Camera camera)
        {               
            Render.DrawCircle(camera, this.Position, this.Color, this.LineWidth, this.Radius, this.DrawType);         
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
