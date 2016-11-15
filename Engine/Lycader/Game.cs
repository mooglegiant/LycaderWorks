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
        public IScene CurrentScene { get; internal set; }

        private IScene NextScene { get; set; }

        /// <summary>
        /// Initializes a new instance of the Game class
        /// </summary>
        internal Game()
            : base(LycaderEngine.ScreenWidth, LycaderEngine.ScreenHeight, GraphicsMode.Default, LycaderEngine.ScreenTitle)
        {
            this.CurrentScene = new BlankScene();
            this.VSync = VSyncMode.On;       
        }

        /// <summary>
        /// Initializes a new instance of the Game class
        /// </summary>
        internal Game(IScene scene)
            : base(LycaderEngine.ScreenWidth, LycaderEngine.ScreenHeight, GraphicsMode.Default, LycaderEngine.ScreenTitle)
        {
            this.CurrentScene = new BlankScene();
            this.QueueScene(scene);

            this.VSync = VSyncMode.On;
        }

        /// <summary>
        /// Overrides our OnLoad event
        /// </summary>
        /// <param name="e">Event Parms</param>
        protected override void OnLoad(EventArgs e)
        {
            GL.Enable(EnableCap.Texture2D);
            GL.Enable(EnableCap.Blend);
            GL.Enable(EnableCap.DepthTest);
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
            float aspectRatio = (float)this.Width / this.Height;
            float xSpan = 1;
            float ySpan = 1;

            if (aspectRatio > 1)
            {
                // Width > Height, so scale xSpan accordinly.
                xSpan *= aspectRatio;
            }
            else
            {
                // Height >= Width, so scale ySpan accordingly.
                ySpan = xSpan / aspectRatio;
            }   

            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();


           //GL.Ortho(0, xSpan, 0, ySpan, -1, 1);
            //GL.Viewport(0, 0, this.Width, this.Height);

            GL.Ortho(0, xSpan * this.Width, 0, this.Height * ySpan, -1, 1);
            GL.Viewport(0, this.Width, 0, this.Height);

            LycaderEngine.ScreenWidth = this.Width;
            LycaderEngine.ScreenHeight = this.Height; 
        }

        /// <summary>
        /// Overrides our OnUpdateFrame event
        /// </summary>
        /// <param name="e">Event Parms</param>
        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            CurrentScene.Update(e);
            ChangeScene();
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

            CurrentScene.Draw(e);

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

        #region "Scene Management"
        public void QueueScene(IScene next)
        {
            this.NextScene = next;
        }

        internal void ChangeScene()
        {
            if (this.NextScene != null)
            {
                this.CurrentScene.Unload();
                this.NextScene.Load();

                this.CurrentScene = this.NextScene;
                this.NextScene = null;
            }
        }
        #endregion  
    }
}
