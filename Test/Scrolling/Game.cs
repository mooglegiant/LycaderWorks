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

            TextureManager.Load("tiles", FileFinder.Find("Resources", "Sonic.png"));

            this.map = new Map();
            this.map.Load(FileFinder.Find("Resources", "Sonic.map"));
            this.map.FlipY();
            this.map.Texture = TextureManager.Get("tiles");
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
            this.map.MoveAll(1, 0, false);
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

            for (int i = 0; i < this.map.Layers.Count; i++)
            {
                this.map.Blit(i);
            }

            SwapBuffers();
        }
    }
}