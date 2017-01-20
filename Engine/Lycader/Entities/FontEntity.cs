//-----------------------------------------------------------------------
// <copyright file="SpriteFont.cs" company="Mooglegiant" >
//      Copyright (c) Mooglegiant. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Lycader.Entities
{
    using System.Drawing;
    using OpenTK;
    using OpenTK.Graphics.OpenGL;

    /// <summary>
    /// A font made from a texture
    /// </summary>
    public class SpriteFont : Entity, IEntity
    {

        public override Vector3 Center
        {
            get
            {
                return new Vector3(
                        this.Position.X - ((this.Text.Length * this.Height) / 2),
                        this.Position.Y - (this.Height / 2),
                        this.Position.Z
                    );
            }
        }

        public SpriteFont()
            : base(new Vector3(0f,0f,0f), 1f, 1)
        {
        }

        /// <summary>
        /// Initializes a new instance of the SpriteFont class.
        /// </summary>
        /// <param name="texture">The loaded texture to use for the fonts</param>
        /// <param name="height">Maximum height in pixels of character </param>
        /// <param name="position">World postion of entity</param>
        /// <param name="text">Text to render</param>
        public SpriteFont(Texture texture, int height, Vector3 position, string text = "")
            : base(position, 1f, 1)
        {
            this.Texture = texture;
            this.Color = Color.White;
            this.Height = height;;
            this.Rotation = 0;
            this.Text = text;
        }

        /// <summary>
        /// Gets or sets the Sprite's current texture
        /// </summary>
        public Texture Texture { get; set; }

        /// <summary>
        /// Gets or sets the texture's color shading
        /// </summary>
        public Color Color { get; set; }

        /// <summary>
        /// Gets or sets the pixel height of the font
        /// </summary>
        public float Height { get; set; }

        /// <summary>
        /// Gets or sets the current text to display
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Draws the text to the screen
        /// </summary>
        public override void Draw(Camera camera)
        {
            if (string.IsNullOrEmpty(this.Text))
            {
                return;
            }

            Vector3 screenPosition = camera.GetScreenPosition(this.Position);

            this.Texture.Bind();

            GL.PushMatrix();
            {
                GL.Color3(this.Color);

                camera.SetViewport();
                camera.SetOrtho();

                GL.Translate(screenPosition.X, screenPosition.Y, 0);
                GL.Scale(this.Height, this.Height, 1f);
                GL.Rotate(this.Rotation, 0, 0, 1);

                GL.Begin(PrimitiveType.Quads);
                {
                    double offsetX = 0;

                    foreach (var ch in this.Text)
                    {
                        this.WriteCharacter(ch, offsetX);
                        offsetX += .75;
                    }
                }
                GL.End();
            }
            GL.PopMatrix();
           // GL.Color3(Color.White); //Reset color?
        }


        /// <summary>
        /// Gets a value indicating whether the sprite is displayed on the screen or not
        /// </summary>
        public override bool IsOnScreen(Camera camera)
        {
            Vector3 screenPosition = camera.GetScreenPosition(this.Position);

            return (screenPosition.X < camera.WorldView.Right
                 || screenPosition.Y < camera.WorldView.Top
                 || screenPosition.X + (this.Text.Length * this.Height) > camera.WorldView.Left
                 || screenPosition.Y + this.Height > camera.WorldView.Bottom);         
        }

        /// <summary>
        /// Override when inheriting for update logic
        /// </summary>
        public override void Update()
        {
        }

        /// <summary>
        /// Writes a single character to the screen
        /// </summary>
        /// <param name="ch">the current character to render</param>
        /// <param name="offsetX">the character's offset</param>
        private void WriteCharacter(char ch, double offsetX)
        {
            byte ascii;

            unchecked
            {
                ascii = (byte)ch;
            }

            int row = ascii >> 4;
            int col = ascii & 0x0F;

            double centerX = (col + 0.5) * .0625;
            double centerY = (row + 0.5) * .0625;
            double left = centerX - .025;
            double right = centerX + .025;
            double top = centerY - .025;
            double bottom = centerY + .025;

            int z = 100;

            GL.TexCoord2(left, top);
            GL.Vertex3(offsetX, 1, z);

            GL.TexCoord2(right, top);
            GL.Vertex3(offsetX + 1, 1, z);

            GL.TexCoord2(right, bottom);
            GL.Vertex3(offsetX + 1, 0, z);

            GL.TexCoord2(left, bottom);
            GL.Vertex3(offsetX, 0, z);
        }
    }
}
