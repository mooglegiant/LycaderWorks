﻿
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
            if (InputHelper.IsKeyPressed(Key.Escape))
            {
                LycaderEngine.Screen.Exit();
            }

            if (InputHelper.IsKeyPressed(Key.F11))
            {
                if (LycaderEngine.Screen.WindowState == WindowState.Fullscreen)
                    LycaderEngine.Screen.WindowState = WindowState.Normal;
                else
                    LycaderEngine.Screen.WindowState = WindowState.Fullscreen;
            }

           this.camera1.CenterOnSprite(mario);
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
