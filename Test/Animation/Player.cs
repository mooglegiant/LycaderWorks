//-----------------------------------------------------------------------
// <copyright file="Player.cs" company="Mooglegiant" >
//      Copyright (c) Mooglegiant. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Animation
{
    using System;

    using OpenTK;

    using Lycader;
    using Lycader.Entities;

    /// <summary>
    /// Player's animation states
    /// </summary>
    public enum AnimationStates : int
    {
        /// <summary>
        /// Standing Animation
        /// </summary>
        Standing = 1,

        /// <summary>
        /// Looping Animation
        /// </summary>
        Loop = 2
    }

    /// <summary>
    /// The player sprite
    /// </summary>
    public class Player : SpriteEntity
    {
        /// <summary>
        /// Current player state
        /// </summary>
        private AnimationStates animationState;

        /// <summary>
        /// Initializes a new instance of the Player class
        /// </summary>
        public Player()
        {
            this.Position = new Vector3((LycaderEngine.Resolution.Width / 2) - 30, (LycaderEngine.Resolution.Height / 2) - 39, 1);

            this.Zoom = 2;

            this.animationState = AnimationStates.Standing;

            CreateAnimation((int)AnimationStates.Standing, false);
            Animations[(int)AnimationStates.Standing].Add("sonic", 50);

            CreateAnimation((int)AnimationStates.Loop, true);
            Animations[(int)AnimationStates.Loop].Add("sonic-stare", 50);
            Animations[(int)AnimationStates.Loop].Add("sonic-watch", 55);
            Animations[(int)AnimationStates.Loop].Add("sonic-tap", 15);
            Animations[(int)AnimationStates.Loop].Add("sonic-stare", 15);
            Animations[(int)AnimationStates.Loop].Add("sonic-tap", 15);
            Animations[(int)AnimationStates.Loop].Add("sonic-stare", 50);
        }

        /// <summary>
        /// Animates the sprite on update
        /// </summary>
        public override void Update()
        {
            if (this.animationState == AnimationStates.Standing)
            {
                if (Animations[(int)this.animationState].IsComplete)
                {
                    this.animationState = AnimationStates.Loop;
                }
            }

            Texture = Animations[(int)this.animationState].GetTexture();
            Animations[(int)this.animationState].Animate();
        }
    }
}
