//-----------------------------------------------------------------------
// <copyright file="SelectScreen.cs" company="Mooglegiant" >
//      Copyright (c) Mooglegiant. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Falldown.Scenes
{
    using System;

    using OpenTK;
    using OpenTK.Input;

    using Lycader;
    using Lycader.Entities;

    /// <summary>
    /// Title screen
    /// </summary>
    public class SelectScreen : IScene
    {
        /// <summary>
        /// The screens text class
        /// </summary>
        private FontEntity title, pressStart;

        private EntityManager manager = new EntityManager();
        private FontEntity HighScore;
        private FontEntity TotalScore;
        private FontEntity PlayTimes;

        private SpriteEntity Mode1, Mode2;
        int index = 1;
        int otherindex;
        int x;


        /// <summary>
        /// Initializes a new instance of the TitleScreen class
        /// </summary>
        public SelectScreen()
        {       
        }

        public void Load()
        {
            var background = new SpriteEntity()
            {
                Texture = "title2.gif"
            };

            this.manager.Add(background);

            this.HighScore = new FontEntity("pixel.png", 15, new Vector3(20, 100, 100), 1, "High Score: " + Globals.Scores["HighScore1"].ToString());
            this.manager.Add(this.HighScore);

            this.TotalScore = new FontEntity("pixel.png", 15, new Vector3(20, 75, 100), 1, "Total Score: " + Globals.Scores["TotalScore1"].ToString());
            this.manager.Add(this.TotalScore);

            this.PlayTimes = new FontEntity("pixel.png", 15, new Vector3(20, 50, 100), 1, "Times Played: " + Globals.Scores["Mode1Count"].ToString());
            this.manager.Add(this.PlayTimes);

            index = 1;
            otherindex = 2;

            Mode1 = new SpriteEntity(new Vector3(150, 130, 10), 1, 0, "mode1.png");
            this.manager.Add(Mode1);

            Mode2 = new SpriteEntity(new Vector3(340, 130, 10), 1, 0, "mode2.png");
            Mode2.Alpha = 175;
            this.manager.Add(Mode2);
            x = 0;
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
            if (InputManager.IsKeyPressed(Key.Enter))
            {
             //   Globals.NewGame();
             //   LycaderEngine.ChangeScene(new TitleScreen());               
            }

            if (InputManager.IsKeyPressed(Key.Escape))
            {
                LycaderEngine.Screen.Exit();
            }

            if (InputManager.IsKeyPressed(Key.Left))
            {
                index--;
                SelectMode();
            }

            if (InputManager.IsKeyPressed(Key.Right))
            {
                index++;
                SelectMode();
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

        public void SelectMode()
        {

            if (index < 1) { index = 1; }
            if (index > 2) { index = 2; }

            HighScore.DisplayText = "High Score: " + Globals.Scores[string.Format("HighScore{0}", index)].ToString();
            TotalScore.DisplayText = "Total Score: " + Globals.Scores[string.Format("TotalScore{0}", index)].ToString();
            PlayTimes.DisplayText = "Times Played: " + Globals.Scores[string.Format("Mode{0}Count", index)].ToString();

            if (index == 1)
            {
                if (index == otherindex)
                {
                    otherindex = 2;
                    SoundManager.Find("select.wav").Play();
                    Mode1.Alpha = 255;
                    Mode2.Alpha = 175;
                }
            }


            if (index == 2)
            {
                if (index == otherindex)
                {
                    otherindex = 1;
                    SoundManager.Find("select.wav").Play();             
                    Mode1.Alpha = 175;
                    Mode2.Alpha = 255;
                }
            }
        }
    }
}
