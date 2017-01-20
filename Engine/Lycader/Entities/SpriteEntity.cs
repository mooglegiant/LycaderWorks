//-----------------------------------------------------------------------
// <copyright file="SpriteEntity.cs" company="Mooglegiant" >
//      Copyright (c) Mooglegiant. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Lycader.Entities
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
    /// SpriteEntity class
    /// </summary>
    public class SpriteEntity : Entity, IEntity
    {
        /// <summary>
        /// Initializes a new instance of the Sprite class
        /// </summary>
        public SpriteEntity()
            : base(new Vector3(0f,0f,0f), 1f, 1)
        {  
            this.Animations = new Dictionary<int, Animation>();
        }      

        /// <summary>
        /// Gets or sets the Sprite's current texture
        /// </summary>
        public Texture Texture { get; set; }

        /// <summary>
        /// Gets or sets the sprite's current animation
        /// </summary>
        public Dictionary<int, Animation> Animations { get; set; }

        public int CurrentAnimation { get; set; } = 0;

        public override Vector3 Center
        {
            get
            {
                return new Vector3(
                        Texture.Width != 0 ? this.Position.X + (this.Texture.Width / 2) : this.Position.X,
                        Texture.Height != 0 ? this.Position.Y + (this.Texture.Height / 2) : this.Position.Y,
                        this.Position.Z
                    );
            }
        }

        /// <summary>
        /// Gets a value indicating whether the sprite is displayed on the screen or not
        /// </summary>
        public override bool IsOnScreen(Camera camera)
        {
            Vector3 screenPosition = camera.GetScreenPosition(this.Position);

            return (screenPosition.X < camera.WorldView.Right
                || screenPosition.Y < camera.WorldView.Top
                || screenPosition.X + this.Texture.Width > camera.WorldView.Left
                || screenPosition.Y + this.Texture.Height > camera.WorldView.Bottom);
        }

        /// <summary>
        /// Renders the Sprite to the screen
        /// </summary>
        public override void Draw(Camera camera)
        {
            if (this.Texture == null)
            {
                return;
            }

            Vector3 screenPosition = camera.GetScreenPosition(this.Position);
            this.Texture.Bind();

            GL.PushMatrix();
            {
                camera.SetViewport();
                camera.SetOrtho();

                GL.Color4(Color4.White);

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
                    GL.Vertex3(screenPosition.X, screenPosition.Y + this.Texture.Height * camera.Zoom, this.Position.Z);

                    GL.TexCoord2(0, 1);
                    GL.Vertex3(screenPosition.X, screenPosition.Y, this.Position.Z);

                    GL.TexCoord2(1, 1);
                    GL.Vertex3(screenPosition.X + this.Texture.Width * camera.Zoom, screenPosition.Y, this.Position.Z);

                    GL.TexCoord2(1, 0);
                    GL.Vertex3(screenPosition.X + this.Texture.Width * camera.Zoom, screenPosition.Y + this.Texture.Height * camera.Zoom, this.Position.Z);
                }
                GL.End();
            }
            GL.PopMatrix();
        }

        /// <summary>
        /// Override when inheriting for update logic
        /// </summary>
        public override void Update()
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
        public bool IsColliding(IEntity entity)
        {
            if (this.Texture == null)
            {
                return false;
            }

            //// TODO: Apply rotation & scale math?
            return Collision.Collision2D.IsColliding(this.CollisionShape, entity.CollisionShape);
        }
    }
}
