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
            this.ViewPort = new Box2(0f, LycaderEngine.ScreenHeight, LycaderEngine.ScreenWidth, 0f);
        }

        /// <summary>
        /// Initializes members of the Camera class
        /// </summary>
        public Camera(Box2 viewport)
        {
            this.ViewPort = viewport;
        }

        public Vector2 Position { get; set; } = new Vector2(0f, 0f);

        public Box2 ViewPort { get; set; }

        public float Zoom { get; set; } = 1f;

        public int Order { get; set; } = 1;

        public void CenterOnSprite(Sprite sprite)
        {
            this.Position = new Vector2(sprite.CenterX, sprite.CenterY);
        }
    }
}
