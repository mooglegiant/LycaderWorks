//-----------------------------------------------------------------------
// <copyright file="FontEntity.cs" company="Mooglegiant" >
//      Copyright (c) Mooglegiant. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Lycader.Entities
{
    using System.Drawing;
    using OpenTK;
    using OpenTK.Graphics;
    using Lycader.Graphics;

    /// <summary>
    /// A font made from a texture
    /// </summary>
    public class FontEntity : Entity, IEntity
    {
        public override Vector3 Center
        {
            get
            {
                return new Vector3(
                        this.Position.X - ((this.Text.Length * this.FontSize) / 2),
                        this.Position.Y - (this.FontSize / 2),
                        this.Position.Z);
            }
        }

        /// <summary>
        /// Initializes a new instance of the FontEntity class.
        /// </summary>
        /// <param name="texture">The loaded texture to use for the fonts</param>
        /// <param name="fontSize">Maximum height in pixels of character </param>
        /// <param name="position">World position of entity</param>
        /// <param name="text">Text to render</param>
        public FontEntity(string texture, int fontSize, Vector3 position, float spacing, string text = "")
            : base(position, 1f, 1)
        {
            this.Texture = texture;
            this.Color = Color4.White;
            this.FontSize = fontSize;
            this.Rotation = 0;
            this.Text = text;
            this.Spacing = spacing;
            this.BackgroundColor = Color4.Transparent;
            this.Padding = new PointF(0, 0);
        }

        /// <summary>
        /// Gets or sets the Sprite's current texture name
        /// </summary>
        public string Texture { get; set; }

        /// <summary>
        /// Gets or sets the text's color shading
        /// </summary>
        public Color4 Color { get; set; }

        /// <summary>
        /// Gets or sets the text's background color
        /// </summary>
        public Color4 BackgroundColor { get; set; }

        /// <summary>
        /// Gets or sets the text background color padding
        /// </summary>
        public PointF Padding { get; set; }

        /// <summary>
        /// Gets or sets the pixel height of the font
        /// </summary>
        public float FontSize { get; set; }

        /// <summary>
        /// Gets or sets the current text to display
        /// </summary>
        public string Text { get; set; }

        public float Spacing { get; set; }

        /// <summary>
        /// Draws the text to the screen
        /// </summary>
        public override void Draw(Camera camera)
        {
            if (this.BackgroundColor != Color4.Transparent)
            {
                Render.DrawQuad(camera, new Vector3(this.Position.X - this.Padding.X, this.Position.Y - this.Padding.Y, this.Position.Z - 1), (this.Text.Length * this.FontSize * this.Spacing) + (this.Padding.X * 2), this.FontSize + (this.Padding.Y * 2), this.BackgroundColor, 1f, DrawType.Solid);
            }

            Render.DrawText(camera, this.Texture, this.Position, this.Color, this.FontSize, this.Rotation, this.Spacing, this.Text);
        }

        /// <summary>
        /// Gets a value indicating whether the sprite is displayed on the screen or not
        /// </summary>
        public override bool IsOnScreen(Camera camera)
        {
            Vector3 screenPosition = camera.GetScreenPosition(this.Position);

            return (screenPosition.X < camera.WorldView.Right
                 || screenPosition.Y < camera.WorldView.Top
                 || screenPosition.X + (this.Text.Length * this.FontSize) > camera.WorldView.Left
                 || screenPosition.Y + this.FontSize > camera.WorldView.Bottom);         
        }

        /// <summary>
        /// Override when inheriting for update logic
        /// </summary>
        public override void Update()
        {
        }
    }
}
