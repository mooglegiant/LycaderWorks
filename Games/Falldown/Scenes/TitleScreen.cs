//-----------------------------------------------------------------------
// <copyright file="TitleScreen.cs" company="Mooglegiant" >
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
    /// Title screen
    /// </summary>
    public class TitleScreen : IScene
    {
        /// <summary>
        /// The screens text class
        /// </summary>
        private FontEntity pressStart;

        private EntityManager manager = new EntityManager();

        /// <summary>
        /// Initializes a new instance of the TitleScreen class
        /// </summary>
        public TitleScreen()
        {       
        }

        public void Load()
        {
            var background = new SpriteEntity()
            {
                Texture = "title2.gif"
            };

            this.manager.Add(background);

            this.pressStart = new FontEntity("pixel.png", 20, new Vector3(190, 300, 100), 1, "Press Start");
            this.manager.Add(this.pressStart);

            OggStream stream = new OggStream("Assets/Music/menu.ogg");
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
            if (InputManager.IsKeyPressed(Key.Enter))
            {
                //   Globals.NewGame();
                LycaderEngine.ChangeScene(new SelectScreen());               
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
