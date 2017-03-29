//-----------------------------------------------------------------------
// <copyright file="AnimationFrame.cs" company="Mooglegiant" >
//      Copyright (c) Mooglegiant. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Lycader.Graphics
{
    /// <summary>
    /// Classed used to hold texture and timing information
    /// </summary>
    internal class AnimationFrame
    {
        /// <summary>
        /// Initializes a new instance of the AnimationFrame class
        /// </summary>
        /// <param name="key">TextureManager texture's key</param>
        /// <param name="counter">frame count until next texture</param>
        public AnimationFrame(string key, int counter)
        {
            this.Key = key;
            this.Counter = counter;
        }

        /// <summary>
        /// Gets or sets the current texture key in the TextureManager
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// Gets or sets the current counter to wait until next frame
        /// </summary>
        public int Counter { get; set; }
    }
}
