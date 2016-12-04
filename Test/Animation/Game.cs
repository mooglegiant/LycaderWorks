//-----------------------------------------------------------------------
// <copyright file="Game.cs" company="Mooglegiant" >
//      Copyright (c) Mooglegiant. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Animation
{
    using System;
    using System.Drawing;

    using MogAssist;
    using MogAssist.Audio;
    using MogAssist.Graphics;
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
        /// Initializes a new instance of the Game class
        /// </summary>
        public Game()
            : base(Config.ScreenWidth, Config.ScreenHeight, GraphicsMode.Default, "Animation Demo")
        {
            VSync = VSyncMode.On;
            this.Keyboard.KeyDown += this.OnKeyDown;
            this.Keyboard.KeyUp += this.OnKeyUp;
        }

        /// <summary>
        /// Overrides our OnLoad event
        /// </summary>
        /// <param name="e">Event Parms</param>
        public override void OnLoad(EventArgs e)
        {
            GL.ClearColor(Color.Black);

            TextureManager.Load("sonic", FileFinder.Find("Resources", "Images", "sonic.png"));
            TextureManager.Load("sonic-stare", FileFinder.Find("Resources", "Images", "sonic-stare.png"));
            TextureManager.Load("sonic-tap", FileFinder.Find("Resources", "Images", "sonic-tap.png"));
            TextureManager.Load("sonic-watch", FileFinder.Find("Resources", "Images", "sonic-watch.png"));

            Globals.NewGame();
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
            GL.Viewport(0, 0, this.Width, this.Height);

            Config.ScreenWidth = this.Width;
            Config.ScreenHeight = this.Height;
        }

        /// <summary>
        /// Overrides our OnUpdateFrame event
        /// </summary>
        /// <param name="e">Event Parms</param>
        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            if (Keyboard[Key.Escape])
            {
                Exit();
            }

            Globals.CurrentScreen.OnUpdateFrame(Keyboard, e);
        }

        /// <summary>
        /// Implements the keydown event
        /// </summary>
        /// <param name="sender">the keyboard device</param>
        /// <param name="e">the current key pressed</param>
        protected void OnKeyDown(object sender, KeyboardKeyEventArgs e)
        {
            Globals.CurrentScreen.OnKeyDown(e.Key);
        }

        /// <summary>
        /// Implements the keyup event
        /// </summary>
        /// <param name="sender">the keyboard device</param>
        /// <param name="e">the current key pressed</param>
        protected void OnKeyUp(object sender, KeyboardKeyEventArgs e)
        {
            Globals.CurrentScreen.OnKeyUp(e.Key);
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

            Globals.CurrentScreen.OnRenderFrame(e);

            SwapBuffers();
        }
    }
}

