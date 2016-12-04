//-----------------------------------------------------------------------
// <copyright file="PlayingScreen.cs" company="Mooglegiant" >
//      Copyright (c) Mooglegiant. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Animation.Screens
{
    using MogAssist;
    using MogAssist.Graphics;
    using OpenTK;
    using OpenTK.Input;

    /// <summary>
    /// Playing screenlet
    /// </summary>
    public class PlayingScreen : IScreen
    {
        /// <summary>
        /// The screen's player
        /// </summary>
        private Player player = new Player();

        /// <summary>
        /// Initializes a new instance of the PlayingScreen class
        /// </summary>
        public PlayingScreen()
        {
        }

        /// <summary>
        /// Implements OnUpdateFrame
        /// </summary>
        /// <param name="keyboard">current keyboard</param>
        /// <param name="e">event args</param>
        public void OnUpdateFrame(KeyboardDevice keyboard, FrameEventArgs e)
        {
            this.player.Update();
        }

        /// <summary>
        /// Implements OnRender
        /// </summary>
        /// <param name="e">event args</param>
        public void OnRenderFrame(FrameEventArgs e)
        {
            this.player.Blit();
        }

        /// <summary>
        /// Implements OnKeyDown
        /// </summary>
        /// <param name="key">current key pressed</param>
        public void OnKeyDown(Key key)
        {
        }

        /// <summary>
        /// Implements OnKeyUp
        /// </summary>
        /// <param name="key">current key released</param>
        public void OnKeyUp(Key key)
        {
        }
    }
}
