//-----------------------------------------------------------------------
// <copyright file="Player.cs" company="Mooglegiant" >
//      Copyright (c) Mooglegiant. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Animation
{
    using System;

    using MogAssist;
    using MogAssist.Audio;
    using MogAssist.Graphics;

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
    public class Player : Sprite
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
            X = (Config.ScreenWidth / 2) - 30;
            Y = (Config.ScreenHeight / 2) - 39;
            ScaleX = 2;
            ScaleY = 2;
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
            this.Update();
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

            Texture = TextureManager.Get(Animations[(int)this.animationState].GetTexture());
            Animations[(int)this.animationState].Animate();
        }
    }
}
