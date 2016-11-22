using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Lycader
{
    public static class LycaderEngine
    {

        public static Game Game { get; set; }

          /// <summary>
        /// Initializes static members of the Config class
        /// </summary>
        static LycaderEngine()
        {
            ScreenWidth = 800;
            ScreenHeight = 600;
            ScreenTitle = "No Title";            
        }


        static public void Initalize()
        {
            Game = new Game();
        }
        static public void Initalize(IScene scene)
        {
            Game = new Game(scene);
        }


        #region Screen Settings
        /// <summary>
        /// Gets or sets the Screen's Width in pixels
        /// </summary>
        public static int ScreenWidth { get; set; }

        /// <summary>
        /// Gets or sets the screen's Height in pixels
        /// </summary>
        public static int ScreenHeight { get; set; }

        /// <summary>
        /// Gets or sets the screen's title name
        /// </summary>
        public static string ScreenTitle { get; set; }

        public static Color BackgroundColor { get; set; } = Color.Black;
        #endregion

        #region Timing Settings
        /// <summary>
        /// Gets or sets the Game's Fps at load (use for timing events)
        /// </summary>
        public static double Fps { get; set; }
        #endregion

        #region Sound Configuration
        /// <summary>
        /// Gets or sets a value indicating whether sound is enabled or not
        /// </summary>
        public static bool SoundEnabled
        {
            get
            {
                return AllowSoundPlayed;
            }

            set
            {
                // If no sound driver is loaded, don't allow sound to be enabled
                if (!HasSoundDevice)
                {
                    AllowSoundPlayed = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether a sound drive is available or not
        /// </summary>
        internal static bool HasSoundDevice { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the sound manager will allow sounds to be played
        /// </summary>
        internal static bool AllowSoundPlayed { get; set; }
        #endregion

        static public bool IsShuttingDown { get; set; } = false;
    }
}
