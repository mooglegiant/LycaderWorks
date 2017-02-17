using System;
using System.Drawing;

using SdlDotNet.Input;
using SdlDotNet.Graphics.Sprites;
using SdlDotNet.Graphics;
using SDLGE;

using Game.Base;


namespace Game.Screens
{
    public class GameOver : GameScreen
    {
        TextSprite Gameover;
        TextSprite TotalScore;

        public GameOver()
        {
            Gameover = new TextSprite(new SdlDotNet.Graphics.Font("Data\\Fonts\\lucon.ttf", 20));
            Gameover.Text = "Game Over";
            Gameover.Color = Color.White;
            Gameover.Position = new Point((640 - Gameover.Rectangle.Width)/2, 180);            

            TotalScore = new TextSprite(new SdlDotNet.Graphics.Font("Data\\Fonts\\lucon.ttf", 20));
            TotalScore.Text = "Final Score: " + Globals.Score.ToString();
            TotalScore.Color = Color.White;
            TotalScore.Position = new Point((640 - TotalScore.Rectangle.Width) / 2, 210);

            Engine.Songs.AddSong("Data\\Sounds\\EndSong.ogg", "End", false, 100);
            Engine.Songs.Play("End");
            Globals.ClearAllLayers();
        }

        public override void Kill()
        {
            Engine.Songs.Remove("End");
        }

        public override void Draw()
        {
            Globals.Layer1.Blit(Gameover.Surface, Gameover.Position);
            Globals.Layer1.Blit(TotalScore.Surface, TotalScore.Position);
        }

        public override void KeysUp(KeyboardEventArgs e)
        {
        }

        public override void KeysDown(KeyboardEventArgs e)
        {
            if(e.Key == Key.Return)
            {
                Globals.ActiveScreen.Kill();
                Globals.ActiveScreen = new Title();
            }
        }
    }
}
