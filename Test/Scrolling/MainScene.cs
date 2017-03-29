//-----------------------------------------------------------------------
// <copyright file="MainScene.cs" company="Mooglegiant" >
//      Copyright (c) Mooglegiant. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Scrolling
{
    using OpenTK;
    using OpenTK.Graphics;
    using OpenTK.Input;

    using Lycader;
    using Lycader.Entities;
    using Lycader.Maps;
    using Lycader.Utilities;
 
    public class MainScene : IScene
    {
        private Map map;
        private Camera camera1;

        public MainScene()
        {
        }

        public void Load()
        {
            camera1 = new Camera();


            TextureManager.Load("tiles", FileFinder.Find("Resources", "Sonic.png"));

            this.map = new Map();
            this.map.Load(FileFinder.Find("Resources", "Sonic.map"));
            this.map.FlipY();
            this.map.Texture = TextureManager.Find("tiles");
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
            if (InputManager.IsKeyPressed(Key.Escape))
            {
                Engine.Screen.Exit();
            }

            if (InputManager.IsKeyPressed(Key.F11))
            {
                if (Engine.Screen.WindowState == WindowState.Fullscreen)
                    Engine.Screen.WindowState = WindowState.Normal;
                else
                    Engine.Screen.WindowState = WindowState.Fullscreen;
            }

            if (InputManager.IsKeyDown(Key.Right))
            {
                this.map.MoveAll(-1, 0, false);
            }
        }

        /// <summary>
        /// Implements OnRender
        /// </summary>
        /// <param name="e">event args</param>
        public void Draw(FrameEventArgs e)
        {
            for (int i = 0; i < this.map.Layers.Count; i++)
            {
                this.map.Draw(i, camera1);
            }
        }
    }
}
