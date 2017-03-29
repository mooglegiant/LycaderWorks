

namespace Falldown
{
    using System;

    using OpenTK;
    using OpenTK.Graphics;
    using OpenTK.Input;

    using Lycader;
    using Lycader.Entities;

    public class HUD : Entity, IEntity
    {
        FontEntity score;
        FontEntity levelUp;
        FontEntity pause;

        /// <summary>
        /// Initializes a new instance of the HUD class
        /// </summary>
        /// <param name="position">Current world position</param>
        public HUD()
            : base(new Vector3(0, 0, 0), 1, 0)
        {
            this.score = new FontEntity("pixel.png", 11, new Vector3(Engine.Screen.Width - 120, Engine.Screen.Height - 45, 100), 1);
            this.score.Color = Color4.White;
            this.score.BackgroundColor = Color4.Black;
            this.score.Padding = new System.Drawing.PointF(3, 10);

            this.levelUp = new FontEntity("pixel.png", 11, new Vector3(Engine.Screen.Width - 120, Engine.Screen.Height - 25, 100), 1);
            this.levelUp.Color = Color4.White;
            this.levelUp.BackgroundColor = Color4.Black;
            this.levelUp.Padding = new System.Drawing.PointF(3, 10);

            this.pause = new FontEntity("pixel.png", 20, new Vector3(32 + Engine.Screen.Width / 4, Engine.Screen.Height * .75f, 100), 1.25f, "Paused");
            this.pause.Color = Color4.White;
            this.pause.BackgroundColor = Color4.Black;
            this.pause.Padding = new System.Drawing.PointF(20, 30);
        }

        /// <summary>
        /// Updates the sprite
        /// </summary>
        public override void Update()
        {
            this.score.Text = Globals.Score.ToString();
            this.levelUp.Text = Globals.LevelDisplay;
        }

        public override bool IsOnScreen(Camera camera)
        {
            return true;
        }

        public override void Draw(Camera camera)
        {

            if (Globals.IsPaused)
            {
                this.pause.Draw(camera);
            }
 
            this.levelUp.Draw(camera);
            this.score.Draw(camera);
        }
    }
}