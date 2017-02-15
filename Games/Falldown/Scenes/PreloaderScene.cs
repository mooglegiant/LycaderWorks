//-----------------------------------------------------------------------
// <copyright file="PreloaderScene.cs" company="Mooglegiant" >
//      Copyright (c) Mooglegiant. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Falldown.Scenes
{
    using OpenTK;
    using OpenTK.Input;

    using Lycader;
    using Lycader.Utilities;

    /// <summary>
    /// Preload assets screen
    /// </summary>
    public class PreloaderScene : IScene
    {
        /// <summary>
        /// Initializes a new instance of the PreloaderScene class
        /// </summary>
        public PreloaderScene()
        {
        }

        public void Load()
        {
            ContentBuffer.AddTexture(FileFinder.Find("Assets", "ImageFonts"));
            ContentBuffer.AddTexture(FileFinder.Find("Assets", "Images"));
            ContentBuffer.AddAudio(FileFinder.Find("Assets", "Sounds"));
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
            Lycader.ContentBuffer.Process(10);
            if (Lycader.ContentBuffer.IsQueueEmpty())
            {
                LycaderEngine.ChangeScene(new IntroScreen());
            }

            if (InputManager.IsKeyPressed(Key.Escape))
            {
                LycaderEngine.Screen.Exit();
            }

            if (InputManager.IsKeyPressed(Key.F11))
            {
                LycaderEngine.Screen.ToggleFullScreen();
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
