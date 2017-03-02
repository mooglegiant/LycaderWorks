using System;
using System.Drawing;

using SdlDotNet.Input;
using SdlDotNet.Graphics.Sprites;
using SdlDotNet.Graphics;
using SDLGE;

using Game.Base;


namespace Game.Screens
{
    public class LevelDisplay : GameScreen
    {
        TextSprite Display;
        int Level_Number;
        string Level_Name1;
        string Level_Name2;
        int DisplayCount;

        public LevelDisplay(int Level, string LevelName)
        {
            Display = new TextSprite(new SdlDotNet.Graphics.Font("Data\\Fonts\\Dos.ttf", 8));
            Display.Color = Color.White;
            Display.BackgroundColor = Color.Black;
            Display.Text = "Level " + Level.ToString() + " - " + LevelName;
            Display.Position = new Point(0, 0);
            DisplayCount = 1000;
            Level_Number = Level;
            Level_Name1 = "Level " + Level.ToString();
            Level_Name2 = LevelName;
            DisplayCount = 0;
            Engine.Window.Layers[0].Clear();
            Engine.Window.Layers[1].Clear();
            Engine.Window.Layers[2].Clear();
            Draw();
        }

        public override void Kill()
        {
        }

        public override void Draw()
        {
            if (Engine.Window.Layers[1].Redraw)
            {
                Display.Text = Level_Name1;
                Display.Position = new Point((EngineSettings.ScreenX - Display.Rectangle.Width) / 2, 80);
                Engine.Window.Layers[1].Screen.Blit(Display.Surface, Display.Position);

                Display.Text = Level_Name2;
                Display.Position = new Point((EngineSettings.ScreenX - Display.Rectangle.Width) / 2, 100);
                Engine.Window.Layers[1].Screen.Blit(Display.Surface, Display.Position);
            }

            DisplayCount++;

            if (DisplayCount == 200)
            {
                GlobalObjects.GameScreen.Kill();
                if (Level_Number == 1)
                {
                    GlobalObjects.GameScreen = new Level();
                }
            }
        }

        public override void KeysUp(KeyboardEventArgs e)
        {
        }

        public override void KeysDown(KeyboardEventArgs e)
        {
        }
    }
}
