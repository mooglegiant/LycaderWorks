using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lycader.Graphics.Collision
{
    static public class Collision2D
    {
        static public bool IsColliding(Vector3 position1, ICollidable shape1, Vector3 position2, ICollidable shape2)
        {
            if (shape1.GetType() == typeof(QuadCollidable))
            {
                if (shape2.GetType() == typeof(QuadCollidable))
                {
                    IsColliding(new Vector2(position1.X, position1.Y + ((QuadCollidable)shape1).Height), new Vector2(position1.X + ((QuadCollidable)shape1).Width, position1.Y), new Vector2(position2.X, position2.Y + ((QuadCollidable)shape2).Height), new Vector2(position2.X + ((QuadCollidable)shape2).Width, position2.Y));
                }
            }

            return false;
         }

        //static private bool IsColliding(Vector3 position1, QuadCollidable shape1, Vector3 position2, QuadCollidable shape2)
        //{
        //    shape1.Width

        //    //(QuadCollidable)shape1
        //    return !(position1.X > position2.X + shape2.Width || position1.X + shape1.Width < position2.X || position1.Y + shape1.Height > position2.Y || position1.Y < position2.Y + shape2.Height);
        //}

        // l1: Top Left coordinate of first rectangle.
        // r1: Bottom Right coordinate of first rectangle.
        // l2: Top Left coordinate of second rectangle.
        // r2: Bottom Right coordinate of second rectangle.
        static private bool IsColliding(Vector2 l1, Vector2 r1, Vector2 l2, Vector2 r2)
        {

            // If one rectangle is on left side of other
            if (l1.X > r2.X || l2.X > r1.X)
                return false;

            // If one rectangle is above other
            if (l1.Y < r2.Y || l2.Y < r1.Y)
                return false;

            return true;
        }
    }
}
