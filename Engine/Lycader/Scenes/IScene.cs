//-----------------------------------------------------------------------
// <copyright file="IScreen.cs" company="Mooglegiant" >
//      Copyright (c) Mooglegiant. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Lycader
{
    using OpenTK;
    using OpenTK.Input;

    /// <summary>
    /// IScene implementation
    /// </summary>
    public interface IScene
    {
        void Load();

        void Unload();

        /// <summary>
        /// Implements Update
        /// </summary>
        /// <param name="keyboard">current keyboard</param>
        /// <param name="e">event args</param>
        void Update(FrameEventArgs e);

        /// <summary>
        /// Implements Draw
        /// </summary>
        /// <param name="e">event args</param>
        void Draw(FrameEventArgs e);
    }
}
