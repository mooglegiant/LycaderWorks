using System;
using System.Drawing;

using SdlDotNet.Input;
using SdlDotNet.Graphics.Sprites;
using SdlDotNet.Graphics;
using SDLGE;

using Game.Base;


namespace Game.Screens
{
    public class LevelSelect : GameScreen
    {
        TextSprite HighScore;
        TextSprite TotalScore;
        TextSprite PlayTimes;
        int index;
        int otherindex;

        Sprite Mode1;
        Sprite Mode2;
        int x;        

        public LevelSelect()
        {
            HighScore = new TextSprite(new SdlDotNet.Graphics.Font("Data\\Fonts\\lucon.ttf", 20));
            HighScore.Text = "High Score: " + Globals.Scores["HighScore1"].ToString();
            HighScore.Color = Color.White;
            HighScore.Position = new Point((640 - HighScore.Rectangle.Width) / 2, 380);

            TotalScore = new TextSprite(new SdlDotNet.Graphics.Font("Data\\Fonts\\lucon.ttf", 20));
            TotalScore.Text = "Total Score: " + Globals.Scores["TotalScore1"].ToString();
            TotalScore.Color = Color.White;
            TotalScore.Position = new Point((640 - TotalScore.Rectangle.Width)/2, 410);

            PlayTimes = new TextSprite(new SdlDotNet.Graphics.Font("Data\\Fonts\\lucon.ttf", 20));
            PlayTimes.Text = "Times Played: " + Globals.Scores["Mode1Count"].ToString();
            PlayTimes.Color = Color.White;
            PlayTimes.Position = new Point((640 - PlayTimes.Rectangle.Width) / 2, 440);

            index = 0;
            otherindex = 1;
            Engine.Surfaces.AddSurface("Data\\Images\\Title2.gif", "Title", false);
            Engine.Surfaces.AddSurface("Data\\Images\\Mode1.jpg", "Mode1", false);
            Engine.Surfaces.AddSurface("Data\\Images\\Mode2.jpg", "Mode2", false);

            Mode1 = new Sprite(Engine.Surfaces["Mode1"]);
            Mode2 = new Sprite(Engine.Surfaces["Mode2"]);

            Mode1.Position = new Point(200, 130);
            Mode2.Position = new Point(400, 130);
            x = 0;

            Engine.Sounds.AddSfx("Data\\Sounds\\select.ogg", "Select", 100);
            Globals.ClearAllLayers();
        }

        public override void Kill()
        {
            Engine.Surfaces.Remove("Title");
            Engine.Surfaces.Remove("Mode1");
            Engine.Surfaces.Remove("Mode2");
            Engine.Sounds.Remove("Menu");
            Engine.Sounds.Remove("Select");
        }

        public override void Draw()
        {
            
            if (Globals.Layer1.Redraw)
            {
                Globals.Layer1.Blit(Engine.Surfaces["Title"], new Point(0, 0));
            }

            Animate();

            Globals.Layer2.Clear();
            Globals.Layer2.Blit(Mode1.Surface, Mode1.Position);
            Globals.Layer2.Blit(Mode2.Surface, Mode2.Position);
            Globals.Layer2.Blit(HighScore.Surface, HighScore.Position);
            Globals.Layer2.Blit(TotalScore.Surface, TotalScore.Position);
            Globals.Layer2.Blit(PlayTimes.Surface, PlayTimes.Position);       
        }

        public override void KeysUp(KeyboardEventArgs e)
        {
        }

        public override void KeysDown(KeyboardEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                if (index == 0)
                {
                    Globals.GameMode = 1;
                }
                else
                {  
                    Globals.GameMode = 2;
                }
                Globals.ActiveScreen.Kill();
                Globals.ActiveScreen = new Level();
            }

            if (e.Key == Key.LeftArrow)
            {
                index--;
                SelectMode();
            }
            else if (e.Key == Key.RightArrow)
            {
                index++;
                SelectMode();
            }
        }

        public void SelectMode()
        {           

            if (index < 1) { index = 1; }
            if (index > 2) { index = 2; }

            if (index == 0)
            {
                HighScore.Text = "High Score: " + Globals.Scores["HighScore1"].ToString();
                TotalScore.Text = "Total Score: " + Globals.Scores["TotalScore1"].ToString();
                PlayTimes.Text = "Times Played: " + Globals.Scores["Mode1Count"].ToString();
                if (index == otherindex)
                {
                    otherindex = 1;
                    Engine.Sounds.Play("Select");
                }
            }
            if (index == 1)
            {
                HighScore.Text = "High Score: " + Globals.Scores["HighScore2"].ToString();
                TotalScore.Text = "Total Score: " + Globals.Scores["TotalScore2"].ToString();
                PlayTimes.Text = "Times Played: " + Globals.Scores["Mode2Count"].ToString();
                if (index == otherindex)
                {
                    otherindex = 0;
                    Engine.Sounds.Play("Select");
                }
            }
        }

        public void Animate()
        {
            if (index == 0)
            {
                if (x < 0) { x += 5; }
                else if (x > 0) { x -= 5; }
            }
            else if (index == 1)
            {
                if (x < 180) { x += 5; }
                else if (x > 180) { x -= 5; }
            }
            Mode1.Position = new Point(250 - x, 130);
            Mode2.Position = new Point(430 - x, 130);
        }
    }
}
