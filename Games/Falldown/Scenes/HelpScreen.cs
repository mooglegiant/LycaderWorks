//-----------------------------------------------------------------------
// <copyright file="HelpScreen.cs" company="Mooglegiant" >
//      Copyright (c) Mooglegiant. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Falldown.Scenes
{
    using System;

    using OpenTK;
    using OpenTK.Input;

    using Lycader;
    using Lycader.Entities;

    /// <summary>
    /// Help screen
    /// </summary>
    public class HelpScreen : IScene
    {
        private EntityManager manager = new EntityManager();
        private SpriteEntity logo = new SpriteEntity();
        /// <summary>
        /// Initializes a new instance of the TitleScreen class
        /// </summary>
        public HelpScreen()
        {       
        }

        public void Load()
        {
            logo.Texture = "help.gif";
            this.manager.Add(logo);         
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
            if (InputManager.IsKeyPressed(Key.Enter))
            {
                LycaderEngine.ChangeScene(new TitleScreen());  
            }

            if (InputManager.IsKeyPressed(Key.Escape))
            {
                LycaderEngine.Screen.Exit();
            }

            this.manager.Update();
        }

        /// <summary>
        /// Implements OnRender
        /// </summary>
        /// <param name="e">event args</param>
        public void Draw(FrameEventArgs e)
        {
            this.manager.Render();
        }
    }
}
