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
    using Lycader.Graphics;
    using System.Drawing;

    /// <summary>
    /// SpriteEntity class
    /// </summary>
    public class SpriteEntity : Entity, IEntity
    {
        /// <summary>
        /// Initializes a new instance of the SpriteEntity class
        /// </summary>
        public SpriteEntity()
            : this(new Vector3(0f, 0f, 0f), 1f, 0, "")
        {
        }

        /// <summary>
        /// Initializes a new instance of the SpriteEntity class
        /// </summary>
        public SpriteEntity(Vector3 position, float zoom, int rotation)
            : this(position, zoom, rotation, "")
        {
        }

        /// <summary>
        /// Initializes a new instance of the SpriteEntity class
        /// </summary>
        public SpriteEntity(Vector3 position, float zoom, int rotation, string texture)
            : base(position, zoom, rotation)
        {
            this.Texture = texture;
            this.Animations = new Dictionary<string, Animation>();

            if (!string.IsNullOrEmpty(texture))
            {
                this.TileSize = new Rectangle(0, 0, TextureManager.Find(texture).Width, TextureManager.Find(texture).Height);
            }
        }

        /// <summary>
        /// Gets or sets the Sprite's current texture
        /// </summary>
        public string Texture { get; set; }

        /// <summary>
        /// Gets or sets the sprite's current animation
        /// </summary>
        public Dictionary<string, Animation> Animations { get; set; }

        public string CurrentAnimation { get; set; } 

        public Rectangle TileSize { get; set; }

        public override Vector3 Center
        {
            get
            {
                Texture texture = TextureManager.Find(this.Texture);

                return new Vector3(
                        TileSize.Width != 0 ? this.Position.X + (TileSize.Width / 2) : this.Position.X,
                        TileSize.Height != 0 ? this.Position.Y + (TileSize.Height / 2) : this.Position.Y,
                        this.Position.Z);
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
                || screenPosition.X + TileSize.Width > camera.WorldView.Left
                || screenPosition.Y + TileSize.Height > camera.WorldView.Bottom);
        }

        /// <summary>
        /// Renders the Sprite to the screen
        /// </summary>
        public override void Draw(Camera camera)
        {
            if (!string.IsNullOrEmpty(this.CurrentAnimation))
            {
                int tile = this.Animations[CurrentAnimation].GetTile();
                Render.DrawTile(camera, this.Texture, this.Position, this.Rotation, this.Zoom, this.Alpha, tile, this.TileSize.Width, this.TileSize.Height);
            }
            else
            {
                Render.DrawTexture(camera, this.Texture, this.Position, this.Rotation, this.Zoom, this.Alpha);
            }
        }

        /// <summary>
        /// Override when inheriting for update logic
        /// </summary>
        public override void Update()
        {
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
