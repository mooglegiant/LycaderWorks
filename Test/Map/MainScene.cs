
namespace MapTest
{
    using OpenTK;
    using OpenTK.Graphics;
    using OpenTK.Input;

    using Lycader;
    using Lycader.Graphics;
    using Lycader.Graphics.Primitives;
    using Lycader.Input;
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
            TextureContent.Load("tiles", FileFinder.Find("Resources", "Images", "tiles.png"));

            this.world = new TileMap();
            this.world.Load(FileFinder.Find("Resources", "Maps", "world.map"));
            this.world.FlipY();
            this.world.Texture = TextureContent.Find("tiles");

            TextureContent.Load("mario-stand", FileFinder.Find("Resources", "Images", "mario-stand.png"));
            TextureContent.Load("mario-run1", FileFinder.Find("Resources", "Images", "mario-run1.png"));
            TextureContent.Load("mario-run2", FileFinder.Find("Resources", "Images", "mario-run2.png"));
            TextureContent.Load("mario-run3", FileFinder.Find("Resources", "Images", "mario-run3.png"));
            TextureContent.Load("mario-jump", FileFinder.Find("Resources", "Images", "mario-jump.png"));
            TextureContent.Load("mario-stop", FileFinder.Find("Resources", "Images", "mario-stop.png"));

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
            if (KeyboardHelper.IsKeyPressed(Key.Escape))
            {
                LycaderEngine.Game.Exit();
            }

            if (KeyboardHelper.IsKeyPressed(Key.F11))
            {
                if (LycaderEngine.Game.WindowState == WindowState.Fullscreen)
                    LycaderEngine.Game.WindowState = WindowState.Normal;
                else
                    LycaderEngine.Game.WindowState = WindowState.Fullscreen;
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
