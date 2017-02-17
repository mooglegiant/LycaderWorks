//-----------------------------------------------------------------------
// <copyright file="LevelScreen.cs" company="Mooglegiant" >
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
    /// Level screen
    /// </summary>
    public class LevelScreen : IScene
    {
        private EntityManager manager = new EntityManager();

        /// <summary>
        /// Initializes a new instance of the TitleScreen class
        /// </summary>
        public LevelScreen()
        {       

        }

        public void Load()
        {
            var background = new SpriteEntity()
            {
                Texture = "background.gif"
            };

            this.manager.Add(background);

            this.manager.Add(new Ball());
        }

        public void Unload()
        {
            Globals.Scores.Save();
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
                Globals.IsPaused = !Globals.IsPaused;
            }
        
            if (InputManager.IsKeyPressed(Key.Escape))
            {
                LycaderEngine.Screen.Exit();
            }

            if (InputManager.IsKeyPressed(Key.F11))
            {
                LycaderEngine.Screen.ToggleFullScreen();
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
