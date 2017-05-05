namespace Falldown
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Lycader;
    using Lycader.Audio;
    using Lycader.Utilities;

    static public class Globals
    {
        static public int Score = 0;
        static public int CurrentLevel = 1;
        static public float BlockSpeed = 1f;
        static public float BallFallSpeed = 2f;
        static public float BallMoveSpeed = 3f;

        static public float ExtraMoveSpeed = 1.2f;
        static public float ExtraFallSpeed = 3f;

        static public bool ExtraScore = false;
        static public int GameMode = 2;

        static public string LevelDisplay = "Level:1";
        static public SaveFile Scores = new SaveFile();
        static public bool IsPaused = false;

        static public void NewGame()
        {
            Score = 0;
            CurrentLevel = 1;
            BlockSpeed = 1f;
            BallFallSpeed = 2f;
            BallMoveSpeed = 3f;
            ExtraMoveSpeed = 0;
            ExtraFallSpeed = 0;
            ExtraScore = false;
            GameMode = 0;

            LevelDisplay = "Level:1";
        }

        static public void LevelUp()
        {
            CurrentLevel++;
            BlockSpeed += .8f;
            BallFallSpeed += .3f;
            BallMoveSpeed += .4f;
  
            LevelDisplay = "Level:" + CurrentLevel.ToString();

            SoundManager.Find("LevelUp.wav").Play();          
        }

        static public void AddToScore(int Points)
        {
            Score += Points;

            if(Score % 10 == 0)
            {
                LevelUp();
            }
        }
    }
}
