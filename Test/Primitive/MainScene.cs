
namespace PrimitiveTest
{
    using OpenTK;
    using OpenTK.Graphics;
    using OpenTK.Input;

    using Lycader;
    using Lycader.Graphics;
    using Lycader.Graphics.Primitives;
    using Lycader.Input;
    using Lycader.Utilities;
 
    public class MainScene : IScene
    {
        private CircleEntity circle1 = new CircleEntity(new Vector3(20, 20, 0f), 20, Color4.Red, DrawType.Solid, 1f);
        private CircleEntity circle2 = new CircleEntity(new Vector3(30, 20, 1f), 20, Color4.Blue, DrawType.Solid, 1f);
        private LineEntity line = new LineEntity(new Vector3(250, 0, 0), new Vector3(250, 250, 0), Color4.Green, 4);
        private LineEntity line2 = new LineEntity(new Vector3(300, 0, 0), new Vector3(300, 250, 0), Color4.Green, 1);

        private Camera camera1 = new Camera();   

        public MainScene()
        {
            camera1.ViewPort = new Box2(0, LycaderEngine.ScreenHeight, LycaderEngine.ScreenWidth, 0);     
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
        }

        /// <summary>
        /// Implements OnRender
        /// </summary>
        /// <param name="e">event args</param>
        public void Draw(FrameEventArgs e)
        {
            circle1.Draw(camera1);
            circle2.Draw(camera1);
            line.Draw(camera1);
            line2.Draw(camera1);
        }
    }
}
