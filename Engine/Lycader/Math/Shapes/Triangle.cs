using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lycader.Math.Shapes
{
    using OpenTK;
    public struct Triangle
    {
        public Vector2 v1;

        public Vector2 v2;

        public Vector2 v3;

        public bool IsClockwise
        {
            get
            {
                return !Line.IsOnLeft(new Line(this.v1, this.v2, false), this.v3);
            }
        }

        public Vector2 Center
        {
            get
            {
                return (this.v1 + this.v2 + this.v3) / 3f;
            }
        }

        public float Area
        {
            get
            {
                float num = Calc.Distance(this.v1, this.v2);
                float num2 = Calc.Distance(this.v2, this.v3);
                float num3 = Calc.Distance(this.v3, this.v1);
                float num4 = (num + num2 + num3) / 2f;
                return (float)System.Math.Sqrt((double)(num4 * (num4 - num) * (num4 - num2) * (num4 - num3)));
            }
        }

        public Line L1
        {
            get
            {
                return new Line(this.v1, this.v2, false);
            }
        }

        public Line L2
        {
            get
            {
                return new Line(this.v2, this.v3, false);
            }
        }

        public Line L3
        {
            get
            {
                return new Line(this.v3, this.v1, false);
            }
        }

        public Vector2 GetVertex(int index)
        {
            if (index < 0 || index > 2)
            {
                throw new System.ArgumentOutOfRangeException("index");
            }
            switch (index)
            {
                case 1:
                    return this.v2;
                case 2:
                    return this.v3;
                default:
                    return this.v1;
            }
        }

        public void SetVertex(int index, Vector2 value)
        {
            if (index < 0 || index > 2)
            {
                throw new System.ArgumentOutOfRangeException("index");
            }
            switch (index)
            {
                case 1:
                    this.v2 = value;
                    return;
                case 2:
                    this.v3 = value;
                    return;
                default:
                    this.v1 = value;
                    return;
            }
        }

        public Line GetEdge(int index)
        {
            if (index < 0 || index > 2)
            {
                throw new System.ArgumentOutOfRangeException("index");
            }
            return new Line(this.GetVertex(index), this.GetVertex((index + 1) % 3), false);
        }

        public Triangle(Vector2 v1, Vector2 v2, Vector2 v3, bool rearrange = false)
        {
            this.v1 = v1;
            this.v2 = v2;
            this.v3 = v3;
            if (rearrange && !this.IsClockwise)
            {
                this.ReverseOrder();
            }
        }

        public void ReverseOrder()
        {
            Vector2 vector = this.v1;
            this.v1 = this.v3;
            this.v3 = vector;
        }

        public static Triangle RotateAround(Triangle triangle, Vector2 origin, float radians)
        {
            return new Triangle(Calc.RotateAround(triangle.v1, origin, radians), Calc.RotateAround(triangle.v2, origin, radians), Calc.RotateAround(triangle.v3, origin, radians), false);
        }

        public void RotateAround(Vector2 origin, float radians)
        {
            this.v1 = Calc.RotateAround(this.v1, origin, radians);
            this.v2 = Calc.RotateAround(this.v2, origin, radians);
            this.v3 = Calc.RotateAround(this.v3, origin, radians);
        }

        public static bool Contains(Triangle triangle, Vector2 point)
        {
            if (triangle.IsClockwise)
            {
                return !triangle.L1.IsOnLeft(point) && !triangle.L2.IsOnLeft(point) && !triangle.L3.IsOnLeft(point);
            }
            return triangle.L1.IsOnLeft(point) && triangle.L2.IsOnLeft(point) && triangle.L3.IsOnLeft(point);
        }

        public bool Contains(Vector2 point)
        {
            if (this.IsClockwise)
            {
                return !this.L1.IsOnLeft(point) && !this.L2.IsOnLeft(point) && !this.L3.IsOnLeft(point);
            }
            return this.L1.IsOnLeft(point) && this.L2.IsOnLeft(point) && this.L3.IsOnLeft(point);
        }

        public static Triangle operator +(Triangle triangle, Vector2 offset)
        {
            return new Triangle(triangle.v1 + offset, triangle.v2 + offset, triangle.v3 + offset, false);
        }

        public static Triangle operator -(Triangle triangle, Vector2 offset)
        {
            return new Triangle(triangle.v1 - offset, triangle.v2 - offset, triangle.v3 - offset, false);
        }

        public static explicit operator Polygon(Triangle t)
        {
            Polygon result = new Polygon(3);
            result.verts[0] = t.v1;
            result.verts[1] = t.v2;
            result.verts[2] = t.v3;
            return result;
        }
    }
}
