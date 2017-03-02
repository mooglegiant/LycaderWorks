using System;
using System.Drawing;

using SdlDotNet.Input;
using SdlDotNet.Graphics.Sprites;
using SdlDotNet.Graphics;
using SdlDotNet.Graphics.Primitives;
using SDLGE;

using Game.Base;
using Game.Objects;

namespace Game.Screens
{
    public class Level : GameScreen
    {
        HUD hud;

        public Level()
        {
            Engine.Maps.AddMap("Level", "Data\\Maps\\Map1.xml", "Data\\Maps\\level1.bmp");

            //Player1.LoadTurle();
            Engine.Sprites.AddSprite("Data\\Images\\Black.png", "Black", false);
            Engine.Window.ClearAllLayers();
            hud = new HUD();
            Draw();
        }

        public override void Kill()
        {
            Engine.Maps.Remove("Level");
        }

        public override void Draw()
        {
            int halfscreen = EngineSettings.ScreenX / 2;
            int point = GlobalObjects.Players[0].Location.X + GlobalObjects.Players[1].Location.X;
            point = point / 2;



            if (point > halfscreen)
            {
                Engine.Window.Layers[0].Camera.X = point - halfscreen;
                Engine.Window.Layers[1].Camera.X = (point - halfscreen) / 2;
                Engine.Window.Layers[2].Camera.X = point - halfscreen;
                Engine.Window.Layers[0].Clear();
                Engine.Window.Layers[1].Clear();
            }

              
            if (Engine.Window.Layers[0].Redraw)
            {
                Engine.Maps["Level"].SetCamera(Engine.Window.Layers[0].Camera); 
                Engine.Window.Layers[0].Screen.Blit(Engine.Maps["Level"].GetLayer(0, true).Surface);
            }

            if (Engine.Window.Layers[1].Redraw)
            {
                Engine.Maps["Level"].SetCamera(Engine.Window.Layers[1].Camera);
                Engine.Window.Layers[1].Screen.Blit(Engine.Maps["Level"].GetLayer(1, true).Surface);
            }

            //Draw Objects on Layer 2
            Engine.Window.Layers[2].Clear();

            if (Engine.Window.Layers[3].Redraw)
            {
                Engine.Window.Layers[3].Screen.Blit(Engine.Sprites["Black"].Surface, new Point(0, -199));
                hud.Draw();
            }           
        }


        public override void KeysUp(KeyboardEventArgs e)
        {
            if (e.Key == Key.LeftArrow)
            {
                GlobalObjects.Players[0].PressLeft(false);
            }
            if (e.Key == Key.RightArrow)
            {
                GlobalObjects.Players[0].PressRight(false);
            }
            if (e.Key == Key.DownArrow)
            {
                GlobalObjects.Players[0].PressDown(false);
            }
            if (e.Key == Key.UpArrow)
            {
                GlobalObjects.Players[0].PressUp(false);
            }
            if (e.Key == Key.A)
            {
                GlobalObjects.Players[0].PressJump(false);
            }
            if (e.Key == Key.S)
            {
                GlobalObjects.Players[0].PressAttack(false);
            }

        }

        public override void KeysDown(KeyboardEventArgs e)
        {
            if (e.Key == Key.LeftArrow)
            {
                GlobalObjects.Players[0].PressLeft(true);
            }
            if (e.Key == Key.RightArrow)
            {
                GlobalObjects.Players[0].PressRight(true);
            }
            if (e.Key == Key.DownArrow)
            {
                GlobalObjects.Players[0].PressDown(true);
            }
            if (e.Key == Key.UpArrow)
            {
                GlobalObjects.Players[0].PressUp(true);
            }
            if (e.Key == Key.A)
            {
                GlobalObjects.Players[0].PressJump(true);
            }
            if (e.Key == Key.S)
            {
                GlobalObjects.Players[0].PressAttack(true);
            }
        }
    }
}
