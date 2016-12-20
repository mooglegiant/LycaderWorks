//-----------------------------------------------------------------------
// <copyright file="Camera.cs" company="Mooglegiant" >
//      Copyright (c) Mooglegiant. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Lycader.Graphics
{   
    using System.Drawing;

    using OpenTK;
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
            this.WindowSize = new Size(LycaderEngine.Resolution.Width, LycaderEngine.Resolution.Height); 
            this.WorldPosition = new PointF(0, 0); ;
        }

        /// <summary>
        /// Initializes members of the Camera class
        /// </summary>
        public Camera(Point screenPosition, Size windowSize, PointF worldPosition)
        {
            this.ScreenPosition = screenPosition;
            this.WindowSize = windowSize;
            this.WorldPosition = worldPosition;
        }

        public Point ScreenPosition { get; set; }

        public Size WindowSize { get; set; }

        public PointF WorldPosition { get; set; }

        public Box2 ViewPort { get { return new Box2(ScreenPosition.X, ScreenPosition.Y + WindowSize.Height, ScreenPosition.X + WindowSize.Width, ScreenPosition.Y); } }

        public float Zoom { get; set; } = 1f;

        public int Order { get; set; } = 1;

        public void CenterOnSprite(Sprite sprite)
        {
            this.WorldPosition = new PointF(sprite.Center.X, sprite.Center.Y);            
        }
    }
}
