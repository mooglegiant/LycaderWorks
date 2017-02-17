using System;
using System.Drawing;

using SdlDotNet.Input;
using SdlDotNet.Graphics.Sprites;
using SdlDotNet.Graphics;
using SDLGE;

using Game.Base;


namespace Game.Screens
{
    public class Help : GameScreen
    {
        public Help()
        {
            Engine.Surfaces.AddSurface("Data\\Images\\Help.gif", "Help", false);
            Globals.Layer1.Clear();
        }

        public override void Kill()
        {
            Engine.Surfaces.Remove("Help");
        }

        public override void Draw()
        {
            if (Globals.Layer1.Redraw)
            {
                Globals.Layer1.Blit(Engine.Surfaces["Help"], new Point(0, 0));
            }
        }

        public override void KeysUp(KeyboardEventArgs e)
        {
        }

        public override void KeysDown(KeyboardEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                Globals.ActiveScreen.Kill();
                Globals.ActiveScreen = new Title();
            }
        }
    }
}
