//-----------------------------------------------------------------------
// <copyright file="MainScene.cs" company="Mooglegiant" >
//      Copyright (c) Mooglegiant. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace MapTest
{
    using OpenTK;
    using OpenTK.Graphics;
    using OpenTK.Input;

    using Lycader;
    using Lycader.Entities;
    using Lycader.Utilities;
    using Lycader.Maps;

    public class MainScene : IScene
    {
        private TileMap world;
        private Camera camera1;
        private Player mario;

        public MainScene()
        {
            camera1 = new Camera();
        }

        public void Load()
        {
            TextureManager.Load("tiles", FileFinder.Find("Resources", "Images", "tiles.png"));

            this.world = new TileMap();
            this.world.Load(FileFinder.Find("Resources", "Maps", "world.map"));
            this.world.FlipY();
            this.world.Texture = TextureManager.Find("tiles");

            TextureManager.Load("mario-stand", FileFinder.Find("Resources", "Images", "mario-stand.png"));
            TextureManager.Load("mario-run1", FileFinder.Find("Resources", "Images", "mario-run1.png"));
            TextureManager.Load("mario-run2", FileFinder.Find("Resources", "Images", "mario-run2.png"));
            TextureManager.Load("mario-run3", FileFinder.Find("Resources", "Images", "mario-run3.png"));
            TextureManager.Load("mario-jump", FileFinder.Find("Resources", "Images", "mario-jump.png"));
            TextureManager.Load("mario-stop", FileFinder.Find("Resources", "Images", "mario-stop.png"));

            mario = new Player();    
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
                LycaderEngine.Screen.Exit();
            }

            if (InputManager.IsKeyDown(Key.Up))
            {
                this.mario.Position += new Vector3(0, 3, 0);
            }

            if (InputManager.IsKeyDown(Key.Down))
            {
                this.mario.Position += new Vector3(0, -3, 0);
            }

            if (InputManager.IsKeyDown(Key.Left))
            {
                this.mario.Position += new Vector3(-3, 0, 0);
            }

            if (InputManager.IsKeyDown(Key.Right))
            {
                this.mario.Position += new Vector3(3, 0, 0);
            }

            if (InputManager.IsKeyPressed(Key.F11))
            {
                if (LycaderEngine.Screen.WindowState == WindowState.Fullscreen)
                    LycaderEngine.Screen.WindowState = WindowState.Normal;
                else
                    LycaderEngine.Screen.WindowState = WindowState.Fullscreen;
            }

           this.camera1.CenterOnSprite(mario, 0, System.Math.Max(LycaderEngine.Resolution.Width / 2, this.world.Layers[0].Width * 32), 0, LycaderEngine.Resolution.Height / 2);
           this.mario.Update();
        }

        /// <summary>
        /// Implements OnRender
        /// </summary>
        /// <param name="e">event args</param>
        public void Draw(FrameEventArgs e)
        {
            this.world.Draw(camera1);          
            this.mario.Draw(camera1);
        }
    }
}
