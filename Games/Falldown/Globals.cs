namespace Falldown
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Lycader;
    using Lycader.Utilities;

    static public class Globals
    {
        static public int Score = 0;
        static public int CurrentLevel = 1;
        static public double BlockSpeed = 1.5;
        static public double BallFallSpeed = 2.5;
        static public double BallMoveSpeed = 2.5;
        static public int ExtraMoveSpeed = 0;
        static public int ExtraFallSpeed = 0;
        static public bool ExtraScore = false;
        static public int GameMode = 0;

        static public string LevelDisplay;
        static public int DisplayLevel = 100;
        static public Color LevelColor = Color.Aqua;
        static public SaveFile Scores = new SaveFile();
        static public bool IsPaused = false;

        static public void NewGame()
        {
            Score = 0;
            CurrentLevel = 1;
            BlockSpeed = 1.5;
            BallFallSpeed = 2.5;
            BallMoveSpeed = 2.5;
            ExtraMoveSpeed = 0;
            ExtraFallSpeed = 0;
            ExtraScore = false;
            GameMode = 0;

            LevelDisplay = string.Empty;
            DisplayLevel = 100;
            LevelColor = Color.Aqua;
        }

        static public void LevelUp()
        {
            CurrentLevel++;
            BlockSpeed += .21;
            BallFallSpeed += .08;
            BallMoveSpeed += .20;
            Score += 100;

            DisplayLevel = 100;
            LevelDisplay = "Level - " + CurrentLevel.ToString();

            SoundManager.Find("LevelUp.wav").Play();

            switch (CurrentLevel % 10)
            {
                case 0:
                    LevelColor = Color.Aqua;
                    break;
                case 1:
                    LevelColor = Color.Blue;
                    break;
                case 2:
                    LevelColor = Color.Green;
                    break;
                case 3:
                    LevelColor = Color.Orange;
                    break;
                case 4:
                    LevelColor = Color.Red;
                    break;
                case 5:
                    LevelColor = Color.Yellow;
                    break;
                case 6:
                    LevelColor = Color.Brown;
                    break;
                case 7:
                    LevelColor = Color.Wheat;
                    break;
                case 8:
                    LevelColor = Color.FloralWhite;
                    break;
                case 9:
                    LevelColor = Color.MidnightBlue;
                    break;
            }
        }

        static public void Init()
        {
            LevelColor = Color.Aqua;
        }

        static public void AddToScore(int Points)
        {
            Score += Points;
            if (ExtraScore == true)
            {
                Score += Points;
            }
        }
    }
}
