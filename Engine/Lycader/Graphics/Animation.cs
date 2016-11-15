//-----------------------------------------------------------------------
// <copyright file="Animation.cs" company="Mooglegiant" >
//      Copyright (c) Mooglegiant. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Lycader.Graphics
{
    using System.Collections.Generic;

    /// <summary>
    /// Holds information about animating a sprite
    /// </summary>
    public class Animation
    {
        /// <summary>
        /// List of all the queued animations
        /// </summary>
        private List<AnimationFrame> frames;

        /// <summary>
        /// Does the animation loop or not
        /// </summary>
        private bool loop;

        /// <summary>
        /// Time remaining on current frame
        /// </summary>
        private int frameCounter;

        /// <summary>
        /// The animation index
        /// </summary>
        private int index;

        /// <summary>
        /// Initializes a new instance of the Animation class
        /// </summary>
        /// <param name="loop">Does this animation loop or not</param>
        public Animation(bool loop)
        {
            this.frames = new List<AnimationFrame>();
            this.loop = loop;
            this.index = 0;
        }

        /// <summary>
        /// Gets a value indicating whether the animation is complete or not
        /// </summary>
        public bool IsComplete { get; private set; }

        /// <summary>
        /// Adds a frame to our animation
        /// </summary>
        /// <param name="key">texture name to display</param>
        /// <param name="counter">frame counter to wait until next texture</param>
        public void Add(string key, int counter)
        {
            if (this.frames.Count == 0)
            {
                this.frameCounter = counter;
            }

            this.frames.Add(new AnimationFrame(key, counter));
        }

        /// <summary>
        /// Retreive the key for the current frame texture
        /// </summary>
        /// <returns>key for the frame in the texture manager</returns>
        public string GetTexture()
        {
            return this.frames[this.index].Key;
        }

        /// <summary>
        /// Animates the sprite
        /// </summary>
        public void Animate()
        {
            if (this.frameCounter > 0)
            {
                this.IsComplete = false;
                this.frameCounter--;

                if (this.frameCounter == 0)
                {
                    if (this.index == this.frames.Count - 1)
                    {
                        if (this.loop)
                        {
                            this.index = 0;
                            this.SetFrame();
                        }
                        else
                        {
                            this.IsComplete = true;
                        }
                    }
                    else
                    {
                        this.index++;
                        this.SetFrame();
                    }
                }
            }
        }

        /// <summary>
        /// Resets the current Animation;
        /// </summary>
        public void Reset()
        {
            this.index = 0;
            this.SetFrame();
        }

        /// <summary>
        /// Moves to the next frame
        /// </summary>
        private void SetFrame()
        {
            this.frameCounter = this.frames[this.index].Counter;
        }
    }
}
