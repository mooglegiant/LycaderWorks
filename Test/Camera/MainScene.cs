
namespace CameraTest
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    using OpenTK;
    using OpenTK.Graphics;
    using OpenTK.Input;

    using Lycader;
    using Lycader.Graphics;

    using Lycader.Input;
    using Lycader.Utilities;
    using System.Drawing;

    public class MainScene : IScene
    {
        private Sprites.Ball ball;
        private List<Camera> cameras = new List<Camera>();

        public MainScene()
        {
        }

        public void Load()
        {
            cameras.Add(new Camera(new System.Drawing.Point(0, 0), new System.Drawing.Size(800, 300), new System.Drawing.PointF(0, 0)) { Order = 1 });
            cameras.Add(new Camera(new System.Drawing.Point(100, 300), new System.Drawing.Size(800, 300), new System.Drawing.PointF(0, 0)) { Order = 2 });

            TextureContent.Load("ball", FileFinder.Find("Resources", "Images", "ball.png"));
            ball = new Sprites.Ball();
        }

        public void Unload()
        {
            TextureContent.Unload("ball");
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

            if (KeyboardHelper.IsKeyHeld(Key.Right))
            {
                cameras[0].WorldSize = new Size(cameras[0].WorldSize.Width + 10, cameras[0].WorldSize.Height);
            }
            if (KeyboardHelper.IsKeyHeld(Key.Left))
            {
                cameras[0].WorldSize = new Size(cameras[0].WorldSize.Width - 10, cameras[0].WorldSize.Height);
            }

            //move camera1
            if (KeyboardHelper.IsKeyHeld(Key.Up))
            {
                cameras[0].WorldSize = new Size(cameras[0].WorldSize.Width, cameras[0].WorldSize.Height + 10);
            }
            if (KeyboardHelper.IsKeyHeld(Key.Down))
            {
                cameras[0].WorldSize = new Size(cameras[0].WorldSize.Width, cameras[0].WorldSize.Height - 10);
            }

            // Ball position
            if (KeyboardHelper.IsKeyHeld(Key.D))
            {
                ball.Position += new Vector3(10f, 0f, 0f);
            }
            if (KeyboardHelper.IsKeyHeld(Key.A))
            {
                ball.Position -= new Vector3(10f, 0f, 0f);
            }  
            if (KeyboardHelper.IsKeyHeld(Key.W))
            {
                ball.Position += new Vector3(0f, 10f, 0f);
            }
            if (KeyboardHelper.IsKeyHeld(Key.S))
            {
                ball.Position -= new Vector3(0f, 10f, 0f);
            }

            ball.Update();         
        }

        /// <summary>
        /// Implements OnRender
        /// </summary>
        /// <param name="e">event args</param>
        public void Draw(FrameEventArgs e)
        {
            foreach (Camera camera in cameras.OrderBy(c => c.Order))
            {
                if (ball.IsOnScreen(camera))
                {
                    ball.Draw(camera);
                }
            }                                                
        }
    }
}
