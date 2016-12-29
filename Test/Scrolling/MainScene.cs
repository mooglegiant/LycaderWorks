
namespace Scrolling
{
    using OpenTK;
    using OpenTK.Graphics;
    using OpenTK.Input;

    using Lycader;
    using Lycader.Graphics;
    using Lycader.Graphics.Primitives;
    using Lycader.Input;
    using Lycader.Maps;
    using Lycader.Utilities;
 
    public class MainScene : IScene
    {
        private TileMap map;
        private Camera camera1;

        public MainScene()
        {
        }

        public void Load()
        {
            camera1 = new Camera();


            TextureContent.Load("tiles", FileFinder.Find("Resources", "Sonic.png"));

            this.map = new TileMap();
            this.map.Load(FileFinder.Find("Resources", "Sonic.map"));
            this.map.FlipY();
            this.map.Texture = TextureContent.Get("tiles");
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

            this.map.MoveAll(1, 0, false);
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
