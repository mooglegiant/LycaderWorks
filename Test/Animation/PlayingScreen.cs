//-----------------------------------------------------------------------
// <copyright file="PlayingScreen.cs" company="Mooglegiant" >
//      Copyright (c) Mooglegiant. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Animation
{
    using Lycader;
    using Lycader.Graphics;
    using Lycader.Utilities;
    using OpenTK;
    using OpenTK.Input;

    /// <summary>
    /// Playing screenlet
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

            TextureContent.Load("sonic", FileFinder.Find("Resources", "Images", "sonic.png"));
            TextureContent.Load("sonic-stare", FileFinder.Find("Resources", "Images", "sonic-stare.png"));
            TextureContent.Load("sonic-tap", FileFinder.Find("Resources", "Images", "sonic-tap.png"));
            TextureContent.Load("sonic-watch", FileFinder.Find("Resources", "Images", "sonic-watch.png"));

            player = new Player();
            camera = new Camera();
        }

        public void Unload()
        {
            TextureContent.Unload();
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
