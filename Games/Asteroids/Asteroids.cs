//-----------------------------------------------------------------------
// <copyright file="Asteroids.cs" company="Mooglegiant" >
//      Copyright (c) Mooglegiant. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Asteroids
{
    using System;
    using System.Drawing;

    using Lycader;
    using Lycader.Audio;
    using Lycader.Graphics;
    using Lycader.Utilities;

    using OpenTK;
    using OpenTK.Graphics;
    using OpenTK.Graphics.OpenGL;
    using OpenTK.Input;

    /// <summary>
    /// Our game class
    /// </summary>
    public class Asteroids : GameWindow
    {
        /// <summary>
        /// Initializes a new instance of the Asteroids class
        /// </summary>
        public Asteroids()
            : base(LycaderEngine.ScreenWidth, LycaderEngine.ScreenHeight, GraphicsMode.Default, "Asteroids")
        {
            VSync = VSyncMode.On;
            System.Windows.Forms.Cursor.Hide();
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

            TextureContent.Load("background", FileFinder.Find("Resources", "Images", "background.png"));
            TextureManager.Load("ship", FileFinder.Find("Resources", "Images", "ship.png"));
            TextureManager.Load("bullet", FileFinder.Find("Resources", "Images", "bullet.png"));

            TextureManager.Load("asteroid1", FileFinder.Find("Resources", "Images", "asteroid-small.png"));
            TextureManager.Load("asteroid2", FileFinder.Find("Resources", "Images", "asteroid-med.png"));
            TextureManager.Load("asteroid3", FileFinder.Find("Resources", "Images", "asteroid-large.png"));
            TextureManager.Load("asteroid4", FileFinder.Find("Resources", "Images", "asteroid-mega.png"));

            TextureManager.Load("font", FileFinder.Find("Resources", "Images", "font.png"));
            TextureManager.Load("font1", FileFinder.Find("Resources", "Images", "font.png"));

            SoundManager.Load("sound", FileFinder.Find("Resources", "Sounds", "boop.wav"));

            Globals.NewGame();
        }

        /// <summary>
        /// Overrides our OnUnload event
        /// </summary>
        /// <param name="e">Event Parms</param>
        public override void OnUnload(EventArgs e)
        {
            TextureManager.Unload();
            SoundManager.Unload();
            SoundPlayer.Unload();
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

            Background.Render();
            Globals.CurrentScreen.OnRenderFrame(e);

            SwapBuffers();
        }
    }
}

