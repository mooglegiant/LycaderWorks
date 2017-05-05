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
    using Lycader.Graphics;
    using Lycader.Entities;

    /// <summary>
    /// Boot Scene
    /// </summary>
    public class CharacterSelectScene : IScene
    {
        private EntityManager manager = new EntityManager();
        private Camera camera = new Camera();

        /// <summary>
        /// Initializes a new instance of the TitleScreen class
        /// </summary>
        public CharacterSelectScene()
        {       
        }

        public void Load()
        {
            TextureManager.Load("turtle_color", "Assets\\Images\\turtles-color.png");
            TextureManager.Load("turtle_color", "Assets\\Images\\turtles-color.png", 48, 128);
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
                SceneManager.ChangeScene(new TitleScene());  
            }

            if (InputManager.IsKeyPressed(Key.Escape))
            {
                Engine.Screen.Exit();
            }

            this.manager.Update();
        }

        /// <summary>
        /// Implements Render
        /// </summary>
        /// <param name="e">event args</param>
        public void Draw(FrameEventArgs e)
        {
            Render.DrawTexture(camera, "turtle_color_1", new Vector3(0, 0, 0), 0, 1, 255);
            Render.DrawTexture(camera, "turtle_color_2", new Vector3(50, 0, 0), 0, 1, 255);
            Render.DrawTexture(camera, "turtle_color_3", new Vector3(100, 0, 0), 0, 1, 255);
            Render.DrawTexture(camera, "turtle_color_4", new Vector3(150, 0, 0), 0, 1, 255);

            this.manager.Draw();
        }
    }
}
