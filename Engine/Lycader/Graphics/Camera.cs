//-----------------------------------------------------------------------
// <copyright file="Camera.cs" company="Mooglegiant" >
//      Copyright (c) Mooglegiant. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Lycader
{
    using System.Drawing;

    using OpenTK;
    using OpenTK.Graphics;
    using OpenTK.Graphics.OpenGL;

    using Lycader.Entities;

    /// <summary>
    /// Our camera class
    /// </summary>
    public class Camera
    {
        /// <summary>
        /// Initializes a new instance of the Camera class
        /// </summary>
        public Camera()
        {
            this.ScreenPosition = new Point(0, 0); 
            this.WorldSize = new Size(LycaderEngine.Resolution.Width, LycaderEngine.Resolution.Height); 
            this.WorldPosition = new PointF(0, 0);
        }

        /// <summary>
        /// Initializes a new instance of the Camera class
        /// </summary>
        public Camera(Point screenPosition, Size worldSize, PointF worldPosition)
        {
            this.ScreenPosition = screenPosition;
            this.WorldSize = worldSize;
            this.WorldPosition = worldPosition;
        }

        public Point ScreenPosition { get; set; }

        public Size WorldSize { get; set; }

        public PointF WorldPosition { get; set; }

        public Box2 WorldView
        {
            get
            {
                return new Box2(this.ScreenPosition.X, this.ScreenPosition.Y + this.WorldSize.Height, this.ScreenPosition.X + this.WorldSize.Width, this.ScreenPosition.Y);
            }
        }

        public float Zoom { get; set; } = 1f;

        public int Order { get; set; } = 1;

        public Color4 BackgroundColor { get; set; } = Color4.Black;

        public void BeginDraw()
        {
            Render.DrawQuad(this, new Vector3(1, 1, 0), this.WorldView.Width - 2, this.WorldView.Height - 2, this.BackgroundColor, 1.0f, DrawType.Solid);
        }

        public void CenterOnSprite(IEntity entity)
        {
            this.WorldPosition = new PointF(entity.Center.X - (this.WorldSize.Width / 2), entity.Center.Y - (this.WorldSize.Height / 2));            
        }

        public void CenterOnSprite(IEntity entity, float minX, float maxX, float minY, float maxY)
        {
            this.WorldPosition = new PointF(entity.Center.X - (this.WorldSize.Width / 2), entity.Center.Y - (this.WorldSize.Height / 2));
            this.WorldPosition = new PointF(MathHelper.Clamp(this.WorldPosition.X, minX, maxX), MathHelper.Clamp(this.WorldPosition.Y, minY, maxY));
        }

        public Vector3 GetScreenPosition(Vector3 position)
        {
            return new Vector3(position.X - this.WorldPosition.X, position.Y - this.WorldPosition.Y, position.Z + (this.Order * 100));
        }

        public void SetViewport()
        {
            GL.Viewport((int)(this.ScreenPosition.X * LycaderEngine.WindowAdjustment.Width), (int)(this.ScreenPosition.Y * LycaderEngine.WindowAdjustment.Height), (int)(this.WorldSize.Width * LycaderEngine.WindowAdjustment.Width), (int)(this.WorldSize.Height * LycaderEngine.WindowAdjustment.Height));
        }

        public void SetOrtho()
        {
            SizeF orthoAdjust = new SizeF(
                (float)1 / ((float)LycaderEngine.Screen.Width / (float)this.WorldSize.Width) * LycaderEngine.WindowAdjustment.Width,
                (float)1 / ((float)LycaderEngine.Screen.Height / (float)this.WorldSize.Height) * LycaderEngine.WindowAdjustment.Height);

            GL.Ortho(-orthoAdjust.Width, orthoAdjust.Width, -orthoAdjust.Height, orthoAdjust.Height, 0 + (this.Order * 100), 100 + (this.Order * 100));
        }
    }
}
