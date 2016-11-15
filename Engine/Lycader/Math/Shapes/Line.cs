using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lycader.Math.Shapes
{
    using OpenTK;

    public struct Line
    {
        public Vector2 p1;

        public Vector2 p2;

        public float Length
        {
            get
            {
                return Calc.Distance(this.p1, this.p2);
            }
        }

        public double HeadingDir
        {
            get
            {
                return Calc.DirectionTo(this.p1, this.p2);
            }
        }

        public float Slope
        {
            get
            {
                if (this.UndefinedSlope)
                {
                    System.Console.WriteLine("Tried to get slope of a line with undefined slope");
                    return 3.40282347E+38f;
                }
                return (this.p2.Y - this.p1.Y) / (this.p2.X - this.p1.X);
            }
        }

        public Vector2 MidPoint
        {
            get
            {
                return (this.p1 + this.p2) / 2f;
            }
        }

        public Range XRange
        {
            get
            {
                return new Range(this.p1.X, this.p2.X);
            }
        }

        public Range YRange
        {
            get
            {
                return new Range(this.p1.Y, this.p2.Y);
            }
        }

        public bool UndefinedSlope
        {
            get
            {
                return this.p1.X == this.p2.X;
            }
        }

        public double LeftNormalDir
        {
            get
            {
                double num = this.HeadingDir - 1.5707963705062866;
                if (num >= 0.0)
                {
                    return num;
                }
                return num + 6.2831854820251465;
            }
        }

        public double RightNormalDir
        {
            get
            {
                double num = this.HeadingDir + 1.5707963705062866;
                if (num <= 6.2831854820251465)
                {
                    return num;
                }
                return num - 6.2831854820251465;
            }
        }

        public Vector2 LeftNormal
        {
            get
            {
                return Vector2.Normalize((this.p2 - this.p1).PerpendicularLeft);
            }
        }

        public Vector2 RightNormal
        {
            get
            {
                return Vector2.Normalize((this.p2 - this.p1).PerpendicularRight);
            }
        }

        public System.Drawing.RectangleF BoundingRec
        {
            get
            {
                return new System.Drawing.RectangleF(System.Math.Min(this.p1.X, this.p2.X), System.Math.Min(this.p1.Y, this.p2.Y), System.Math.Max(this.p1.X, this.p2.X) - System.Math.Min(this.p1.X, this.p2.X), System.Math.Max(this.p1.Y, this.p2.Y) - System.Math.Min(this.p1.Y, this.p2.Y));
            }
        }

        public Vector2 GetPoint(int index)
        {
            if (index < 0 || index > 1)
            {
                throw new System.ArgumentOutOfRangeException("index");
            }
            if (index != 0)
            {
                return this.p2;
            }
            return this.p1;
        }

        public Line(Vector2 p1, Vector2 p2, bool rearrange = false)
        {
            if (rearrange)
            {
                this.p1 = ((p1.X <= p2.X) ? p1 : p2);
                this.p2 = ((p1.X <= p2.X) ? p2 : p1);
                return;
            }
            this.p1 = p1;
            this.p2 = p2;
        }

        public static bool Intersects(Line line1, Line line2, out Vector2 intersect)
        {
            return Calc.LineVLine(line1.p1, line1.p2, line2.p1, line2.p2, out intersect, true);
        }

        public bool Intersects(Line line, out Vector2 intersect)
        {
            return Calc.LineVLine(this.p1, this.p2, line.p1, line.p2, out intersect, true);
        }

        public static Vector2 GetLerpPoint(Line line, float lerp)
        {
            return line.p1 + (line.p2 - line.p1) * lerp;
        }

        public Vector2 GetLerpPoint(float lerp)
        {
            return this.p1 + (this.p2 - this.p1) * lerp;
        }

        public Vector2 GetPointAtX(float x, bool relative = false)
        {
            if (!this.UndefinedSlope)
            {
                float num = relative ? x : (x - this.p1.X);
                return new Vector2(x, this.p1.Y + this.Slope * num);
            }
            if (x < this.p1.X)
            {
                return this.p1;
            }
            return this.p2;
        }

        public static bool IsAbove(Line line, Vector2 point)
        {
            if (line.p2.X != line.p1.X)
            {
                return line.GetPointAtX(point.X, false).Y >= point.Y;
            }
            if (line.p2.Y >= line.p1.Y)
            {
                return point.X <= line.p1.X;
            }
            return point.X >= line.p1.X;
        }

        public bool IsAbove(Vector2 point)
        {
            if (this.p2.X != this.p1.X)
            {
                return this.GetPointAtX(point.X, false).Y >= point.Y;
            }
            if (this.p2.Y >= this.p1.Y)
            {
                return point.X <= this.p1.X;
            }
            return point.X >= this.p1.X;
        }

        public static bool IsOnLeft(Line line, Vector2 point)
        {
            if (line.p2.X == line.p1.X)
            {
                if (line.p2.Y >= line.p1.Y)
                {
                    return point.X >= line.p1.X;
                }
                return point.X <= line.p1.X;
            }
            else
            {
                if (Calc.WithinBounds(0f, 3.14159274f, (float)line.LeftNormalDir))
                {
                    return !line.IsAbove(point);
                }
                return line.IsAbove(point);
            }
        }

        public bool IsOnLeft(Vector2 point)
        {
            if (this.p2.X == this.p1.X)
            {
                if (this.p2.Y >= this.p1.Y)
                {
                    return point.X >= this.p1.X;
                }
                return point.X <= this.p1.X;
            }
            else
            {
                if (Calc.WithinBounds(0f, 3.14159274f, (float)this.LeftNormalDir))
                {
                    return !this.IsAbove(point);
                }
                return this.IsAbove(point);
            }
        }

        public static Vector2 PerpPoint(Line line, Vector2 testPoint)
        {
            Vector2 vector = line.p1;
            Vector2 vector2 = line.p2;
            Vector2 vector3 = testPoint;
            Vector2 vector4 = testPoint + line.RightNormal;
            Vector2 arg_25_0 = Vector2.Zero;
            return new Vector2(((vector.X * vector2.Y - vector.Y * vector2.X) * (vector3.X - vector4.X) - (vector.X - vector2.X) * (vector3.X * vector4.Y - vector3.Y * vector4.X)) / ((vector.X - vector2.X) * (vector3.Y - vector4.Y) - (vector.Y - vector2.Y) * (vector3.X - vector4.X)), ((vector.X * vector2.Y - vector.Y * vector2.X) * (vector3.Y - vector4.Y) - (vector.Y - vector2.Y) * (vector3.X * vector4.Y - vector3.Y * vector4.X)) / ((vector.X - vector2.X) * (vector3.Y - vector4.Y) - (vector.Y - vector2.Y) * (vector3.X - vector4.X)));
        }

        public Vector2 PerpPoint(Vector2 testPoint)
        {
            Vector2 vector = this.p1;
            Vector2 vector2 = this.p2;
            Vector2 vector3 = testPoint;
            Vector2 vector4 = testPoint + this.RightNormal;
            Vector2 arg_22_0 = Vector2.Zero;
            return new Vector2(((vector.X * vector2.Y - vector.Y * vector2.X) * (vector3.X - vector4.X) - (vector.X - vector2.X) * (vector3.X * vector4.Y - vector3.Y * vector4.X)) / ((vector.X - vector2.X) * (vector3.Y - vector4.Y) - (vector.Y - vector2.Y) * (vector3.X - vector4.X)), ((vector.X * vector2.Y - vector.Y * vector2.X) * (vector3.Y - vector4.Y) - (vector.Y - vector2.Y) * (vector3.X * vector4.Y - vector3.Y * vector4.X)) / ((vector.X - vector2.X) * (vector3.Y - vector4.Y) - (vector.Y - vector2.Y) * (vector3.X - vector4.X)));
        }

        public static Line RotateAround(Line line, Vector2 origin, float radians)
        {
            return new Line(Calc.RotateAround(line.p1, origin, radians), Calc.RotateAround(line.p2, origin, radians), false);
        }

        public void RotateAround(Vector2 origin, float radians)
        {
            this.p1 = Calc.RotateAround(this.p1, origin, radians);
            this.p2 = Calc.RotateAround(this.p2, origin, radians);
        }

        public static Line Translate(Line line, Vector2 offset)
        {
            return line + offset;
        }

        public void Translate(Vector2 offset)
        {
            this.p1 += offset;
            this.p2 += offset;
        }

        public static Line operator +(Line line, Vector2 offset)
        {
            return new Line(line.p1 + offset, line.p2 + offset, false);
        }

        public static Line operator -(Line line, Vector2 offset)
        {
            return new Line(line.p1 - offset, line.p2 - offset, false);
        }
    }
}
