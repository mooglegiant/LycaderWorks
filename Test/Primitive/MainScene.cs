//-----------------------------------------------------------------------
// <copyright file="MainScene.cs" company="Mooglegiant" >
//      Copyright (c) Mooglegiant. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace PrimitiveTest
{
    using OpenTK;
    using OpenTK.Graphics;
    using OpenTK.Input;

    using Lycader;
    using Lycader.Entities;

    public class MainScene : IScene
    {
        private CircleEntity circle1 = new CircleEntity(new Vector3(20, 20, 0f), 20, Color4.Red, DrawType.Solid, 1f);
        private CircleEntity circle2 = new CircleEntity(new Vector3(30, 20, 1f), 20, Color4.Blue, DrawType.Solid, 1f);
        private LineEntity line = new LineEntity(new Vector3(250, 0, 0), new Vector3(250, 250, 0), Color4.Green, 4);
        private LineEntity line2 = new LineEntity(new Vector3(300, 0, 0), new Vector3(300, 250, 0), Color4.Green, 1);

        private Camera camera1; 

        public MainScene()
        {
        }

        public void Load()
        {
            camera1 = new Camera();
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
            if (InputManager.IsKeyPressed(Key.Escape))
            {
                LycaderEngine.Screen.Exit();
            }

        }

        /// <summary>
        /// Implements OnRender
        /// </summary>
        /// <param name="e">event args</param>
        public void Draw(FrameEventArgs e)
        {
            circle1.Draw(camera1);
            circle2.Draw(camera1);
            line.Draw(camera1);
            line2.Draw(camera1);
        }
    }
}
