//-----------------------------------------------------------------------
// <copyright file="FileFinder.cs" company="Mooglegiant" >
//      Copyright (c) Mooglegiant. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Lycader.Utilities
{
    using System;
    using System.IO;

    /// <summary>
    /// Finds an absolute file path
    /// </summary>
    public static class FileFinder
    {
        /// <summary>
        /// Gets or sets the default folder paths to use for all Find calls
        /// </summary>
        public static string DefaultPath { get; set; }

        /// <summary>
        /// Recursivly calls Path.Combine to fine the file path
        /// </summary>
        /// <param name="paths">location to find the file</param>
        /// <returns>full file path</returns>
        public static string Find(params string[] paths)
        {
            string filePath = paths[0];
            if (!string.IsNullOrEmpty(DefaultPath))
            {
                filePath = Path.Combine(DefaultPath, paths[0]);
            }

            for (int i = 1; i < paths.Length; i++)
            {
                filePath = Path.Combine(filePath, paths[i]);
            }

            return filePath;
        }
    }
}
