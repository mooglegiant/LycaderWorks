//-----------------------------------------------------------------------
// <copyright file="LevelScreen.cs" company="Mooglegiant" >
//      Copyright (c) Mooglegiant. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Asteroids.Scenes
{
    using OpenTK;
    using OpenTK.Input;

    using Lycader;
    using Lycader.Entities;
    using Lycader.Scenes;
  
    /// <summary>
    /// Display Level screen
    /// </summary>
    public class LevelScreen : IScene
    {
        /// <summary>
        /// The screens text class
        /// </summary>
        private FontEntity levelDisplay;

        private SceneManager manager = new SceneManager();

        private int timer = 100;

        public void Load()
        {
            this.levelDisplay = new FontEntity("font", 40, new Vector3(270, 300, 100), .75, string.Format("Level: {0}", Globals.Level));
            this.manager.Add(this.levelDisplay);
        }

        public void Unload()
        {
        }

        /// <summary>
        /// Implements OnUpdateFrame
        /// </summary>
        /// <param name="keyboard">current keyboard</param>
        /// <param name="e">event args</param>
        public void Update(FrameEventArgs e)
        {
            this.timer--;
            if (this.timer == 0)
            {
                LycaderEngine.ChangeScene(new PlayingScreen());
            }

            if (InputManager.IsKeyPressed(Key.Escape))
            {
                LycaderEngine.Screen.Exit();
            }

            if (InputManager.IsKeyPressed(Key.F11))
            {
                LycaderEngine.Screen.ToggleFullScreen();
            }

            this.manager.Update();                    
        }

        /// <summary>
        /// Implements OnRender
        /// </summary>
        /// <param name="e">event args</param>
        public void Draw(FrameEventArgs e)
        {
            this.manager.Render();           
        }
    }
}
