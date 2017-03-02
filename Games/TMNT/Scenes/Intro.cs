using System;
using System.Drawing;

using SdlDotNet.Input;
using SdlDotNet.Graphics.Sprites;
using SdlDotNet.Graphics;
using SDLGE;

using Game.Base;


namespace Game.Screens
{
    public class Intro : GameScreen
    {
        Point logo;
        TextSprite display;
        bool AllowInput;
        int mode;

        public Intro()
        {

            Engine.Sprites.AddSprite("Data\\Images\\city.png", "City", false);
            Engine.Sprites.AddSprite("Data\\Images\\title.png", "Title", true);

            logo = new Point(35, -200);
            AllowInput = false;
            mode = 0;

            display = new TextSprite(new SdlDotNet.Graphics.Font("Data\\Fonts\\Dos.ttf", 8));
            display.Color = Color.White;
            display.BackgroundColor = Color.Black;
            display.Text = " Press Start ";
            display.Position = new Point((EngineSettings.ScreenX - display.Rectangle.Width) / 2, 100);

            Engine.Songs.AddSong("Data\\Sounds\\Songs\\Title.ogg", "Title", false, 100);
            Engine.Songs.Play("Title");

            Engine.Sounds.AddSfx("Data\\Sounds\\SFX\\Pause.ogg", "Start", 100);
            Engine.Window.Layers[0].Clear();
            Engine.Window.Layers[1].Clear();
            Engine.Window.Layers[2].Clear();
            Draw();
        }

        public override void Kill()
        {
            Engine.Sprites.Remove("City");
            Engine.Sprites.Remove("Title");
            Engine.Songs.Stop();
            Engine.Songs.Remove("Title");
            Engine.Sounds.Remove("Start");
        }

        public override void Draw()
        {
            if (Engine.Window.Layers[0].Redraw)
            {
                Engine.Window.Layers[0].Screen.Blit(Engine.Sprites["City"]);
                Engine.Window.Layers[0].Screen.Blit(Engine.Sprites["Title"], logo);

            }

            if (mode >= 1)
            {
                if (Engine.Window.Layers[1].Redraw)
                {
                    Engine.Window.Layers[1].Screen.Blit(display.Surface, display.Position);
                }
            }

            Update();
        }

        public void Update()
        {
            if (mode == 0)
            {
                if (logo.Y < 15)
                {
                    logo.Y += 2;
                }
                else
                {
                    mode = 1;
                    AllowInput = true;
                }
            }
            else if (mode == 2)
            {
                if (Engine.Songs.IsSongFinished && !Engine.Sounds.IsSoundsPlaying)
                {
                    GlobalObjects.GameScreen.Kill();
                    GlobalObjects.GameScreen = new CharacterSelect(1);
                }
            }
        }


        public override void KeysUp(KeyboardEventArgs e)
        {
        }

        public override void KeysDown(KeyboardEventArgs e)
        {
            if (AllowInput)
            {
                if (e.Key == Key.Return)
                {
                    mode = 2;
                    Engine.Sounds.Play("Start");
                    Engine.Songs.Fade(1000);
                }
            }
        }
    }
}
