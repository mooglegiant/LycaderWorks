//-----------------------------------------------------------------------
// <copyright file="Player.cs" company="Mooglegiant" >
//      Copyright (c) Mooglegiant. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Animation
{
    using OpenTK;

    using Lycader;
    using Lycader.Entities;
    using Lycader.Graphics;

    /// <summary>
    /// The player sprite
    /// </summary>
    public class Player : SpriteEntity
    {
        /// <summary>
        /// Initializes a new instance of the Player class
        /// </summary>
        public Player()
        {
            this.Position = new Vector3((Engine.Resolution.Width / 2) - 30, (Engine.Resolution.Height / 2) - 39, 1);

            this.Zoom = 2;
            this.TileSize = new System.Drawing.Rectangle(0, 0, 30, 39);

            var standing = new Animation(true);
            standing.Add(0, 180);
            standing.Add(1, 120);
            standing.Add(3, 120);
            standing.Add(1, 15);
            standing.Add(2, 15);
            standing.Add(1, 15);
            standing.Add(2, 15);
            standing.Add(1, 120);

            Animations.Add("standing", standing);

            this.CurrentAnimation = "standing";
        }

        /// <summary>
        /// Animates the sprite on update
        /// </summary>
        public override void Update()
        {
            Animations[this.CurrentAnimation].Animate();
        }
    }
}
