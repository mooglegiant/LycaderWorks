using System;
using System.Drawing;

using SdlDotNet.Graphics.Sprites;
using SdlDotNet.Graphics.Primitives;

using SDLGE;

namespace Game.Objects
{
    public static class Scoreboard
    {
        static TextSprite ScoreBoard;
        static TextSprite TextBoard;

        static public void Init()
        {
            ScoreBoard = new TextSprite("Score", new SdlDotNet.Graphics.Font("Data\\FONTS\\TRANSIST.TTF", 18), Color.LimeGreen, new Point(535, 42));
            ScoreBoard.BackgroundColor = Color.DarkGreen;
            TextBoard = new TextSprite(" Score ", new SdlDotNet.Graphics.Font("Data\\FONTS\\ARIAL.TTF", 18), Color.White, new Point(540, 10));
            TextBoard.BackgroundColor = Color.Black;
        }

        static public void Draw()
        {
            ScoreBoard.Text = " " + Globals.Score.ToString().PadLeft(7, '0') + " ";
            Globals.Layer2.Blit(ScoreBoard.Surface, ScoreBoard.Position);
            Globals.Layer2.Blit(TextBoard.Surface, TextBoard.Position);

            if (Globals.DisplayLevel > 0)
            {
                Globals.Layer2.Blit(Globals.LevelDisplay.Surface, Globals.LevelDisplay.Position);
                Globals.DisplayLevel--;
            }
        }
    }
}
