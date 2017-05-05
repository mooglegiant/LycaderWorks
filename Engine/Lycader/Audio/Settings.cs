using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Audio;

namespace Lycader.Audio
{
    public static class Settings
    {
        /// <summary>
        /// The screen's AudioContext
        /// </summary>
        private static AudioContext audioContext;

        /// <summary>
        /// Gets or sets a value indicating whether sound is enabled or not
        /// </summary>
        public static bool Enabled
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

        public static float MusicVolume { get; set; }

        public static float EffectsVolume { get; set; }


        /// <summary>
        /// Gets or sets a value indicating whether a sound drive is available or not
        /// </summary>
        internal static bool HasSoundDevice { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the sound manager will allow sounds to be played
        /// </summary>
        internal static bool AllowSoundPlayed { get; set; }

        internal static void Initialize()
        {
            AllowSoundPlayed = false;
            HasSoundDevice = false;
            Enabled = false;

            // Make sure we have a sound device available.  If not, do not allow playing of sounds :)
            if (AudioContext.AvailableDevices.Count > 0)
            {
                if (!string.IsNullOrEmpty(AudioContext.AvailableDevices[0]))
                {
                    audioContext = new AudioContext();
                    AllowSoundPlayed = true;
                    HasSoundDevice = true;
                    Enabled = true;
                    MusicVolume = 100;
                    EffectsVolume = 100;
                }
            }
        }
    }
}
