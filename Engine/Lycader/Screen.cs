//-----------------------------------------------------------------------
// <copyright file="Screen.cs" company="Mooglegiant" >
//      Copyright (c) Mooglegiant. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Lycader
{
    using System;
    using System.Drawing;

    using OpenTK;
    using OpenTK.Audio;
    using OpenTK.Graphics;
    using OpenTK.Graphics.OpenGL;

    /// <summary>
    /// Our game class
    /// </summary>
    public class Screen : GameWindow
    {
        /// <summary>
        /// The screen's AudioContext
        /// </summary>
        private static AudioContext audioContext;

        /// <summary>
        /// Initializes a new instance of the Screen class
        /// </summary>
        internal Screen(int width, int height, string windowTitle)
            : base(width, height, GraphicsMode.Default, windowTitle)
        {
            LycaderEngine.CurrentScene = new BlankScene();
            this.VSync = VSyncMode.On;

            SoundManager.AllowSoundPlayed = false;
            SoundManager.HasSoundDevice = false;
            SoundManager.Enabled = false;

            // Make sure we have a sound device available.  If not, do not allow playing of sounds :)
            if (AudioContext.AvailableDevices.Count > 0)
            {
                if (!string.IsNullOrEmpty(AudioContext.AvailableDevices[0]))
                {
                    audioContext = new AudioContext();
                    SoundManager.AllowSoundPlayed = true;
                    SoundManager.HasSoundDevice = true;
                    SoundManager.Enabled = true;
                    new OggStreamer();
                }
            }
        }

        public void ToggleFullScreen()
        {
            if (this.WindowState == WindowState.Fullscreen)
            {
                this.WindowState = WindowState.Normal;
            }
            else
            {
                this.WindowState = WindowState.Fullscreen;
            }
        }

        /// <summary>
        /// Overrides our OnLoad event
        /// </summary>
        /// <param name="e">Event Parameters</param>
        protected override void OnLoad(EventArgs e)
        {
            GL.ClearColor(Color.Black);

            GL.Enable(EnableCap.DepthTest);
            GL.Enable(EnableCap.Texture2D);
            GL.Enable(EnableCap.AlphaTest);
            GL.AlphaFunc(AlphaFunction.Greater, 0f);
            GL.Enable(EnableCap.Blend);

            GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);
        }

        protected override void OnUnload(EventArgs e)
        {
            LycaderEngine.IsShuttingDown = true;
            if (OggStreamer.Instance != null)
            {
                OggStreamer.Instance.Dispose();
            }
        }

        /// <summary>
        /// Overrides our OnResize event
        /// </summary>
        /// <param name="e">Event Parameters</param>
        protected override void OnResize(EventArgs e)
        {
            LycaderEngine.WindowAdjustment = new SizeF((float)(this.Width / (float)LycaderEngine.Resolution.Width), (float)(this.Height / (float)LycaderEngine.Resolution.Height));

            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();

            GL.Viewport(0, 0, this.Width, this.Height);

            //Scale screen
            GL.Ortho(0, this.Width / LycaderEngine.WindowAdjustment.Width, 0, this.Height / LycaderEngine.WindowAdjustment.Height, 100, -100);
        }

        protected override void OnWindowInfoChanged(EventArgs e)
        {
            base.OnWindowInfoChanged(e);
        }

        /// <summary>
        /// Overrides our OnUpdateFrame event
        /// </summary>
        /// <param name="e">Event Parameters</param>
        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            // Do not process if minimized
            if (LycaderEngine.Screen.WindowState == WindowState.Minimized)
            {
                return;
            }

            // Do not process if out of focus
            if (LycaderEngine.Screen.Focused == false)
            {
                return;
            }

            LycaderEngine.CurrentScene.Update(e);
            LycaderEngine.ToggleScene();
            InputManager.Update();
            SoundManager.ProcessQueue();

            // LycaderEngine.Fps = (avgfps + (1.0f / (float)e.Time)) / 2.0f;
            // Title = string.Format("{0} - FPS:{1:0.00}", LycaderEngine.ScreenTitle, avgfps);
        }

        /// <summary>
        /// Overrides our OnRenderFrame event
        /// </summary>
        /// <param name="e">Event Parameters</param>
        protected override void OnRenderFrame(FrameEventArgs e)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();
            GL.ClearColor(LycaderEngine.BackgroundColor);

            LycaderEngine.CurrentScene.Draw(e);

            this.SwapBuffers();  
        }
    }
}
