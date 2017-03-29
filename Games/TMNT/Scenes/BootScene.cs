//-----------------------------------------------------------------------
// <copyright file="BootScene.cs" company="Mooglegiant" >
//      Copyright (c) Mooglegiant. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace TMNT.Scenes
{
    using System;

    using OpenTK;
    using OpenTK.Input;

    using Lycader;
    using Lycader.Audio;
    using Lycader.Entities;

    /// <summary>
    /// Boot Scene
    /// </summary>
    public class BootScene : IScene
    {
        private EntityManager manager = new EntityManager();
        private SpriteEntity logo = new SpriteEntity();
        OggStream stream = new OggStream("Assets/Music/playstation_boot.ogg");

        /// <summary>
        /// Initializes a new instance of the TitleScreen class
        /// </summary>
        public BootScene()
        {       
        }

        public void Load()
        {
            logo.Texture = "logo.png";
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
               SceneManager.ChangeScene(new TitleScene());  
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
