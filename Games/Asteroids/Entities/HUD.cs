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
    public class HUD : Entity, IEntity
    {
        SpriteFont score;
        SpriteEntity lives;
        
        /// <summary>
        /// Initializes a new instance of the HUD class
        /// </summary>
        /// <param name="position">Current world position</param>
        public HUD()
            : base(new Vector3(0, 0, 0), 1, 0)
        {
            score = new SpriteFont("font", 20, new Vector3(20, LycaderEngine.Screen.Height - 25, 100));
            lives = new SpriteEntity(new Vector3(20, LycaderEngine.Screen.Height - 60, 100), 1f, 90);

            lives.Texture = "player";
        }


        /// <summary>
        /// Updates the sprite
        /// </summary>
        public override void Update()
        {
            score.DisplayText = Globals.Score.ToString().PadLeft(7, ' ');
        }

        public override bool IsOnScreen(Camera camera)
        {
            return true;
        }

        public override void Draw(Camera camera)
        {
            score.Draw(camera);

            for (int i = 0; i < Globals.Lives; i++)
            {
                lives.Position = new Vector3(120 - (20 * (i + 1)), LycaderEngine.Screen.Height - 60, 100);
                lives.Draw(camera);
            }            
        }
    }
}
