using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

using SdlDotNet.Graphics;
using SdlDotNet.Graphics.Sprites;
using SdlDotNet.Graphics.Primitives;

using SDLGE;

using Game.Screens;

namespace Game.Objects
{
    /// <summary>
    /// Player class
    /// </summary>
    static class Ball
    {
        static public bool LeftPressed = false;
        static public bool RightPressed = false;

        static Circle PlayerBall;
        static int frame;
        static int frametick;

        static public Point Location
        {
            get { return new Point(PlayerBall.PositionX - PlayerBall.Radius, PlayerBall.PositionY - PlayerBall.Radius); }
        }

        static public void Init()
        {
            frame = 1;
            frametick = 0;
            PlayerBall = new Circle(42, 32, 10);
            Engine.Surfaces.AddSurface("Data\\Images\\ball1.bmp", "Ball1", true);
            Engine.Surfaces.AddSurface("Data\\Images\\ball2.bmp", "Ball2", true);
            Engine.Surfaces.AddSurface("Data\\Images\\ball3.bmp", "Ball3", true);
            Engine.Surfaces.AddSurface("Data\\Images\\ball4.bmp", "Ball4", true);
        }

        static public void Draw()
        {            
            Globals.Layer2.Blit(Engine.Surfaces["Ball" + frame.ToString()], Location);
        }

        static public void Update()
        {

            PlayerBall.PositionY += (short)(Globals.BallFallSpeed + Globals.ExtraFallSpeed);
            PlayerBall.PositionY = (short)Blocks.CollisionCheck(PlayerBall);

            if (PlayerBall.PositionY > 470)
            {
                PlayerBall.PositionY = 470;
            }
            if (PlayerBall.PositionY < -25)
            {
                Globals.ActiveScreen.Kill();
                Globals.ActiveScreen = new GameOver();
            }
            
            if (LeftPressed) { MoveLeft(); }
            if (RightPressed) { MoveRight(); }
        }

        static private void MoveLeft()
        {
            frametick++;

            if (frametick > 5)
            {
                frame--;
                frametick = 0;

                if (frame < 1)
                {
                    frame = 4;
                }
            }

            PlayerBall.PositionX -= (short)(Globals.BallMoveSpeed + Globals.ExtraMoveSpeed);
            if (PlayerBall.PositionX < 32 + PlayerBall.Radius)
            {
                PlayerBall.PositionX = (short)(32 + PlayerBall.Radius);
            }
        }

        static private void MoveRight()
        {

            frametick++;

            if (frametick > 5)
            {
                frame++;
                frametick = 0;

                if (frame > 4)
                {
                    frame = 1;
                }
            }

            PlayerBall.PositionX += (short)(Globals.BallMoveSpeed + Globals.ExtraMoveSpeed);
            if (PlayerBall.PositionX > 512 - PlayerBall.Radius)
            {
                PlayerBall.PositionX = (short)(512 - PlayerBall.Radius);
            }
        }
    }
}
