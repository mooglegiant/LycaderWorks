//-----------------------------------------------------------------------
// <copyright file="TitleScene.cs" company="Mooglegiant" >
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
    using OpenTK.Graphics;

    /// <summary>
    /// Title Scene
    /// </summary>
    public class TitleScene : IScene
    {
        private EntityManager manager = new EntityManager();
        private SpriteEntity logo = new SpriteEntity();
        private SpriteEntity city = new SpriteEntity();
        private string audioFile = "Assets/Music/title.ogg";

        /// <summary>
        /// Initializes a new instance of the TitleScreen class
        /// </summary>
        public TitleScene()
        {
            logo.Position = new Vector3(32, 300, 1);

            logo.Texture = "title.png";           
            this.manager.Add(logo);

            city.Texture = "city.png";
            this.manager.Add(city);

            FontEntity pressStart = new FontEntity("DOS.png", 12, new Vector3(60, 50, 99), 1f, "Press Start" );
            pressStart.Color = Color4.White;
            pressStart.BackgroundColor = Color4.Black;
            pressStart.Padding = new System.Drawing.PointF(3, 10);

            this.manager.Add(pressStart);

            MusicManager.Add(audioFile);
            MusicManager.Find(audioFile).Play();
        }

        public void Load()
        {
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
                MusicManager.Unload();
               // LycaderEngine.Scenes.ChangeScene(new TitleScreen());  
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
            if (logo.Position.Y > 150)
            {
                logo.Position -= new Vector3(0, .5f, 0);
            }

            this.manager.Draw();
        }
    }
}
