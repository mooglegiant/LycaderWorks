﻿//-----------------------------------------------------------------------
// <copyright file="Texture.cs" company="Mooglegiant" >
//      Copyright (c) Mooglegiant. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Lycader.Graphics
{
    using OpenTK;
    using OpenTK.Graphics.OpenGL;
    using System;

    /// <summary>
    /// Holds basic information about the Texture
    /// </summary>
    public class Texture2D 
    {
        /// <summary>
        /// Gets the current OpenGL texture number
        /// </summary>
        public int Handle { get; internal set; }

        /// <summary>
        /// Gets the height of the texture
        /// </summary>
        public float Height { get; internal set; }

        /// <summary>
        /// Gets the width of the texture
        /// </summary>
        public float Width { get; internal set; }
    }
}