//-----------------------------------------------------------------------
// <copyright file="IScene.cs" company="Mooglegiant" >
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
        void Update(FrameEventArgs e);

        /// <summary>
        /// Implements Draw
        /// </summary>
        void Draw(FrameEventArgs e);
    }
}
