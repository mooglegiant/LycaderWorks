//-----------------------------------------------------------------------
// <copyright file="Preloader.cs" company="Mooglegiant" >
//      Copyright (c) Mooglegiant. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Asteroids.Scenes
{
    using System;
    using Lycader;
    using Lycader.Graphics;
    using OpenTK;
    using OpenTK.Input;
    using Lycader.Scenes;
    using Lycader.Input;
    using Lycader.Utilities;
    using Lycader.Audio;
    using System.Threading.Tasks;
    using Lycader.Graphics.Primitives;
    using OpenTK.Graphics;

    /// <summary>
    /// Level start screenlet
    /// </summary>
    public class Preloader : IScene
    {
        /// <summary>
        /// Initializes a new instance of the preloader class
        /// </summary>
        public Preloader()
        {
        }

        public void Load()
        {
            AssetQueue.Texture("font", FileFinder.Find("Assets", "Fonts", "defaultfont.png"));

            AssetQueue.Texture("background", FileFinder.Find("Assets", "Images", "background.png"));
            AssetQueue.Texture("ship", FileFinder.Find("Assets", "Images", "ship.png"));
            AssetQueue.Texture("ship_thrust1", FileFinder.Find("Assets", "Images", "ship_acc1.png"));
            AssetQueue.Texture("ship_thrust2", FileFinder.Find("Assets", "Images", "ship_acc2.png"));
            AssetQueue.Texture("bullet", FileFinder.Find("Assets", "Images", "bullet.png"));

            AssetQueue.Texture("asteroid1-1", FileFinder.Find("Assets", "Images", "asteroid-small1.png"));
            AssetQueue.Texture("asteroid1-2", FileFinder.Find("Assets", "Images", "asteroid-small2.png"));
            AssetQueue.Texture("asteroid1-3", FileFinder.Find("Assets", "Images", "asteroid-small3.png"));

            AssetQueue.Texture("asteroid2-1", FileFinder.Find("Assets", "Images", "asteroid-med1.png"));
            AssetQueue.Texture("asteroid2-2", FileFinder.Find("Assets", "Images", "asteroid-med2.png"));
            AssetQueue.Texture("asteroid2-3", FileFinder.Find("Assets", "Images", "asteroid-med3.png"));

            AssetQueue.Texture("asteroid3-1", FileFinder.Find("Assets", "Images", "asteroid-large1.png"));
            AssetQueue.Texture("asteroid3-2", FileFinder.Find("Assets", "Images", "asteroid-large2.png"));
            AssetQueue.Texture("asteroid3-3", FileFinder.Find("Assets", "Images", "asteroid-large3.png"));

            AssetQueue.Audio("sound", FileFinder.Find("Assets", "Sounds", "boop.wav"));
            AssetQueue.Audio("bangLarge", FileFinder.Find("Assets", "Sounds", "bangLarge.wav"));
            AssetQueue.Audio("bangMedium", FileFinder.Find("Assets", "Sounds", "bangMedium.wav"));
            AssetQueue.Audio("bangSmall", FileFinder.Find("Assets", "Sounds", "bangSmall.wav"));
            AssetQueue.Audio("beat1", FileFinder.Find("Assets", "Sounds", "beat1.wav"));
            AssetQueue.Audio("beat2", FileFinder.Find("Assets", "Sounds", "beat2.wav"));
            AssetQueue.Audio("extraShip", FileFinder.Find("Assets", "Sounds", "extraShip.wav"));
       //     AssetQueue.Audio("thrust", FileFinder.Find("Assets", "Sounds", "saucer.wav"));
      //      AssetQueue.Audio("saucer", FileFinder.Find("Assets", "Sounds", "saucer.wav"));
        }

        public void Unload()
        {
   
        }

        /// <summary>
        /// Implements OnUpdateFrame
        /// </summary>
        /// <param name="keyboard">current keyboard</param>
        /// <param name="e">event args</param>
        public void Update(FrameEventArgs e)
        {
            AssetQueue.Process(10);
            if (AssetQueue.IsQueueEmpty())
            {
                Globals.InitializeHUD();
                LycaderEngine.ChangeScene(new TitleScreen());
            }

            if (KeyboardHelper.IsKeyPressed(Key.Escape))
            {
                LycaderEngine.Game.Exit();
            }

            if (KeyboardHelper.IsKeyPressed(Key.F11))
            {
                if (LycaderEngine.Game.WindowState == WindowState.Fullscreen)
                    LycaderEngine.Game.WindowState = WindowState.Normal;
                else
                    LycaderEngine.Game.WindowState = WindowState.Fullscreen;
            }
        }

        /// <summary>
        /// Implements OnRender
        /// </summary>
        /// <param name="e">event args</param>
        public void Draw(FrameEventArgs e)
        {
        }
    }
}
