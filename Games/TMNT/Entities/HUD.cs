using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

using SdlDotNet.Graphics.Sprites;
using SdlDotNet.Graphics;

using SDLGE;

namespace Game.Objects
{
    class HUD
    {
        TextSprite ScoreDisplay;
        int Life = 16;
        Point[] Points;

        public HUD()
        {
            int player1;
            int player2;

            ScoreDisplay = new TextSprite(new SdlDotNet.Graphics.Font("Data\\Fonts\\Dos.ttf", 8));
            ScoreDisplay.Color = Color.White;
            ScoreDisplay.BackgroundColor = Color.Black;
            ScoreDisplay.Position = new Point(112, 12);

            Engine.Sprites.AddSprite("Data\\Images\\Turtles\\life.png", "life", false);

            player1 = GlobalObjects.Players[0].Turtle;
            player2 = GlobalObjects.Players[1].Turtle;

            Points = new Point[4];
            Points[0] = new Point(12, 5);
            Points[1] = new Point(55, 24);
            Points[2] = new Point(132, 5);
            Points[3] = new Point(175, 24);

            if (player1 == 1)
            {   
                Engine.Sprites.AddSprite("Data\\Images\\Turtles\\frame-leo.png", "Frame1", false);
            }
            if (player1 == 2)
            {
                Engine.Sprites.AddSprite("Data\\Images\\Turtles\\frame-raph.png", "Frame1", false);
            }
            if (player1 == 3)
            {
                Engine.Sprites.AddSprite("Data\\Images\\Turtles\\frame-mike.png", "Frame1", false);
            }
            if (player1 == 4)
            {
                Engine.Sprites.AddSprite("Data\\Images\\Turtles\\frame-don.png", "Frame1", false);
            }

            if (player2 == 1)
            {
                Engine.Sprites.AddSprite("Data\\Images\\Turtles\\frame-leo.png", "Frame2", false);
            }
            if (player2 == 2)
            {
                Engine.Sprites.AddSprite("Data\\Images\\Turtles\\frame-raph.png", "Frame2", false);
            }
            if (player2 == 3)
            {
                Engine.Sprites.AddSprite("Data\\Images\\Turtles\\frame-mike.png", "Frame2", false);
            }
            if (player2 == 4)
            {
                Engine.Sprites.AddSprite("Data\\Images\\Turtles\\frame-don.png", "Frame2", false);
            }
            if(player2 == 0)
            {
                Engine.Sprites.AddSprite("Data\\Images\\Turtles\\tmnt-logo.png", "Frame2", false);
                Points[2] = new Point(145, 5);
            }

            ScoreDisplay.Text = "0";


        }

        public void Draw()
        {
            Surface temp = Engine.Sprites["life"].Surface;

            #region Draw Player1

            Engine.Window.Layers[3].Screen.Blit(Engine.Sprites["Frame1"].Surface, Points[0]);
            if (GlobalObjects.Players[0].Turtle != 0)
            {
                
                ScoreDisplay.Text = GlobalObjects.Players[0].Score.ToString();
                ScoreDisplay.Position = new Point(120 - ScoreDisplay.Width, 12);
                Engine.Window.Layers[3].Screen.Blit(ScoreDisplay.Surface, ScoreDisplay.Position);

                for (int i = 0; i < Life; i++)
                {
                    Points[1].X = 55 + (i * 4);
                    Engine.Window.Layers[3].Screen.Blit(temp, Points[1]);
                }
            }
            #endregion

            #region Draw Player2
            Engine.Window.Layers[3].Screen.Blit(Engine.Sprites["Frame2"].Surface, Points[2]);
            if (GlobalObjects.Players[1].Turtle != 0)
            {
                ScoreDisplay.Text = GlobalObjects.Players[1].Score.ToString();
                ScoreDisplay.Position = new Point(240 - ScoreDisplay.Width, 12);
                Engine.Window.Layers[3].Screen.Blit(ScoreDisplay.Surface, ScoreDisplay.Position);
                for (int i = 0; i < Life; i++)
                {
                    Points[3].X = 175 + (i * 4);
                    Engine.Window.Layers[3].Screen.Blit(temp, Points[3]);
                }
            }
            #endregion
        }
    }
}
