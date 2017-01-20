//-----------------------------------------------------------------------
// <copyright file="HUD.cs" company="Mooglegiant" >
//      Copyright (c) Mooglegiant. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Asteroids
{
    using Lycader;
    using Lycader.Entities;
    using OpenTK;

    /// <summary>
    /// A bullet class
    /// </summary>
    public class HUD : Entity
    {
        SpriteFont score;
        
        /// <summary>
        /// Initializes a new instance of the HUD class
        /// </summary>
        /// <param name="position">Current world position</param>
        public HUD()
            : base(new Vector3(0, 0, 0), 1, 1)
        {
            score = new SpriteFont(TextureManager.Find("font"), 20, new Vector3(20, LycaderEngine.Screen.Height - 25, 100));
        }


        /// <summary>
        /// Updates the sprite
        /// </summary>
        public override void Update()
        {
            score.Text = Globals.Score.ToString("d7");
        }

        public override bool IsOnScreen(Camera camera)
        {
            return true;
        }

        public override void Draw(Camera camera)
        {
            score.Draw(camera);
        }
    }
}
