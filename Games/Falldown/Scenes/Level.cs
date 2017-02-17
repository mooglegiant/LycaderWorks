using System;
using System.Drawing;

using SdlDotNet.Input;
using SdlDotNet.Graphics.Sprites;
using SdlDotNet.Graphics.Primitives;

using SDLGE;

using Game.Base;
using Game.Objects;


namespace Game.Screens
{
    public class Level : GameScreen
    {

        private int GameCount = 0;
        private bool IsPaused;
        private TextSprite PauseDisplay;

        public Level()
        {
            Blocks.Init();
            Ball.Init();
            Scoreboard.Init();
            if (Globals.GameMode == 1)
            {
                GameCount = Globals.Scores.GetInt("Mode1Count");
            }
            else
            {
                GameCount = Globals.Scores.GetInt("Mode2Count");

            }

            Engine.Surfaces.AddSurface("Data\\Images\\Background.gif", "Background", false);


            Engine.Surfaces.AddSurface("Data\\Images\\Metal.png", "Powerup1", true);
            Engine.Surfaces.AddSurface("Data\\Images\\Shoe.png", "Powerup2", false);
            Engine.Surfaces.AddSurface("Data\\Images\\Double.png", "Powerup3", false);

            Engine.Songs.AddSong("Data\\Sounds\\Song1.ogg", "Song1", false, 100);
            Engine.Songs.AddSong("Data\\Sounds\\Song2.ogg", "Song2", false, 100);
            Engine.Songs.AddSong("Data\\Sounds\\Song2.ogg", "LoopSong2", false, 100);
            Engine.Songs.AddSong("Data\\Sounds\\Song3.ogg", "Song3", false, 100);

            Engine.Sounds.AddSfx("Data\\Sounds\\LevelUp.wav", "LevelUp", 100);
            Engine.Sounds.AddSfx("Data\\Sounds\\PickUp.wav", "PickUp", 100);

            Engine.Songs.Play("Song1");
            Strings Playlist = new Strings();
            Playlist.Add("Song2");
            Playlist.Add("LoopSong2");
            Playlist.Add("Song3");
            Engine.Songs.PlayList = Playlist;
            Engine.Songs.UsePlayList = true;
            Globals.Score = 0;

            //start as paused, since the keypress event will fire and will unpause it
            IsPaused = true;
            PauseDisplay = new TextSprite("  Paused  ", new SdlDotNet.Graphics.Font("Data\\FONTS\\ARIAL.TTF", 18), Color.White, new Point(230, 240));
            PauseDisplay.BackgroundColor = Color.CadetBlue;
            Globals.ClearAllLayers();

        }

        public override void Kill()
        {
            try
            {
                GameCount++;
            }
            catch { GameCount = int.MaxValue; }

            if (Globals.GameMode == 1)
            {
                Globals.Scores.SetInt("Mode1Count", GameCount);
                Globals.Scores.AddInt("TotalScore1", Globals.Score);

                if (Globals.Scores.GetInt("HighScore1") < Globals.Score)
                {
                    Globals.Scores.SetInt("HighScore1", Globals.Score);
                }

            }
            else
            {
                Globals.Scores.SetInt("Mode2Count", GameCount);
                Globals.Scores.AddInt("TotalScore2", Globals.Score);

                if (Globals.Scores.GetInt("HighScore2") < Globals.Score)
                {
                    Globals.Scores.SetInt("HighScore2", Globals.Score);
                }
            }

            Globals.Scores.SaveXmlFile("Save.xml");


            Engine.Surfaces.Remove("Background");
            Engine.Surfaces.Remove("Powerup1");
            Engine.Surfaces.Remove("Powerup2");
            Engine.Surfaces.Remove("Powerup3");

            Engine.Songs.Remove("Song1");
            Engine.Songs.Remove("Song2");
            Engine.Songs.Remove("LoopSong2");
            Engine.Songs.Remove("Song3");

            Engine.Sounds.Remove("LevelUp");
            Engine.Sounds.Remove("PickUp");
        }

        public override void Draw()
        {
            if (Globals.Layer1.Redraw)
            {
                Globals.Layer1.Blit(Engine.Surfaces["Background"], new Point(0, 0));
            }

            Globals.Layer2.Clear();

            Modifiers.Draw();
            Blocks.Draw();
            Ball.Draw();
            Scoreboard.Draw();

            if (!IsPaused)
            {
                Modifiers.Update();
                Blocks.Update();
                Ball.Update();
            }
            else
            {
                Globals.Layer2.Blit(PauseDisplay.Surface, PauseDisplay.Position);
            }                   
        }

        public override void KeysUp(KeyboardEventArgs e)
        {
            if (e.Key == Key.LeftArrow)
            {
                Ball.LeftPressed = false;
            }
            if (e.Key == Key.RightArrow)
            {
                Ball.RightPressed = false;
            }
            if (e.Key == Key.Return)
            {
                IsPaused = !IsPaused;
                if (IsPaused)
                {
                    Engine.Songs.Mute();
                }
                else
                {
                    Engine.Songs.UnMute();
                }
            }
        }

        public override void KeysDown(KeyboardEventArgs e)
        {
            if (e.Key == Key.LeftArrow)
            {
                Ball.LeftPressed = true;
            }
            if (e.Key == Key.RightArrow)
            {
                Ball.RightPressed = true;
            }
        }       
    }
}
