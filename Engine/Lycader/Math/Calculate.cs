using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lycader.Math
{
    using OpenTK;
    using Lycader.Math.Shapes;

    static public class Calc
    {
        static public float TwoPi = Convert.ToSingle(2.0f * System.Math.PI);

        public static Vector2 RotateAround(Vector2 point, Vector2 origin, float radians)
        {
            Vector2 left = point - origin;
            double radians2 = Calc.DirectionTo(origin, point) + (double)radians;
            MathHelper.RadiansToDegrees(radians2);
            Vector2 vec = new Vector2((float)System.Math.Cos((double)radians), (float)System.Math.Sin((double)radians));
            Vector2 vec2 = new Vector2((float)System.Math.Cos((double)(radians + 1.57079637f)), (float)System.Math.Sin((double)(radians + 1.57079637f)));
            left = left.X * vec + left.Y * vec2;
            return left + origin;
        }

        public static double DirectionTo(Vector2 from, Vector2 to)
        {
            double num = System.Math.Atan2((double)(to.Y - from.Y), (double)(to.X - from.X));
            if (num >= 0.0)
            {
                return num;
            }
            return num + 6.2831854820251465;
        }

        public static double AngleBetween(Vector2 v1, Vector2 v2)
        {
            return System.Math.Acos((double)(Vector2.Dot(v1, v2) / v1.Length / v2.Length));
        }

        public static float Distance(Vector2 p1, Vector2 p2)
        {
            return (float)System.Math.Sqrt(System.Math.Pow((double)(p2.X - p1.X), 2.0) + System.Math.Pow((double)(p2.Y - p1.Y), 2.0));
        }

        public static float DistanceSquared(Vector2 p1, Vector2 p2)
        {
            return (float)(System.Math.Pow((double)(p2.X - p1.X), 2.0) + System.Math.Pow((double)(p2.Y - p1.Y), 2.0));
        }

        public static Vector2 CirclePoint(float rotation, float radius)
        {
            return new Vector2(radius * (float)System.Math.Cos((double)rotation), radius * (float)System.Math.Sin((double)rotation));
        }

        public static float AngleDifference(float angle1, float angle2, bool inRadians = true)
        {
            float num = inRadians ? 6.28318548f : 360f;
            float num2 = angle1;
            float num3 = angle2;
            if (num2 > num)
            {
                num2 -= num;
            }
            if (num2 < 0f)
            {
                num2 += num;
            }
            if (num3 > num)
            {
                num3 -= num;
            }
            if (num3 < 0f)
            {
                num3 += num;
            }
            if (System.Math.Abs(num2 - num3) > num / 2f)
            {
                num3 -= num;
            }
            return num3 - num2;
        }

        public static bool WithinBounds(float p1, float p2, float value)
        {
            return System.Math.Min(p1, p2) < value && value < System.Math.Max(p1, p2);
        }

        public static bool BoundsIntersect(float p1, float p2, float p3, float p4, bool inclusive = true)
        {
            float num = System.Math.Min(p1, p2);
            float num2 = System.Math.Max(p1, p2);
            float num3 = System.Math.Min(p3, p4);
            float num4 = System.Math.Max(p3, p4);
            if (inclusive)
            {
                return num <= num4 && num2 >= num3;
            }
            return num < num4 && num2 > num3;
        }

        public static float Lerp(float min, float max, double amount)
        {
            return min + (max - min) * (float)amount;
        }

        public static float Lerp(float min, float max, float amount)
        {
            return min + (max - min) * amount;
        }

        public static int Wrap(int value, int min, int max)
        {
            int range_size = max - min + 1;

            if (value < min)
            {
                return value + range_size * ((min - value) / range_size + 1);
            }

            if (value > max)
            {
                return value - range_size * ((value - max) / range_size + 1);
            }

            return value;
        }

        public static bool SweepTest(System.Drawing.RectangleF staticRec, System.Drawing.RectangleF moveRec, Vector2 velocity, out System.Drawing.RectangleF solution, bool isPlatform = false)
        {
            System.Drawing.RectangleF a = moveRec;
            System.Drawing.RectangleF rectangleF = new System.Drawing.RectangleF(moveRec.X + velocity.X, moveRec.Y + velocity.Y, moveRec.Width, moveRec.Height);
            System.Drawing.RectangleF rect = System.Drawing.RectangleF.Union(a, rectangleF);
            if (!staticRec.IntersectsWith(rect))
            {
                solution = rectangleF;
                return false;
            }
            float num;
            if (velocity.X > 0f)
            {
                num = staticRec.Left - a.Right;
            }
            else
            {
                num = -(a.Left - staticRec.Right);
            }
            float num2;
            if (velocity.Y > 0f)
            {
                num2 = staticRec.Top - a.Bottom;
            }
            else
            {
                num2 = -(a.Top - staticRec.Bottom);
            }
            double num3 = (double)(num / velocity.X);
            double num4 = (double)(num2 / velocity.Y);
            if ((num3 < 0.0 && num4 < 0.0) || (num3 > 1.0 && num4 > 1.0))
            {
                solution = rectangleF;
                return false;
            }
            double num5;
            if (double.IsInfinity(num3) || double.IsInfinity(num4))
            {
                if (double.IsInfinity(num3) && !double.IsInfinity(num4))
                {
                    num5 = num4;
                }
                else
                {
                    if (!double.IsInfinity(num4) || double.IsInfinity(num3))
                    {
                        solution = rectangleF;
                        return false;
                    }
                    num5 = num3;
                }
            }
            else
            {
                num5 = System.Math.Max(num3, num4);
            }
            float num6;
            if (velocity.X > 0f)
            {
                num6 = staticRec.Right - a.Left;
            }
            else
            {
                num6 = -(a.Right - staticRec.Left);
            }
            float num7;
            if (velocity.Y > 0f)
            {
                num7 = staticRec.Bottom - a.Top;
            }
            else
            {
                num7 = -(a.Bottom - staticRec.Top);
            }
            double num8 = (double)(num6 / velocity.X);
            double num9 = (double)(num7 / velocity.Y);
            double num10;
            if (double.IsInfinity(num8) || double.IsInfinity(num9))
            {
                if (double.IsInfinity(num8) && !double.IsInfinity(num9))
                {
                    num10 = num9;
                }
                else
                {
                    if (!double.IsInfinity(num9) || double.IsInfinity(num8))
                    {
                        solution = rectangleF;
                        return false;
                    }
                    num10 = num8;
                }
            }
            else
            {
                num10 = System.Math.Min(num8, num9);
            }
            if (num10 < num5)
            {
                solution = rectangleF;
                return false;
            }
            Vector2 vector = new Vector2(a.X, a.Y) + velocity * (float)num5;
            if (isPlatform)
            {
                if (num3 > num4 && !double.IsInfinity(num3))
                {
                    solution = rectangleF;
                    return false;
                }
                if (num4 > num3 && !double.IsInfinity(num4) && velocity.Y < 0f)
                {
                    solution = rectangleF;
                    return false;
                }
                if (double.IsInfinity(num4))
                {
                    solution = rectangleF;
                    return false;
                }
                if (double.IsInfinity(num3) && velocity.Y <= 0f)
                {
                    solution = rectangleF;
                    return false;
                }
            }
            if (num3 > num4 && !double.IsInfinity(num3))
            {
                if (velocity.X > 0f)
                {
                    vector.X = staticRec.Left - a.Width;
                }
                else if (velocity.X < 0f)
                {
                    vector.X = staticRec.Right;
                }
            }
            if (num4 > num3 && !double.IsInfinity(num4))
            {
                if (velocity.Y > 0f)
                {
                    vector.Y = staticRec.Top - a.Height;
                }
                else if (velocity.Y < 0f)
                {
                    vector.Y = staticRec.Bottom;
                }
            }
            solution = new System.Drawing.RectangleF(vector.X, vector.Y, a.Width, a.Height);
            return true;
        }

        public static bool LineVLine(Line line1, Line line2, out Vector2 intersection, bool inclusive = true)
        {
            return Calc.LineVLine(line1.p1, line1.p2, line2.p1, line2.p2, out intersection, inclusive);
        }

        public static bool LineVLine(Vector2 p1, Vector2 p2, Vector2 p3, Vector2 p4, out Vector2 intersection, bool inclusive = true)
        {
            if ((p1.X - p2.X) * (p3.Y - p4.Y) - (p1.Y - p2.Y) * (p3.X - p4.X) == 0f)
            {
                intersection = Vector2.Zero;
                return false;
            }
            intersection = Vector2.Zero;
            intersection.X = ((p1.X * p2.Y - p1.Y * p2.X) * (p3.X - p4.X) - (p1.X - p2.X) * (p3.X * p4.Y - p3.Y * p4.X)) / ((p1.X - p2.X) * (p3.Y - p4.Y) - (p1.Y - p2.Y) * (p3.X - p4.X));
            intersection.Y = ((p1.X * p2.Y - p1.Y * p2.X) * (p3.Y - p4.Y) - (p1.Y - p2.Y) * (p3.X * p4.Y - p3.Y * p4.X)) / ((p1.X - p2.X) * (p3.Y - p4.Y) - (p1.Y - p2.Y) * (p3.X - p4.X));
            Range range = new Range(p1.X, p2.X);
            Range range2 = new Range(p3.X, p4.X);
            return Range.IsInside(range, intersection.X, inclusive) && Range.IsInside(range2, intersection.X, inclusive);
        }
     
        public static bool LineVRectangle(System.Drawing.RectangleF rec, Line line, out Vector2 intersection1, out Vector2? intersection2)
        {
            return Calc.LineVRectangle(rec, line.p1, line.p2, out intersection1, out intersection2);
        }

        public static bool LineVRectangle(System.Drawing.RectangleF rec, Vector2 l1P1, Vector2 l1P2, out Vector2 intersection1, out Vector2? intersection2)
        {
            Vector2 vector = new Vector2(rec.X, rec.Y);
            Vector2 vector2 = new Vector2(rec.Right, rec.Y);
            Vector2 vector3 = new Vector2(rec.Right, rec.Bottom);
            Vector2 vector4 = new Vector2(rec.X, rec.Bottom);
            Vector2 zero = Vector2.Zero;
            intersection1 = Vector2.Zero;
            intersection2 = null;
            bool flag = false;
            if (Calc.LineVLine(vector, vector2, l1P1, l1P2, out zero, true))
            {
                if (!flag)
                {
                    intersection1 = zero;
                    flag = true;
                }
                else
                {
                    intersection2 = new Vector2?(zero);
                }
            }
            if (Calc.LineVLine(vector2, vector3, l1P1, l1P2, out zero, true))
            {
                if (!flag)
                {
                    intersection1 = zero;
                    flag = true;
                }
                else
                {
                    intersection2 = new Vector2?(zero);
                }
            }
            if (Calc.LineVLine(vector3, vector4, l1P1, l1P2, out zero, true))
            {
                if (!flag)
                {
                    intersection1 = zero;
                    flag = true;
                }
                else
                {
                    intersection2 = new Vector2?(zero);
                }
            }
            if (Calc.LineVLine(vector4, vector, l1P1, l1P2, out zero, true))
            {
                if (!flag)
                {
                    intersection1 = zero;
                    flag = true;
                }
                else
                {
                    intersection2 = new Vector2?(zero);
                }
            }
            if (flag && intersection2.HasValue && Calc.Distance(intersection1, l1P1) > Calc.Distance(intersection2.Value, l1P1))
            {
                Vector2 value = intersection1;
                intersection1 = intersection2.Value;
                intersection2 = new Vector2?(value);
            }
            return flag;
        }

        public static bool CircleVLine(Circle circle, Line line, out Vector2 intersection1, out Vector2? intersection2)
        {
            Vector2 vector = line.PerpPoint(circle.center);
            float num = Vector2.Dot(vector - line.p1, Vector2.Normalize(line.p2 - line.p1));
            num /= line.Length;
            if (num >= 0f && num <= 1f && Calc.DistanceSquared(circle.center, vector) < circle.radius * circle.radius)
            {
                intersection1 = vector;
                intersection2 = null;
                return true;
            }
            if (Calc.DistanceSquared(circle.center, line.p1) < circle.radius * circle.radius)
            {
                intersection1 = line.p1;
                intersection2 = null;
                return true;
            }
            if (Calc.DistanceSquared(circle.center, line.p2) < circle.radius * circle.radius)
            {
                intersection1 = line.p2;
                intersection2 = null;
                return true;
            }
            intersection1 = Vector2.Zero;
            intersection2 = null;
            return false;
        }

        public static bool CircleVLineContinuous(Circle circle, Line line, Vector2 velocity, out float firstAmount, out float secondAmount, out Vector2 intersection)
        {
            if (velocity == Vector2.Zero)
            {
                intersection = Vector2.Zero;
                firstAmount = 1f;
                secondAmount = 1f;
                return false;
            }
            Vector2 left;
            Calc.LineVLine(circle.center, circle.center + velocity, line.p1, line.p2, out left, true);
            float num = Vector2.Dot(left - circle.center, Vector2.Normalize(velocity));
            double a = Calc.AngleBetween(velocity, line.p2 - line.p1);
            float length = velocity.Length;
            float val = (float)((double)num - (double)circle.radius / System.Math.Sin(a) * (double)System.Math.Sign(num)) / length;
            float val2 = (float)((double)num + (double)circle.radius / System.Math.Sin(a) * (double)System.Math.Sign(num)) / length;
            firstAmount = System.Math.Min(val, val2);
            secondAmount = System.Math.Max(val, val2);
            Vector2 vec = Vector2.Zero;
            if (line.IsOnLeft(firstAmount * velocity + circle.center))
            {
                vec = line.RightNormal;
            }
            else
            {
                vec = line.LeftNormal;
            }
            intersection = firstAmount * velocity + circle.center + vec * circle.radius;
            if ((line.p1.X != line.p2.X && line.XRange.IsInside(intersection.X, true)) || (line.p1.Y != line.p2.Y && line.YRange.IsInside(intersection.Y, true)))
            {
                return firstAmount > -(1f / length) && firstAmount <= 1f;
            }
            if (Calc.CircleVPointContinuous(circle, line.p1, velocity, out firstAmount, out secondAmount))
            {
                intersection = line.p1;
                return true;
            }
            if (Calc.CircleVPointContinuous(circle, line.p2, velocity, out firstAmount, out secondAmount))
            {
                intersection = line.p2;
                return true;
            }
            return false;
        }

        public static bool CircleVPointContinuous(Circle circle, Vector2 point, Vector2 velocity, out float firstAmount, out float secondAmount)
        {
            if (velocity == Vector2.Zero || circle.radius == 0f)
            {
                firstAmount = 1f;
                secondAmount = 1f;
                return false;
            }
            float length = velocity.Length;
            Line line = new Line(circle.center, circle.center + velocity, false);
            Vector2 vector = line.PerpPoint(point);
            float num = Calc.Distance(vector, point);
            if (num > circle.radius)
            {
                firstAmount = 1f;
                secondAmount = 1f;
                return false;
            }
            double a = 1.5707963705062866 - System.Math.Asin((double)(num / circle.radius));
            double num2 = System.Math.Sin(a) * (double)circle.radius;
            vector -= circle.center;
            float num3 = Vector2.Dot(vector, Vector2.Normalize(velocity));
            float val = (num3 - (float)num2) / length;
            float val2 = (num3 + (float)num2) / length;
            firstAmount = System.Math.Min(val, val2);
            secondAmount = System.Math.Max(val, val2);
            return firstAmount > -(1f / length) && firstAmount <= 1f;
        }
    }
}
