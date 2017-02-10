//-----------------------------------------------------------------------
// <copyright file="Collision2D.cs" company="Mooglegiant" >
//      Copyright (c) Mooglegiant. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Lycader.Collision
{
    using System.Drawing;

    using OpenTK;

    public static class Collision2D
    {
        public static bool IsColliding(ICollidable shape1, ICollidable shape2)
        {
            if (shape1 == null || shape2 == null)
            {
                return false;
            }

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

            if (shape1.GetType() == typeof(CircleCollidable))
            {
                if (shape2.GetType() == typeof(QuadCollidable))
                {
                    return QuadToQuad((QuadCollidable)shape1, (QuadCollidable)shape2);
                }

                if (shape2.GetType() == typeof(CircleCollidable))
                {
                    return CircleToCircle((CircleCollidable)shape1, (CircleCollidable)shape2);
                }
            }

            return false;
         }

        private static bool QuadToQuad(QuadCollidable shape1, QuadCollidable shape2)
        {
            return IsIntersected(new Vector2(shape1.Position.X, shape1.Position.Y + ((QuadCollidable)shape1).Height), new Vector2(shape1.Position.X + ((QuadCollidable)shape1).Width, shape1.Position.Y), new Vector2(shape2.Position.X, shape2.Position.Y + ((QuadCollidable)shape2).Height), new Vector2(shape2.Position.X + ((QuadCollidable)shape2).Width, shape2.Position.Y));
        }

        private static bool QuadToCircle(QuadCollidable shape1, CircleCollidable shape2)
        {
            return IsIntersected(shape1, shape2);
        }

        private static bool CircleToCircle(CircleCollidable shape1, CircleCollidable shape2)
        {
            return IsIntersected(shape1, shape2);
        }

        // l1: Top Left coordinate of first rectangle.
        // r1: Bottom Right coordinate of first rectangle.
        // l2: Top Left coordinate of second rectangle.
        // r2: Bottom Right coordinate of second rectangle.
        private static bool IsIntersected(Vector2 l1, Vector2 r1, Vector2 l2, Vector2 r2)
        {
            // >0 indicates an overlap
            //float xOverlap = System.Math.Max(0, System.Math.Min(l1.X, l2.X) - System.Math.Max(r1.X, r2.X));
            //float yOverlap = System.Math.Max(0, System.Math.Min(l1.Y, l2.Y) - System.Math.Max(r1.Y, r2.Y));

            // If one rectangle is on left side of other
            if (l1.X > r2.X || l2.X > r1.X)
            {
                return false;
            }

            // If one rectangle is above other
            if (l1.Y < r2.Y || l2.Y < r1.Y)
            {
                return false;
            }

            return true;
        }

        public static bool IsIntersected(QuadCollidable rectangle, CircleCollidable circle)
        {
            var halfWidth = rectangle.Width / 2;
            var halfHeight = rectangle.Height / 2;

            var dx = System.Math.Abs(circle.Center.X - rectangle.Center.X);
            var dy = System.Math.Abs(circle.Center.Y - rectangle.Center.Y);

            if (dx > (circle.Radius + halfWidth) || dy > (circle.Radius + halfHeight))
            {
                return false;
            }

            var circleDistance = new PointF
            {
                X = System.Math.Abs(circle.Center.X - rectangle.Center.X),
                Y = System.Math.Abs(circle.Center.Y - rectangle.Center.Y)
            };

            if (circleDistance.X <= halfWidth)
            {
                return true;
            }

            if (circleDistance.Y <= halfHeight)
            {
                return true;
            }

            var cornerDistanceSq = System.Math.Pow(circleDistance.X - halfWidth, 2) +
                        System.Math.Pow(circleDistance.Y - halfHeight, 2);

            return (cornerDistanceSq <= System.Math.Pow(circle.Radius, 2));
        }

        public static bool IsIntersected(CircleCollidable circle1, CircleCollidable circle2)
        {
            if (circle1.Center.X + circle1.Radius + circle2.Radius > circle2.Center.X
                && circle1.Center.X < circle2.Center.X + circle1.Radius + circle2.Radius
                && circle1.Center.Y + circle1.Radius + circle2.Radius > circle2.Center.Y
                && circle1.Center.Y < circle2.Center.Y + circle1.Radius + circle2.Radius)
            {
                // AABBs are overlapping, check detailed collison
                var deltaX = circle1.Center.X - circle2.Center.X;
                var deltaY = circle1.Center.Y - circle2.Center.Y;

                var distance = (deltaX * deltaX) + (deltaY * deltaY);

                if (distance < (circle1.Radius + circle2.Radius) * (circle1.Radius + circle2.Radius))
                {        
                    return true; // circles have collided
                }
            }

            return false;
        }  
    }
}
