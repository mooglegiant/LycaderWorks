//-----------------------------------------------------------------------
// <copyright file="Game.cs" company="Mooglegiant" >
//      Copyright (c) Mooglegiant. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace TileMap
{
    using System;
    using System.Drawing;

    using MogAssist;
    using MogAssist.Graphics;
    using MogAssist.Maps;
    using MogAssist.Utilities;

    using OpenTK;
    using OpenTK.Graphics;
    using OpenTK.Graphics.OpenGL;
    using OpenTK.Input;

    /// <summary>
    /// Our game class
    /// </summary>
    public class Game : GameWindow
    {
        /// <summary>
        /// Map to render
        /// </summary>
        private Map map;

        /// <summary>
        /// Player Your Controlling
        /// </summary>
        private Player mario;

        /// <summary>
        /// Initializes a new instance of the Game class
        /// </summary>
        public Game()
            : base(Config.ScreenWidth, Config.ScreenHeight, GraphicsMode.Default, "Test")
        {
            VSync = VSyncMode.On;
        }

        /// <summary>
        /// Overrides our OnLoad event
        /// </summary>
        /// <param name="e">Event Parms</param>
        public override void OnLoad(EventArgs e)
        {
            GL.ClearColor(Color.Black);

            TextureManager.Load("tiles", FileFinder.Find("Resources", "Images", "tiles.png"));

            this.map = new Map();
            this.map.Load(FileFinder.Find("Resources", "Maps", "world.map"));
            this.map.FlipY();
            this.map.Texture = TextureManager.Get("tiles");

            TextureManager.Load("mario-stand", FileFinder.Find("Resources", "Images", "mario-stand.png"));
            TextureManager.Load("mario-run1", FileFinder.Find("Resources", "Images", "mario-run1.png"));
            TextureManager.Load("mario-run2", FileFinder.Find("Resources", "Images", "mario-run2.png"));
            TextureManager.Load("mario-run3", FileFinder.Find("Resources", "Images", "mario-run3.png"));
            TextureManager.Load("mario-jump", FileFinder.Find("Resources", "Images", "mario-jump.png"));
            TextureManager.Load("mario-stop", FileFinder.Find("Resources", "Images", "mario-stop.png"));

            this.mario = new Player();
        }

        /// <summary>
        /// Overrides our OnUnload event
        /// </summary>
        /// <param name="e">Event Parms</param>
        public override void OnUnload(EventArgs e)
        {
            TextureManager.Unload();
        }

        /// <summary>
        /// Overrides our OnResize event
        /// </summary>
        /// <param name="e">Event Parms</param>
        protected override void OnResize(EventArgs e)
        {
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            GL.Ortho(0, this.Width, 0, this.Height, -1, 1);

            Config.ScreenWidth = this.Width;
            Config.ScreenHeight = this.Height;
        }

        /// <summary>
        /// Overrides our OnUpdateFrame event
        /// </summary>
        /// <param name="e">Event Parms</param>
        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            float movement = 4;
            float newX = 0;
            float newY = 0;

            if (Keyboard[Key.Escape])
            {
                Exit();
            }

            if (Keyboard[Key.Right])
            {
                newX += movement;
            }

            if (Keyboard[Key.Left])
            {
                newX -= movement;
            }

            if (Keyboard[Key.Up])
            {
                newY += movement;
            }

            if (Keyboard[Key.Down])
            {
                newY -= movement;
            }

            this.map.MoveAll(newX, newY, false);
            Camera.X += newX;
            Camera.Y += newY;                     
            this.mario.X += newX;
            this.mario.Y += newY;
            this.mario.Update();
        }

        /// <summary>
        /// Overrides our OnRenderFrame event
        /// </summary>
        /// <param name="e">Event Parms</param>
        protected override void OnRenderFrame(FrameEventArgs e)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();

            this.map.Blit(0);
            this.map.Blit(1);
            this.mario.Blit();
            SwapBuffers();
        }
    }
}
