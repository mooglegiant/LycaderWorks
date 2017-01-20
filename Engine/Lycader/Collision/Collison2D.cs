using OpenTK;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lycader.Collision
{
    static public class Collision2D
    {
        static public bool IsColliding(ICollidable shape1, ICollidable shape2)
        {
            if(shape1 == null || shape2 == null) { return false; }

            if (shape1.GetType() == typeof(QuadCollidable))
            {
                if (shape2.GetType() == typeof(QuadCollidable))
                {
                    return QuadToQuad((QuadCollidable)shape1, (QuadCollidable)shape2);
                }
                if (shape2.GetType() == typeof(CircleCollidable))
                {
                    return QuadToCircle((QuadCollidable)shape1, (CircleCollidable)shape2);
                }
            }

            return false;
         }

        static private bool QuadToQuad(QuadCollidable shape1, QuadCollidable shape2)
        {
            return IsIntersected(new Vector2(shape1.Position.X, shape1.Position.Y + ((QuadCollidable)shape1).Height), new Vector2(shape1.Position.X + ((QuadCollidable)shape1).Width, shape1.Position.Y), new Vector2(shape2.Position.X, shape2.Position.Y + ((QuadCollidable)shape2).Height), new Vector2(shape2.Position.X + ((QuadCollidable)shape2).Width, shape2.Position.Y));
        }

        static private bool QuadToCircle(QuadCollidable shape1, CircleCollidable shape2)
        {
            return IsIntersected(shape1, shape2);
        }

        //static private bool IsIntersected(QuadCollidable shape1, QuadCollidable shape2)
        //{
        //    shape1.Width

        //    //(QuadCollidable)shape1
        //    return !(position1.X > position2.X + shape2.Width || position1.X + shape1.Width < position2.X || position1.Y + shape1.Height > position2.Y || position1.Y < position2.Y + shape2.Height);
        //}

        // l1: Top Left coordinate of first rectangle.
        // r1: Bottom Right coordinate of first rectangle.
        // l2: Top Left coordinate of second rectangle.
        // r2: Bottom Right coordinate of second rectangle.
        static private bool IsIntersected(Vector2 l1, Vector2 r1, Vector2 l2, Vector2 r2)
        {
            // >0 indicates an overlap
            //float xOverlap = System.Math.Max(0, System.Math.Min(l1.X, l2.X) - System.Math.Max(r1.X, r2.X));
            //float yOverlap = System.Math.Max(0, System.Math.Min(l1.Y, l2.Y) - System.Math.Max(r1.Y, r2.Y));

            // If one rectangle is on left side of other
            if (l1.X > r2.X || l2.X > r1.X)
                return false;

            // If one rectangle is above other
            if (l1.Y < r2.Y || l2.Y < r1.Y)
                return false;

            return true;
        }

        public static bool IsIntersected(QuadCollidable rectangle, CircleCollidable circle)
        {

            var rectangleCenter = new PointF((rectangle.Position.X + rectangle.Width / 2),
                                             (rectangle.Position.Y + rectangle.Height / 2));

            var w = rectangle.Width / 2;
            var h = rectangle.Height / 2;

            var dx = System.Math.Abs(circle.Position.X - rectangleCenter.X);
            var dy = System.Math.Abs(circle.Position.Y - rectangleCenter.Y);

            if (dx > (circle.Radius + w) || dy > (circle.Radius + h)) return false;


            var circleDistance = new PointF
            {
                X = System.Math.Abs(circle.Position.X - rectangle.Position.X - w),
                Y = System.Math.Abs(circle.Position.Y - rectangle.Position.Y - h)
            };


            if (circleDistance.X <= (w))
            {
                return true;
            }

            if (circleDistance.Y <= (h))
            {
                return true;
            }

            var cornerDistanceSq = System.Math.Pow(circleDistance.X - w, 2) +
                        System.Math.Pow(circleDistance.Y - h, 2);

            return (cornerDistanceSq <= (System.Math.Pow(circle.Radius, 2)));
        }

        public static bool IsIntersected(PointF circle, float radius, RectangleF rectangle)
        {

            var rectangleCenter = new PointF((rectangle.X + rectangle.Width / 2),
                                             (rectangle.Y + rectangle.Height / 2));

            var w = rectangle.Width / 2;
            var h = rectangle.Height / 2;

            var dx = System.Math.Abs(circle.X - rectangleCenter.X);
            var dy = System.Math.Abs(circle.Y - rectangleCenter.Y);

            if (dx > (radius + w) || dy > (radius + h)) return false;


            var circleDistance = new PointF
            {
                X = System.Math.Abs(circle.X - rectangle.X - w),
                Y = System.Math.Abs(circle.Y - rectangle.Y - h)
            };


            if (circleDistance.X <= (w))
            {
                return true;
            }

            if (circleDistance.Y <= (h))
            {
                return true;
            }

            var cornerDistanceSq = System.Math.Pow(circleDistance.X - w, 2) +
                        System.Math.Pow(circleDistance.Y - h, 2);

            return (cornerDistanceSq <= (System.Math.Pow(radius, 2)));
        }
    }
}
