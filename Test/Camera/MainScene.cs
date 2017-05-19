//-----------------------------------------------------------------------
// <copyright file="MainScene.cs" company="Mooglegiant" >
//      Copyright (c) Mooglegiant. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace CameraTest
{
    using System.Collections.Generic;
    using System.Linq;

    using OpenTK;
    using OpenTK.Graphics;
    using OpenTK.Input;

    using Lycader;
    using Lycader.Graphics;
    using Lycader.Utilities;

    public class MainScene : IScene
    {
        private Sprites.Ball ball_1;
        private Sprites.Ball ball_2;
        private List<Camera> cameras = new List<Camera>();

        public MainScene()
        {
        }

        public void Load()
        {
            cameras.Add(new Camera(new System.Drawing.Point(0, 0), new System.Drawing.Size(400, 300), new System.Drawing.PointF(0, 0)) { Order = 1 });
            cameras.Add(new Camera(new System.Drawing.Point(300, 200), new System.Drawing.Size(400, 300), new System.Drawing.PointF(0, 0)) { Order = 2 });

            TextureManager.Load("ball", FileFinder.Find("Resources", "Images", "ball.png"));
            ball_1 = new Sprites.Ball();
            ball_2 = new Sprites.Ball();
            ball_2.Position = new Vector3(10f, 10f, 1f);
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

            // Ball position
            if (InputManager.IsKeyDown(Key.D))
            {
                ball_1.Position += new Vector3(10f, 0f, 0f);
            }
            if (InputManager.IsKeyDown(Key.A))
            {
                ball_1.Position -= new Vector3(10f, 0f, 0f);
            }  
            if (InputManager.IsKeyDown(Key.W))
            {
                ball_1.Position += new Vector3(0f, 10f, 0f);
            }
            if (InputManager.IsKeyDown(Key.S))
            {
                ball_1.Position -= new Vector3(0f, 10f, 0f);
            }

            ball_1.Update();         
        }

        /// <summary>
        /// Implements OnRender
        /// </summary>
        /// <param name="e">event args</param>
        public void Draw(FrameEventArgs e)
        {
            foreach (Camera camera in cameras.OrderBy(c => c.Order))
            {
                camera.BeginDraw();

                if (ball_1.IsOnScreen(camera))
                {
                    ball_1.Draw(camera);
                }

                if (ball_2.IsOnScreen(camera))
                {
                    ball_2.Draw(camera);
                }


                //Draw a black background and red border frame
               Render.DrawQuad(camera, new Vector3(1, 0, 0), camera.WorldView.Width - 1, camera.WorldView.Height - 1, Color4.Red, 1.0f, DrawType.Outline);
               
            }
        }
    }
}
