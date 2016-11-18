﻿//-----------------------------------------------------------------------
// <copyright file="Sprite.cs" company="Mooglegiant" >
//      Copyright (c) Mooglegiant. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Lycader.Graphics
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;

    using Lycader;
    using Lycader.Math;

    using OpenTK;
    using OpenTK.Graphics.OpenGL;
    using OpenTK.Graphics;

    /// <summary>
    /// Game sprite class
    /// </summary>
    public class Sprite : Entity, IEntity
    {
        /// <summary>
        /// Initializes a new instance of the Sprite class
        /// </summary>
        public Sprite()
            : base(new Vector3(0f,0f,0f), 1f, 1)
        {  
            this.Animations = new Dictionary<int, Animation>();
        }      

        /// <summary>
        /// Gets or sets the Sprite's current texture
        /// </summary>
        public Texture2D Texture { get; set; }

        /// <summary>
        /// Gets or sets the sprite's current animation
        /// </summary>
        public Dictionary<int, Animation> Animations { get; set; }

        public int CurrentAnimation { get; set; } = 0;

        /// <summary>
        /// Gets the center X of the Sprite's current texture
        /// </summary>
        public float CenterX
        {
            get
            {
                if (Texture.Width != 0)
                {
                    return this.Position.X + (this.Texture.Width / 2);
                }

                return this.Position.X;
            }
        }

        /// <summary>
        /// Gets the center Y of the Sprite's current texture
        /// </summary>
        public float CenterY
        {
            get
            {
                if (Texture.Height != 0)
                {
                    return this.Position.Y + (this.Texture.Height / 2);
                }

                return this.Position.Y;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the sprite is displayed on the screen or not
        /// </summary>
        public bool IsOnScreen(Camera camera)
        {
            Vector2 screenPosition = new Vector2(this.Position.X - camera.Position.X, this.Position.Y - camera.Position.Y);

            return (screenPosition.X < camera.ViewPort.Right
                || screenPosition.Y < camera.ViewPort.Top
                || screenPosition.X + this.Texture.Width > camera.ViewPort.Left
                || screenPosition.Y + this.Texture.Height > camera.ViewPort.Bottom);
        }

        /// <summary>
        /// Renders the Sprite to the screen
        /// </summary>
        public virtual void Draw(Camera camera)
        {
            if (this.Texture == null)
            {
                return;
            }

            Vector2 screenPosition = new Vector2(this.Position.X - camera.Position.X, this.Position.Y - camera.Position.Y);

            float aspectY = camera.Zoom;
            float aspectX = camera.Zoom;
            GL.BindTexture(TextureTarget.Texture2D, this.Texture.Handle);

            GL.PushMatrix();
            {           
                GL.Color4(Color4.White);

                GL.Viewport((int)camera.ViewPort.Left, (int)camera.ViewPort.Bottom, (int)camera.ViewPort.Right, (int)camera.ViewPort.Top);
                
                // Translate to center of the texture
                GL.Translate(screenPosition.X, screenPosition.Y + this.Texture.Height, 0);
                GL.Translate(this.Texture.Width / 2, -1 * (this.Texture.Height / 2), 0.0f);

                GL.Rotate(this.Rotation, 0, 0, 1);
                GL.Scale(this.Zoom, this.Zoom, 1f);

                // Translate back to the starting co-ordinates so drawing works
                GL.Translate(-1 * (this.Texture.Width / 2), 1 * (this.Texture.Height / 2), 0.0f);
                GL.Translate(-screenPosition.X, -(screenPosition.Y + this.Texture.Height), 0);

                GL.Begin(PrimitiveType.Quads);
                {
                    GL.TexCoord2(0, 0);
                    GL.Vertex3(screenPosition.X, screenPosition.Y + this.Texture.Height * aspectY, this.Position.Z);

                    GL.TexCoord2(0, 1);
                    GL.Vertex3(screenPosition.X, screenPosition.Y, this.Position.Z);

                    GL.TexCoord2(1, 1);
                    GL.Vertex3(screenPosition.X + this.Texture.Width * aspectX, screenPosition.Y, this.Position.Z);

                    GL.TexCoord2(1, 0);
                    GL.Vertex3(screenPosition.X + this.Texture.Width * aspectX, screenPosition.Y + this.Texture.Height * aspectY, this.Position.Z);
                }
                GL.End();

               // GL.BindTexture(TextureTarget.Texture2D, 0);
            }
            GL.PopMatrix();
        }

        /// <summary>
        /// Override when inheriting for update logic
        /// </summary>
        public virtual void Update()
        {
        }

        /// <summary>
        /// Creates an animation in the animation storage
        /// </summary>
        /// <param name="animationNumber">animation indexer</param>
        /// <param name="loop">does this animation loop or not</param>
        public void CreateAnimation(int animationNumber, bool loop)
        {
            if (this.Animations.ContainsKey(animationNumber))
            {
                this.Animations.Remove(animationNumber);
            }

            this.Animations.Add(animationNumber, new Animation(loop));
        }

        /// <summary>
        /// Checks if two sprites are colliding
        /// </summary>
        /// <param name="sprite">Collision Sprite</param>
        /// <returns>Indicates if the two are colliding</returns>
        public bool IsColliding(Sprite sprite)
        {
            if (this.Texture == null || sprite.Texture == null)
            {
                return false;
            }

            Box2 rec1, rec2;

            rec1 = new Box2(0, 0, this.Texture.Width, this.Texture.Height);
            rec2 = new Box2(0, 0, sprite.Texture.Width, sprite.Texture.Height); 

            // Offset collision box based on sprite location
            rec1.Right += this.Position.X;
            rec1.Top += this.Position.Y;
            rec1.Left += this.Position.X;
            rec1.Bottom += this.Position.Y;

            rec2.Right += sprite.Position.X;
            rec2.Top += sprite.Position.Y;
            rec2.Left += sprite.Position.X;
            rec2.Bottom += sprite.Position.Y;

            //// TODO: Apply rotation & scale math?

            return this.IsBoxColliding(rec1, rec2);
        }

        /// <summary>
        /// Checks if the boxes are overlapping
        /// </summary>
        /// <param name="rec1">sprite's box</param>
        /// <param name="rec2">colliding box</param>
        /// <returns>A value indicating whether the two boxes are overlapping or not</returns>
        private bool IsBoxColliding(Box2 rec1, Box2 rec2)
        {
            return !(rec1.Left > rec2.Right || rec1.Right < rec2.Left
                || rec1.Top > rec2.Bottom || rec1.Bottom < rec2.Top);
        }
    }
}
