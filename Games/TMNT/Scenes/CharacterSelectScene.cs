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
        private int tutle_select = 1;

        /// <summary>
        /// Initializes a new instance of the TitleScreen class
        /// </summary>
        public CharacterSelectScene()
        {       
        }

        public void Load()
        {
            TextureManager.Load("turtle_gray", @"Assets\Images\turtles-gray.png", true);
            TextureManager.Load("turtle_color", @"Assets\Images\turtles-color.png", true);
            TextureManager.Load("player_select", @"Assets\Images\player-select.png", true);
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

            if (InputManager.IsKeyReleased(Key.Left))
            {
                tutle_select = Lycader.Math.Calculate.Wrap(tutle_select - 1, 1, 4);
            }

            if (InputManager.IsKeyReleased(Key.Right))
            {
                tutle_select = Lycader.Math.Calculate.Wrap(tutle_select + 1, 1, 4);
            }

            this.manager.Update();
        }

        /// <summary>
        /// Implements Render
        /// </summary>
        /// <param name="e">event args</param>
        public void Draw(FrameEventArgs e)
        {
            for (int i = 1; i <= 4; i++)
            {
                if(i == tutle_select)
                {
                    Render.DrawTile(camera, "player_select", new Vector3((i * 12) + ((i - 1) * 49) + 13, 160, 0), 0, 1, 255, i - 1, 23, 22);
                    Render.DrawTile(camera, "turtle_color", new Vector3((i * 12) + ((i - 1) * 49), 30, 0), 0, 1, 255, i - 1, 49, 128);
                }
                else
                {
                    Render.DrawTile(camera, "turtle_gray", new Vector3((i * 12) + ((i - 1) * 49), 30, 0), 0, 1, 255, i - 1, 49, 128);
                }

            }

            this.manager.Draw();
        }
    }
}
