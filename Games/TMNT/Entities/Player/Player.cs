using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

using SDLGE;
using SdlDotNet.Input;
using SdlDotNet.Graphics.Sprites;
using SdlDotNet.Graphics;

using Game.Base;

namespace Game.Objects
{
    public class Player
    {
        public int Turtle, Life, Score, Lives, Continues;

        public Point Location;
        private bool left, right, down, up, jump, attack;
        public MovementState State;

        public Player(int turtle)
        {
            Score = 0;
            Life = 17;
            Turtle = turtle;
            Lives = 3;
            Continues = 3;
            State = MovementState.Standing;
        }

        public void Update()
        {
            if (left)
            {
                Location.X -= 1;
            }
            else if(right)
            {
                Location.X += 1;
            }

            if (up)
            {
                Location.Y -= 1;
            }
            else if (down)
            {
                Location.Y += 1;
            }
        }

        #region Buttons

        public void PressLeft(bool Pressed)
        {
            left = Pressed;
        }

        public void PressRight(bool Pressed)
        {
            right = Pressed;
        }

        public void PressDown(bool Pressed)
        {
            down = Pressed;
        }

        public void PressUp(bool Pressed)
        {
            up = Pressed;
        }

        public void PressJump(bool Pressed)
        {
            jump = Pressed;
        }

        public void PressAttack(bool Pressed)
        {
            attack = Pressed;
        }
        #endregion
    }

    public enum MovementState
    {
        Standing, Attacking, Jumping, JumpAttacking, WalkingLeft, WalkingRight, WalkingUp, WalkingDown
    }
}
