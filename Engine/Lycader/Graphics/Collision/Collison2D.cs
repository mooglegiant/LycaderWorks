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
                    IsColliding(position1, (QuadCollidable)shape1, position2, (QuadCollidable)shape2);
                }
            }

            return false;
         }

        static private bool IsColliding(Vector3 position1, QuadCollidable shape1, Vector3 position2, QuadCollidable shape2)
        {
            return !(position1.X > position2.X + shape2.Width || position1.X + shape1.Width < position2.X || position1.Y + shape1.Height > position2.Y || position1.Y < position2.Y + shape2.Height);
        }
    }
}
