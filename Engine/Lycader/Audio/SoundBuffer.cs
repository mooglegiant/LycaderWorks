//-----------------------------------------------------------------------
// <copyright file="Sound.cs" company="Mooglegiant" >
//      Copyright (c) Mooglegiant. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Lycader.Audio
{
    using System;

    using OpenTK.Audio.OpenAL;

    /// <summary>
    /// Holds basic information about the a sound block
    /// </summary>
    public class SoundBuffer : IDisposable
    {
        /// <summary>
        /// Initializes a new instance of the Sound class
        /// </summary>
        public SoundBuffer()
        {
            this.Buffer = AL.GenBuffer();
        }

        /// <summary>
        /// Finalizes an instance of the Sound class
        /// </summary>
        ~SoundBuffer()
        {
            this.Dispose();
        }

        /// <summary>
        /// Gets or sets the sound buffer ID
        /// </summary>
        public int Buffer { get; set; }

        /// <summary>
        /// Deletes the sound object from the buffer
        /// </summary>
        public void Dispose()
        {
            if (this.Buffer != 0)
            {
                AL.DeleteBuffer(this.Buffer);
                this.Buffer = 0;
            }
        }
    }
}

