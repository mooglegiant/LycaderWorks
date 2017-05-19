//-----------------------------------------------------------------------
// <copyright file="AnimationFrame.cs" company="Mooglegiant" >
//      Copyright (c) Mooglegiant. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Lycader.Graphics
{
    /// <summary>
    /// Class used to hold texture and timing information
    /// </summary>
    public class AnimationFrame
    {
        /// <summary>
        /// Initializes a new instance of the AnimationFrame class
        /// </summary>
        /// <param name="tile">Texture's tile ID</param>
        /// <param name="counter">frame count until next texture</param>
        public AnimationFrame(int tile, int counter)
        {
            this.Tile = tile;
            this.Counter = counter;
        }

        /// <summary>
        /// Gets or sets the current texture key in the TextureManager
        /// </summary>
        public int Tile { get; set; }

        /// <summary>
        /// Gets or sets the current counter to wait until next frame
        /// </summary>
        public int Counter { get; set; }
    }
}
