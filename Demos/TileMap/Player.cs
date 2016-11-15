//-----------------------------------------------------------------------
// <copyright file="Player.cs" company="Mooglegiant" >
//      Copyright (c) Mooglegiant. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace TileMap
{
    using MogAssist;
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
        /// Running Animation
        /// </summary>
        Running = 2,

        /// <summary>
        /// Jumping Animation
        /// </summary>
        Jumping = 3,

        /// <summary>
        /// Stopping Animation
        /// </summary>
        Stopping = 4
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
            CreateAnimation(1, false);
            CreateAnimation(2, true);
            CreateAnimation(3, false);
            CreateAnimation(4, false);

            Animations[(int)AnimationStates.Standing].Add("mario-stand", 1);
            Animations[(int)AnimationStates.Running].Add("mario-run1", 20);
            Animations[(int)AnimationStates.Running].Add("mario-run2", 20);
            Animations[(int)AnimationStates.Running].Add("mario-run3", 20);
            Animations[(int)AnimationStates.Jumping].Add("mario-jump", 1);
            Animations[(int)AnimationStates.Stopping].Add("mario-stop", 1);

            this.animationState = AnimationStates.Standing;
            this.Update();
        }

        /// <summary>
        /// Animates the sprite on update
        /// </summary>
        public override void Update()
        {
            Texture = TextureManager.Get(Animations[(int)this.animationState].GetTexture());
            Animations[(int)this.animationState].Animate();
        }
    }
}
