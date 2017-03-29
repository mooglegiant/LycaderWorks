//-----------------------------------------------------------------------
// <copyright file="IntroScreen.cs" company="Mooglegiant" >
//      Copyright (c) Mooglegiant. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Falldown.Scenes
{
    using System;

    using OpenTK;
    using OpenTK.Input;

    using Lycader;
    using Lycader.Audio;
    using Lycader.Entities;

    /// <summary>
    /// Intro screen
    /// </summary>
    public class IntroScreen : IScene
    {
        private EntityManager manager = new EntityManager();
        private SpriteEntity logo = new SpriteEntity();
        OggStream stream = new OggStream("Assets/Music/playstation_boot.ogg");

        /// <summary>
        /// Initializes a new instance of the TitleScreen class
        /// </summary>
        public IntroScreen()
        {       
        }

        public void Load()
        {
            logo.Texture = "logo.gif";
            logo.Alpha = 0;
            this.manager.Add(logo);
          
            stream.Play();
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
            if (InputManager.IsKeyPressed(Key.Enter) || stream.IsStopped())
            {
                //   Globals.NewGame();
                MusicManager.Unload();
               SceneManager.ChangeScene(new TitleScreen());  
            }

            if (InputManager.IsKeyPressed(Key.Escape))
            {
                Engine.Screen.Exit();
            }

            this.manager.Update();
        }

        /// <summary>
        /// Implements OnRender
        /// </summary>
        /// <param name="e">event args</param>
        public void Draw(FrameEventArgs e)
        {
            logo.Alpha++;
            this.manager.Render();
        }
    }
}
