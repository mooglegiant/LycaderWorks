namespace Lycader
{
    using System;
    using System.Drawing;

    using OpenTK;
    using OpenTK.Graphics;
    using OpenTK.Graphics.OpenGL;
    using OpenTK.Input;
    using Lycader.Scenes;

    /// <summary>
    /// Our game class
    /// </summary>
    public class Game : GameWindow
    {

        private float avgfps = 60;

        public float xAdjust = 1;
        public float yAdjust = 1;

        /// <summary>
        /// Initializes a new instance of the Game class
        /// </summary>
        internal Game(IScene scene, int width, int height, string screenTitle)
            : base(width, height, GraphicsMode.Default, screenTitle)
        {
            LycaderEngine.CurrentScene = new BlankScene();
            LycaderEngine.ChangeScene(scene);

            this.VSync = VSyncMode.On;
        }

        /// <summary>
        /// Overrides our OnLoad event
        /// </summary>
        /// <param name="e">Event Parms</param>
        protected override void OnLoad(EventArgs e)
        {
            GL.Disable(EnableCap.CullFace);
            GL.Enable(EnableCap.Texture2D);
            GL.Enable(EnableCap.Blend);
            //GL.Enable(EnableCap.DepthTest);
            GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);
            GL.PixelStore(PixelStoreParameter.UnpackAlignment, 1);
            GL.ClearColor(Color.Black);
        }

        protected override void OnUnload(EventArgs e)
        {
            LycaderEngine.IsShuttingDown = true;
        }

        /// <summary>
        /// Overrides our OnResize event
        /// </summary>
        /// <param name="e">Event Parms</param>
        protected override void OnResize(EventArgs e)
        {
            LycaderEngine.WindowAdjustment = new SizeF((float)(this.Width / (float)LycaderEngine.Resolution.Width), (float)(this.Height / (float)LycaderEngine.Resolution.Height));

            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();

            GL.Viewport(0, 0, this.Width, this.Height);

            //Scale screen
            GL.Ortho(0, this.Width / LycaderEngine.WindowAdjustment.Width, 0, this.Height / LycaderEngine.WindowAdjustment.Height, 100, -100);
        }

        /// <summary>
        /// Overrides our OnUpdateFrame event
        /// </summary>
        /// <param name="e">Event Parms</param>
        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            LycaderEngine.CurrentScene.Update(e);
            LycaderEngine.ToggleScene();

            LycaderEngine.Fps = (avgfps + (1.0f / (float)e.Time)) / 2.0f;
           // Title = string.Format("{0} - FPS:{1:0.00}", LycaderEngine.ScreenTitle, avgfps);
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

            GL.ClearColor(LycaderEngine.BackgroundColor);

            LycaderEngine.CurrentScene.Draw(e);

            SwapBuffers();
        }

        protected override void OnKeyDown(KeyboardKeyEventArgs e)
        {
            Input.KeyboardHelper.AddKeyPress(e.Key);
        }

        protected override void OnKeyUp(KeyboardKeyEventArgs e)
        {
            Input.KeyboardHelper.RemoveKeyPress(e.Key);
        }

    }
}
