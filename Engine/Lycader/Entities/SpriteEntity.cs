//-----------------------------------------------------------------------
// <copyright file="SpriteEntity.cs" company="Mooglegiant" >
//      Copyright (c) Mooglegiant. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Lycader.Entities
{
    using System.Collections.Generic;

    using Lycader;
    using OpenTK;

    /// <summary>
    /// SpriteEntity class
    /// </summary>
    public class SpriteEntity : Entity, IEntity
    {
        /// <summary>
        /// Initializes a new instance of the SpriteEntity class
        /// </summary>
        public SpriteEntity()
            : base(new Vector3(0f, 0f, 0f), 1f, 0)
        {
            this.Animations = new Dictionary<int, Animation>();
        }

        /// <summary>
        /// Initializes a new instance of the SpriteEntity class
        /// </summary>
        public SpriteEntity(Vector3 position, float zoom, int rotation)
            : base(position, zoom, rotation)
        {
            this.Animations = new Dictionary<int, Animation>();
        }

        /// <summary>
        /// Gets or sets the Sprite's current texture
        /// </summary>
        public string Texture { get; set; }

        /// <summary>
        /// Gets or sets the sprite's current animation
        /// </summary>
        public Dictionary<int, Animation> Animations { get; set; }

        public int CurrentAnimation { get; set; } = 0;

        public override Vector3 Center
        {
            get
            {
                Texture texture = TextureManager.Find(this.Texture);

                return new Vector3(
                        texture.Width != 0 ? this.Position.X + (texture.Width / 2) : this.Position.X,
                        texture.Height != 0 ? this.Position.Y + (texture.Height / 2) : this.Position.Y,
                        this.Position.Z);
            }
        }

        /// <summary>
        /// Gets a value indicating whether the sprite is displayed on the screen or not
        /// </summary>
        public override bool IsOnScreen(Camera camera)
        {
            Vector3 screenPosition = camera.GetScreenPosition(this.Position);
            Texture texture = TextureManager.Find(this.Texture);

            return (screenPosition.X < camera.WorldView.Right
                || screenPosition.Y < camera.WorldView.Top
                || screenPosition.X + texture.Width > camera.WorldView.Left
                || screenPosition.Y + texture.Height > camera.WorldView.Bottom);
        }

        /// <summary>
        /// Renders the Sprite to the screen
        /// </summary>
        public override void Draw(Camera camera)
        {
            Render.DrawTexture(camera, this.Texture, this.Position, this.Rotation, this.Zoom);       
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

        public Texture GetTextureInfo()
        {
            return TextureManager.Find(this.Texture);
        }

        /// <summary>
        /// Checks if two sprites are colliding
        /// </summary>
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
