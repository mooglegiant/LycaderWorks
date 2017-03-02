using System;
using System.Drawing;

using SdlDotNet.Input;
using SdlDotNet.Graphics.Sprites;
using SdlDotNet.Graphics;
using SDLGE;

using Game.Base;

namespace Game.Screens
{
    public class CharacterSelect : GameScreen
    {
        int gamemode;
        int player1selection, player2selection;
        TextSprite display;
        int screenmode;

        public CharacterSelect(int GameMode)
        {
            gamemode = GameMode;
            player1selection = 1;
            player2selection = 0;
            Engine.Songs.AddSong("Data\\Sounds\\Songs\\selection.ogg", "Selection", true, 100);
            Engine.Songs.Play("Selection");
            Engine.Sounds.AddSfx("Data\\Sounds\\SFX\\beep.ogg", "Beep", 100);

            Engine.Sprites.AddSpriteFrame("Data\\Images\\turtles-color.png", "ColorLeo", new Point(0, 0), new Point(47, 128), false);
            Engine.Sprites.AddSpriteFrame("Data\\Images\\turtles-color.png", "ColorRaph", new Point(49, 0), new Point(47, 128), false);
            Engine.Sprites.AddSpriteFrame("Data\\Images\\turtles-color.png", "ColorMike", new Point(98, 0), new Point(47, 128), false);
            Engine.Sprites.AddSpriteFrame("Data\\Images\\turtles-color.png", "ColorDon", new Point(147, 0), new Point(47, 128), false);

            Engine.Sprites.AddSpriteFrame("Data\\Images\\turtles-gray.png", "GrayLeo", new Point(0, 0), new Point(47, 128), false);
            Engine.Sprites.AddSpriteFrame("Data\\Images\\turtles-gray.png", "GrayRaph", new Point(49, 0), new Point(47, 128), false);
            Engine.Sprites.AddSpriteFrame("Data\\Images\\turtles-gray.png", "GrayMike", new Point(98, 0), new Point(47, 128), false);
            Engine.Sprites.AddSpriteFrame("Data\\Images\\turtles-gray.png", "GrayDon", new Point(147, 0), new Point(47, 128), false);

            Engine.Sprites.AddSpriteFrame("Data\\Images\\player-select.png", "P1Leo", new Point(0, 0), new Point(23, 22), true);
            Engine.Sprites.AddSpriteFrame("Data\\Images\\player-select.png", "P1Raph", new Point(24, 0), new Point(23, 22), true);
            Engine.Sprites.AddSpriteFrame("Data\\Images\\player-select.png", "P1Mike", new Point(48, 0), new Point(23, 22), true);
            Engine.Sprites.AddSpriteFrame("Data\\Images\\player-select.png", "P1Don", new Point(72, 0), new Point(23, 22), true);

            display = new TextSprite(new SdlDotNet.Graphics.Font("Data\\Fonts\\Dos.ttf", 8));
            display.Color = Color.White;
            display.BackgroundColor = Color.Black;
            display.Text = "Select Your Turtle";
            display.Position = new Point((EngineSettings.ScreenX - display.Rectangle.Width) / 2, 30);
            screenmode = 0;

            Engine.Sounds.AddSfx("Data\\Sounds\\SFX\\playerselect.ogg", "Start", 100);
            Engine.Window.Layers[0].Clear();
            Engine.Window.Layers[1].Clear();
            Engine.Window.Layers[2].Clear();
            Draw();
        }

        public override void Kill()
        {
            Engine.Songs.Stop();
            Engine.Songs.Remove("Selection");
            Engine.Sounds.Remove("Beep");
            Engine.Sounds.Remove("start");
            Engine.Sprites.Clear();
        }

        public override void Draw()
        {
            if (Engine.Window.Layers[0].Redraw)
            {
                Engine.Window.Layers[0].Screen.Blit(display.Surface, display.Position);

                if (player1selection == 1)
                {
                    Engine.Window.Layers[0].Screen.Blit(Engine.Sprites["ColorLeo"], new Point(15, 70));
                    Engine.Window.Layers[0].Screen.Blit(Engine.Sprites["P1Leo"], new Point(20, 55));
                }
                else
                {
                    Engine.Window.Layers[0].Screen.Blit(Engine.Sprites["GrayLeo"], new Point(15, 70));
                }

                if (player1selection == 2)
                {
                    Engine.Window.Layers[0].Screen.Blit(Engine.Sprites["ColorRaph"], new Point(75, 70));
                    Engine.Window.Layers[0].Screen.Blit(Engine.Sprites["P1Raph"], new Point(80, 55));
                }
                else
                {
                    Engine.Window.Layers[0].Screen.Blit(Engine.Sprites["GrayRaph"], new Point(75, 70));
                }

                if (player1selection == 3)
                {
                    Engine.Window.Layers[0].Screen.Blit(Engine.Sprites["ColorMike"], new Point(135, 70));
                    Engine.Window.Layers[0].Screen.Blit(Engine.Sprites["P1Mike"], new Point(140, 55));
                }
                else
                {
                    Engine.Window.Layers[0].Screen.Blit(Engine.Sprites["GrayMike"], new Point(135, 70));
                }

                if (player1selection == 4)
                {
                    Engine.Window.Layers[0].Screen.Blit(Engine.Sprites["ColorDon"], new Point(195, 70));
                    Engine.Window.Layers[0].Screen.Blit(Engine.Sprites["P1Don"], new Point(200, 55));
                }
                else
                {
                    Engine.Window.Layers[0].Screen.Blit(Engine.Sprites["GrayDon"], new Point(195, 70));
                }
            }

            if (Engine.Songs.IsSongFinished && !Engine.Sounds.IsSoundsPlaying && screenmode == 1)
            {
                GlobalObjects.Players[0] = new Game.Objects.Player(player1selection);
                GlobalObjects.Players[1] = new Game.Objects.Player(player2selection);

                GlobalObjects.GameScreen.Kill();
                GlobalObjects.GameScreen = new LevelDisplay(1, "The Beginning");
            }
        }

        public override void KeysUp(KeyboardEventArgs e)
        {
        }

        public override void KeysDown(KeyboardEventArgs e)
        {
            if (screenmode == 0)
            {
                if (e.Key == Key.LeftArrow)
                {
                    player1selection--;
                    if (player1selection == 0)
                    {
                        player1selection = 1;
                    }
                    else
                    {
                        Engine.Sounds.Play("Beep");
                    }
                    Engine.Window.Layers[0].Clear();
                }
                if (e.Key == Key.RightArrow)
                {
                    player1selection++;
                    if (player1selection == 5)
                    {
                        player1selection = 4;
                    }
                    else
                    {
                        Engine.Sounds.Play("Beep");
                    }
                    Engine.Window.Layers[0].Clear();
                }

                if (e.Key == Key.Return)
                {
                    Engine.Songs.Stop();
                    Engine.Sounds.Play("Start");
                    screenmode = 1;
                }
            }
        }
    }
}
