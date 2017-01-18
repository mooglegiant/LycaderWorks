﻿
namespace CollsionTest
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
 
    public class MainScene : IScene
    {

        private List<Sprites.Ball> balls = new List<Sprites.Ball>();
        private Camera camera1;
        private int timer = 0;

        public MainScene()
        {
            camera1 = new Camera();
        }

        public void Load()
        {
            TextureContent.Load("ball", FileFinder.Find("Resources", "Images", "ball.png"));
        }

        public void Unload()
        {
            TextureContent.Unload("ball");
        }

        /// <summary>
        /// Implements OnUpdateFrame
        /// </summary>
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

            if (InputHelper.IsKeyPressed(Key.Space))
            {
                this.balls.Clear();
            }

            timer++;
            if (timer > 10)
            {
                balls.Add(new Sprites.Ball());
                timer = 0;
            }

            foreach (Sprites.Ball ball in balls)
            {
                ball.Update();
            }

            for (int i = 0; i < balls.Count; i++)
            {
                balls[i].Update();

                balls.Where(b => !b.IsDeleted).ToList().ForEach(ball =>
                {
                    if (ball != balls[i])
                    {
                        if (ball.IsColliding(balls[i]))
                        {
                            ball.IsDeleted = true;
                            balls[i].IsDeleted = true;
                        }
                    }
                });
            }

            balls = balls.Where(b => !b.IsDeleted).ToList();
        }

        /// <summary>
        /// Implements OnRender
        /// </summary>
        /// <param name="e">event args</param>
        public void Draw(FrameEventArgs e)
        {
            foreach (Sprites.Ball ball in balls.Where(x => x.IsOnScreen(camera1)))
            {
                ball.Draw(camera1);               
            }
        }       
    }
}
