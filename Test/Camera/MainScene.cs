
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

            TextureManager.Load("ball", FileFinder.Find("Resources", "Images", "ball.png"));
            ball = new Sprites.Ball();
        }

        public void Unload()
        {
            TextureManager.Unload("ball");
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

            if (InputHelper.IsKeyDown(Key.Right))
            {
                cameras[0].WorldSize = new Size(cameras[0].WorldSize.Width + 10, cameras[0].WorldSize.Height);
            }
            if (InputHelper.IsKeyDown(Key.Left))
            {
                cameras[0].WorldSize = new Size(cameras[0].WorldSize.Width - 10, cameras[0].WorldSize.Height);
            }

            //move camera1
            if (InputHelper.IsKeyDown(Key.Up))
            {
                cameras[0].WorldSize = new Size(cameras[0].WorldSize.Width, cameras[0].WorldSize.Height + 10);
            }
            if (InputHelper.IsKeyDown(Key.Down))
            {
                cameras[0].WorldSize = new Size(cameras[0].WorldSize.Width, cameras[0].WorldSize.Height - 10);
            }

            // Ball position
            if (InputHelper.IsKeyDown(Key.D))
            {
                ball.Position += new Vector3(10f, 0f, 0f);
            }
            if (InputHelper.IsKeyDown(Key.A))
            {
                ball.Position -= new Vector3(10f, 0f, 0f);
            }  
            if (InputHelper.IsKeyDown(Key.W))
            {
                ball.Position += new Vector3(0f, 10f, 0f);
            }
            if (InputHelper.IsKeyDown(Key.S))
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
