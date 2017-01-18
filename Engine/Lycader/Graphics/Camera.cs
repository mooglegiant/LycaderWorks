//-----------------------------------------------------------------------
// <copyright file="Camera.cs" company="Mooglegiant" >
//      Copyright (c) Mooglegiant. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Lycader.Graphics
{   
    using System.Drawing;

    using OpenTK;
    using OpenTK.Graphics.OpenGL;
    using Lycader.Math;

    /// <summary>
    /// Our camera class
    /// </summary>
    public class Camera
    {
        /// <summary>
        /// Initializes members of the Camera class
        /// </summary>
        public Camera()
        {

            this.ScreenPosition = new Point(0, 0); 
            this.WorldSize = new Size(LycaderEngine.Resolution.Width, LycaderEngine.Resolution.Height); 
            this.WorldPosition = new PointF(0, 0); ;
        }

        /// <summary>
        /// Initializes members of the Camera class
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

        public Box2 WorldView { get { return new Box2(ScreenPosition.X, ScreenPosition.Y + WorldSize.Height, ScreenPosition.X + WorldSize.Width, ScreenPosition.Y); } }

        public float Zoom { get; set; } = 1f;

        public int Order { get; set; } = 1;

        public bool IsHud { get; set; } = false;

        public void CenterOnSprite(Sprite sprite)
        {
            this.WorldPosition = new PointF(sprite.Center.X, sprite.Center.Y);            
        }

        public void SetViewport()
        {
            GL.Viewport((int)(ScreenPosition.X * LycaderEngine.WindowAdjustment.Width), (int)(ScreenPosition.Y * LycaderEngine.WindowAdjustment.Height), (int)(this.WorldSize.Width * LycaderEngine.WindowAdjustment.Width), (int)(this.WorldSize.Height * LycaderEngine.WindowAdjustment.Height));
        }

        public void SetOrtho()
        {
            SizeF orthoAdjust = new SizeF(
                (float) 1 / ((float)LycaderEngine.Screen.Width / (float)this.WorldSize.Width) * LycaderEngine.WindowAdjustment.Width,
                (float) 1 / ((float)LycaderEngine.Screen.Height / (float)this.WorldSize.Height) * LycaderEngine.WindowAdjustment.Height
            );

            GL.Ortho(-orthoAdjust.Width, orthoAdjust.Width, -orthoAdjust.Height, orthoAdjust.Height, 0, 100);
        }
    }
}
