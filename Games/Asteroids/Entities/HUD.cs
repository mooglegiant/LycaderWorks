//-----------------------------------------------------------------------
// <copyright file="HUD.cs" company="Mooglegiant" >
//      Copyright (c) Mooglegiant. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Asteroids
{
    using Lycader;
    using Lycader.Graphics;
    using OpenTK;

    /// <summary>
    /// A bullet class
    /// </summary>
    public class HUD : SpriteFont
    {

        /// <summary>
        /// Initializes a new instance of the HUD class
        /// </summary>
        /// <param name="position">Current world position</param>
        public HUD(Texture2D texture, int height, Vector3 position, string text = "")
            : base(texture, height, position, text)
        {
            Texture = TextureContent.Find("font");
        }


        /// <summary>
        /// Updates the sprite
        /// </summary>
        public override void Update()
        {
            this.Text = Globals.Score.ToString();
        }
    }
}
