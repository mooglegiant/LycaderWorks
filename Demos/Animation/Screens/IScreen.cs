//-----------------------------------------------------------------------
// <copyright file="IScreen.cs" company="Mooglegiant" >
//      Copyright (c) Mooglegiant. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Animation.Screens
{
    using OpenTK;
    using OpenTK.Input;

    /// <summary>
    /// Implements an IScreen for our game
    /// </summary>
    public interface IScreen
    {
        /// <summary>
        /// Implements OnUpdateFrame
        /// </summary>
        /// <param name="keyboard">current keyboard</param>
        /// <param name="e">event args</param>
        void OnUpdateFrame(KeyboardDevice keyboard, FrameEventArgs e);

        /// <summary>
        /// Implements OnRender
        /// </summary>
        /// <param name="e">event args</param>
        void OnRenderFrame(FrameEventArgs e);

        /// <summary>
        /// Implements OnKeyDown
        /// </summary>
        /// <param name="key">current key pressed</param>
        void OnKeyDown(Key key);

        /// <summary>
        /// Implements OnKeyUp
        /// </summary>
        /// <param name="key">current key released</param>
        void OnKeyUp(Key key);
    }
}
