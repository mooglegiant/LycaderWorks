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
            this.ViewPort = new Box2(0f, LycaderEngine.Game.Height, LycaderEngine.Game.Width, 0f);
        }

        /// <summary>
        /// Initializes members of the Camera class
        /// </summary>
        public Camera(Box2 viewport)
        {
            this.ViewPort = viewport;
        }

        public Vector3 Position { get; set; } = new Vector3(0f, 0f, 0f);

        public Box2 ViewPort { get; set; }

        public float Zoom { get; set; } = 1f;

        public int Order { get; set; } = 1;

        public void CenterOnSprite(Sprite sprite)
        {
            this.Position = sprite.Center;
        }
    }
}
