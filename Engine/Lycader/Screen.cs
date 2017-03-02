//-----------------------------------------------------------------------
// <copyright file="Screen.cs" company="Mooglegiant" >
//      Copyright (c) Mooglegiant. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Lycader
{
    using System;
    using System.Drawing;
    using IMG = System.Drawing.Imaging;
    using System.IO;

    using OpenTK;
    using OpenTK.Audio;
    using OpenTK.Graphics;
    using OpenTK.Graphics.OpenGL;
    using OpenTK.Input;

    /// <summary>
    /// Our game class
    /// </summary>
    public class Screen : GameWindow
    {

        /// <summary>
        /// Initializes a new instance of the Screen class
        /// </summary>
        internal Screen(int width, int height, string windowTitle)
            : base(width, height, GraphicsMode.Default, windowTitle)
        {
            LycaderEngine.CurrentScene = new BlankScene();
            this.VSync = VSyncMode.On;
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
            Audio.Music.Dispose();
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

            if (InputManager.IsKeyPressed(Key.F11))
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

            if (InputManager.IsKeyPressed(Key.F12))
            {
                SaveScreenshot();
            }

            LycaderEngine.CurrentScene.Update(e);
            LycaderEngine.ToggleScene();
            InputManager.Update();
            Audio.SoundManager.ProcessQueue();

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

        private void SaveScreenshot()
        {
            int fileNumber = Directory.GetFiles(Environment.CurrentDirectory, "*.bmp").GetLength(0);

            Bitmap bmp = new Bitmap(this.ClientSize.Width, this.ClientSize.Height);
            System.Drawing.Imaging.BitmapData data = bmp.LockBits(this.ClientRectangle, IMG.ImageLockMode.WriteOnly, IMG.PixelFormat.Format24bppRgb);
            GL.ReadPixels(0, 0, this.ClientSize.Width, this.ClientSize.Height, PixelFormat.Bgr, PixelType.UnsignedByte, data.Scan0);
            bmp.UnlockBits(data);

            bmp.RotateFlip(RotateFlipType.RotateNoneFlipY);
            bmp.Save(string.Format("screenshot{0}.bmp", fileNumber), IMG.ImageFormat.Bmp);
            bmp.Dispose();

        }

    }
}
