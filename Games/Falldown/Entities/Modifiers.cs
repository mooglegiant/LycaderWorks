using System;
using System.Collections;
using System.Drawing;

using SdlDotNet.Graphics.Sprites;
using SdlDotNet.Graphics.Primitives;

using SDLGE;

namespace Game.Objects
{
    // Bonus Types
    // 1 - Metal
    // 2 - Speed
    // 3 - Double Score

    static public class Modifiers
    {
        static ArrayList Bonuslist = new ArrayList();

        static public void Process(int Type)
        {
            ArrayList newlist = new ArrayList();
            bool found = false;

            int Timer = 0;
            if (Type == 1) 
            { 
                Timer = 300;
                Globals.ExtraFallSpeed = 2;
            }
            else if (Type == 2) 
            { 
                Timer = 200;
                Globals.ExtraMoveSpeed = 1;
            }
            else if (Type == 3) 
            { 
                Timer = 300;
                Globals.ExtraScore = true;
            }

            foreach (Bonus temp in Bonuslist)
            {
                if (temp.Type == Type)
                {
                    temp.Timer += Timer;
                    found = true;
                }
                newlist.Add(temp);
            }

            if (!found)
            {
                Bonus newbonus = new Bonus();
                newbonus.Timer += Timer;
                newbonus.Type = Type;
                newlist.Add(newbonus);
            }

            Bonuslist = newlist;
            Globals.AddToScore(5);
            Engine.Sounds.Play("PickUp");

            Engine.Surfaces.AddSurface("Data\\Images\\blip.bmp", "blip", false);


        }

        static public void Update()
        {
            ArrayList newlist = new ArrayList();
            foreach(Bonus temp in Bonuslist)
            {
                if (temp.Timer != 0)
                {
                    temp.Timer--;
                    newlist.Add(temp);
                }
                else
                {
                    if (temp.Type == 1)
                    {
                        Globals.ExtraFallSpeed = 0;
                    }
                    else if (temp.Type == 2)
                    {
                        Globals.ExtraMoveSpeed = 0;
                    }
                    else if (temp.Type == 3)
                    {
                        Globals.ExtraScore = false;
                    }
                }
            }
            Bonuslist = newlist;
        }

        static public void Draw()
        {
            Box PowerUp;
            TextSprite PowerUpDisplay;
            PowerUp = new Box(0, 0, 20, 20);
            Point SpritePoint = new Point(580, 72);           

            foreach (Bonus ThisBonus in Bonuslist)
            {
                int value = ThisBonus.Timer / 10;                

                PowerUpDisplay = new TextSprite(value.ToString(), new SdlDotNet.Graphics.Font("Data\\FONTS\\ARIAL.TTF", 18), Color.White, SpritePoint);

                Point Loc = new Point();
                Loc.X = (PowerUpDisplay.X - 45);
                Loc.Y = (PowerUpDisplay.Y - 5);
                Globals.Layer2.Blit(Engine.Surfaces["blip"], Loc);                

                Globals.Layer2.Blit(PowerUpDisplay.Surface, PowerUpDisplay.Position);
                PowerUp.XPosition1 = (short)(PowerUpDisplay.X - 40);
                PowerUp.XPosition2 = (short)(PowerUp.XPosition1 + 20);
                PowerUp.YPosition1 = (short)(PowerUpDisplay.Y);
                PowerUp.YPosition2 = (short)(PowerUp.YPosition1 + 20);
                Globals.Layer2.Blit(Engine.Surfaces["Powerup" + ThisBonus.Type.ToString().Trim()], PowerUp.Location);

                SpritePoint.Y += 40;
            }

        }

        private class Bonus
        {
            public int Timer;
            public int Type;
        }
    }
}
