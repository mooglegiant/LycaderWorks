﻿//-----------------------------------------------------------------------
// <copyright file="HUD.cs" company="Mooglegiant" >
//      Copyright (c) Mooglegiant. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Asteroids
{
    using System.Collections.Generic;

    using Lycader;
    using Lycader.Entities;
    using Lycader.Graphics;
    using OpenTK;

    /// <summary>
    /// A hud display class
    /// </summary>
    public class HUD : Entity, IEntity
    {
        FontEntity score;
        SpriteEntity lives;

        /// <summary>
        /// Initializes a new instance of the HUD class
        /// </summary>
        /// <param name="position">Current world position</param>
        public HUD()
            : base(new Vector3(0, 0, 0), 1, 0)
        {
            this.score = new FontEntity("font", 20, new Vector3(20, Engine.Screen.Height - 25, 100), .75f);
            this.lives = new SpriteEntity(new Vector3(20, Engine.Screen.Height - 60, 100), 1f, 90);

            this.lives.Texture = "player";
            this.lives.Animations.Add("idle", new Animation(false, new List<AnimationFrame>() { new AnimationFrame(0, 0) }));
            this.lives.CurrentAnimation = "idle";
            this.lives.TileSize = new System.Drawing.Rectangle(0, 0, 32, 32);
        }

        /// <summary>
        /// Updates the sprite
        /// </summary>
        public override void Update()
        {
            this.score.Text = Globals.Score.ToString().PadLeft(7, ' ');
        }

        public override bool IsOnScreen(Camera camera)
        {
            return true;
        }

        public override void Draw(Camera camera)
        {
            this.score.Draw(camera);

            for (int i = 0; i < Globals.Lives; i++)
            {
                this.lives.Position = new Vector3(120 - (20 * (i + 1)), Engine.Screen.Height - 60, 100);
                this.lives.Draw(camera);
            }            
        }
    }
}
