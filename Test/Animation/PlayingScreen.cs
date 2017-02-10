//-----------------------------------------------------------------------
// <copyright file="PlayingScreen.cs" company="Mooglegiant" >
//      Copyright (c) Mooglegiant. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Animation
{
    using Lycader;
    using Lycader.Utilities;
    using OpenTK;
    using OpenTK.Input;

    /// <summary>
    /// Playing screen
    /// </summary>
    public class PlayingScreen : IScene
    {
        private Player player;
        private Camera camera;

        /// <summary>
        /// Initializes a new instance of the PlayingScreen class
        /// </summary>
        public PlayingScreen()
        {
        }

        public void Load()
        {

            TextureManager.Load("sonic", FileFinder.Find("Resources", "Images", "sonic.png"));
            TextureManager.Load("sonic-stare", FileFinder.Find("Resources", "Images", "sonic-stare.png"));
            TextureManager.Load("sonic-tap", FileFinder.Find("Resources", "Images", "sonic-tap.png"));
            TextureManager.Load("sonic-watch", FileFinder.Find("Resources", "Images", "sonic-watch.png"));

            this.player = new Player();
            this.camera = new Camera();
        }

        public void Unload()
        {
            TextureManager.Unload();
        }

        /// <summary>
        /// Implements OnUpdateFrame
        /// </summary>
        /// <param name="keyboard">current keyboard</param>
        /// <param name="e">event args</param>
        public void Update(FrameEventArgs e)
        {
            this.player.Update();
        }

        public void Draw(FrameEventArgs e)
        {
            player.Draw(camera);
        }
    }
}
