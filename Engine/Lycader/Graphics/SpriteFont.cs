//-----------------------------------------------------------------------
// <copyright file="SpriteFont.cs" company="Mooglegiant" >
//      Copyright (c) Mooglegiant. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Lycader.Graphics
{
    using System.Drawing;
    using OpenTK.Graphics.OpenGL;

    /// <summary>
    /// A font made from a texture
    /// </summary>
    public class SpriteFont
    {
        /// <summary>
        /// Initializes a new instance of the SpriteFont class.
        /// </summary>
        /// <param name="texture">The loaded texture to use for the fonts</param>
        /// <param name="height">Maximum height in pixels of character </param>
        public SpriteFont(Texture2D texture, int height)
        {
            this.Texture = texture;
            this.Color = Color.White;
            this.Height = height;
            this.X = 0;
            this.Y = 0;
            this.Rotation = 0;
            this.Text = string.Empty;
        }

        /// <summary>
        /// Gets or sets the Sprite's current texture
        /// </summary>
        public Texture2D Texture { get; set; }

        /// <summary>
        /// Gets or sets the texture's color shading
        /// </summary>
        public Color Color { get; set; }

        /// <summary>
        /// Gets or sets the pixel height of the font
        /// </summary>
        public double Height { get; set; }

        /// <summary>
        /// Gets or sets the X coordinate
        /// </summary>
        public double X { get; set; }

        /// <summary>
        /// Gets or sets the Y coordinate
        /// </summary>
        public double Y { get; set; }

        /// <summary>
        /// Gets or sets the current rotation
        /// </summary>
        public double Rotation { get; set; }

        /// <summary>
        /// Gets or sets the current text to display
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Draws the text to the screen
        /// </summary>
        public void Blit()
        {
            GL.Color3(this.Color);
            GL.BindTexture(TextureTarget.Texture2D, Texture.Handle);
            GL.PushMatrix();

            GL.Translate(this.X, this.Y, 0);
            double aspectRatio = this.ComputeAspectRatio();
            GL.Scale(aspectRatio * this.Height, this.Height, this.Height);
            GL.Rotate(this.Rotation, 0, 0, 1);

            GL.Begin(PrimitiveType.Quads);

            double offsetX = 0;

            foreach (var ch in this.Text)
            {
                this.WriteCharacter(ch, offsetX);
                offsetX += .75;
            }

            GL.End();
            GL.PopMatrix();
            GL.Color3(Color.White);
        }

        /// <summary>
        /// For scaling correctly
        /// </summary>
        /// <returns>the windows aspect ration for scaling</returns>
        private double ComputeAspectRatio()
        {
            int[] viewport = new int[4];
            GL.GetInteger(GetPName.Viewport, viewport);
            int width = viewport[2];
            int height = viewport[3];
            double aspectRatio = (float)height / (float)width;
            return aspectRatio;
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

            GL.TexCoord2(left, top);
            GL.Vertex2(offsetX, 1);
            GL.TexCoord2(right, top);
            GL.Vertex2(offsetX + 1, 1);
            GL.TexCoord2(right, bottom);
            GL.Vertex2(offsetX + 1, 0);
            GL.TexCoord2(left, bottom);
            GL.Vertex2(offsetX, 0);
        }
    }
}
