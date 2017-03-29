//-----------------------------------------------------------------------
// <copyright file="PreloaderScene.cs" company="Mooglegiant" >
//      Copyright (c) Mooglegiant. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Asteroids.Scenes
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
            ContentBuffer.AddTexture("font", FileFinder.Find("Assets", "Fonts", "defaultfont.png"));

            ContentBuffer.AddTexture("background", FileFinder.Find("Assets", "Images", "background.png"));
            ContentBuffer.AddTexture("player", FileFinder.Find("Assets", "Images", "player.png"));
            ContentBuffer.AddTexture("player_thrust1", FileFinder.Find("Assets", "Images", "player_thrust1.png"));
            ContentBuffer.AddTexture("player_thrust2", FileFinder.Find("Assets", "Images", "player_thrust2.png"));
            ContentBuffer.AddTexture("bullet", FileFinder.Find("Assets", "Images", "bullet.png"));
            ContentBuffer.AddTexture("ship", FileFinder.Find("Assets", "Images", "ship.png"));

            ContentBuffer.AddTexture("asteroid1-1", FileFinder.Find("Assets", "Images", "asteroid-small1.png"));
            ContentBuffer.AddTexture("asteroid1-2", FileFinder.Find("Assets", "Images", "asteroid-small2.png"));
            ContentBuffer.AddTexture("asteroid1-3", FileFinder.Find("Assets", "Images", "asteroid-small3.png"));

            ContentBuffer.AddTexture("asteroid2-1", FileFinder.Find("Assets", "Images", "asteroid-med1.png"));
            ContentBuffer.AddTexture("asteroid2-2", FileFinder.Find("Assets", "Images", "asteroid-med2.png"));
            ContentBuffer.AddTexture("asteroid2-3", FileFinder.Find("Assets", "Images", "asteroid-med3.png"));

            ContentBuffer.AddTexture("asteroid3-1", FileFinder.Find("Assets", "Images", "asteroid-large1.png"));
            ContentBuffer.AddTexture("asteroid3-2", FileFinder.Find("Assets", "Images", "asteroid-large2.png"));
            ContentBuffer.AddTexture("asteroid3-3", FileFinder.Find("Assets", "Images", "asteroid-large3.png"));

            Lycader.ContentBuffer.AddAudio(FileFinder.Find("Assets", "Sounds"));
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
            ContentBuffer.Process(10);
            if (ContentBuffer.IsQueueEmpty())
            {
               SceneManager.ChangeScene(new TitleScreen());
            }

            if (InputManager.IsKeyPressed(Key.Escape))
            {
                Engine.Screen.Exit();
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
