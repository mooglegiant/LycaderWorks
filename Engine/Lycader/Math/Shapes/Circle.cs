namespace Lycader.Math.Shapes
{
    using OpenTK;
    using Lycader.Math;

    public struct Circle
    {
        public Vector2 center;

        public float radius;

        public float Area
        {
            get
            {
                return (float)(3.1415927410125732 * System.Math.Pow((double)this.radius, 2.0));
            }
        }

        public float Circumference
        {
            get
            {
                return 6.28318548f * this.radius;
            }
        }

        public System.Drawing.RectangleF ColRec
        {
            get
            {
                return new System.Drawing.RectangleF(this.center.X - this.radius, this.center.Y - this.radius, this.radius * 2f, this.radius * 2f);
            }
        }

        public System.Drawing.RectangleF InnerRec
        {
            get
            {
                float num = (float)(System.Math.Cos(1.5707963705062866) * (double)this.radius);
                return new System.Drawing.RectangleF(this.center.X - num, this.center.Y - num, num * 2f, num * 2f);
            }
        }

        public Circle(Vector2 center, float radius)
        {
            this.center = center;
            this.radius = radius;
        }

        public Circle(System.Drawing.RectangleF rec)
        {
            this.radius = System.Math.Max(Calc.Distance(new Vector2(rec.X, rec.Y), new Vector2(rec.Right, rec.Bottom)), Calc.Distance(new Vector2(rec.Right, rec.Y), new Vector2(rec.X, rec.Bottom)));
            this.center = new Vector2(rec.X + rec.Width / 2f, rec.Y + rec.Height / 2f);
        }

        public Circle(System.Drawing.Rectangle rec)
        {
            this.radius = System.Math.Max(Calc.Distance(new Vector2((float)rec.X, (float)rec.Y), new Vector2((float)rec.Right, (float)rec.Bottom)), Calc.Distance(new Vector2((float)rec.Right, (float)rec.Y), new Vector2((float)rec.X, (float)rec.Bottom)));
            this.center = new Vector2((float)rec.X + (float)rec.Width / 2f, (float)rec.Y + (float)rec.Height / 2f);
        }

        public static bool Intersects(Circle c1, Circle c2)
        {
            return Calc.Distance(c1.center, c2.center) < c1.radius + c2.radius;
        }

        public bool Intersects(Circle circle)
        {
            return Calc.Distance(this.center, circle.center) < this.radius + circle.radius;
        }

        public static bool IsInside(Circle circle, Vector2 point)
        {
            return Calc.Distance(circle.center, point) < circle.radius;
        }

        public bool IsInside(Vector2 point)
        {
            return Calc.Distance(this.center, point) < this.radius;
        }

        public bool IsInside(float x, float y)
        {
            return Calc.Distance(this.center, new Vector2(x, y)) < this.radius;
        }

        public static Circle RotateAround(Circle circle, Vector2 origin, float radians)
        {
            return new Circle(Calc.RotateAround(circle.center, origin, radians), circle.radius);
        }

        public void RotateAround(Vector2 origin, float radians)
        {
            this.center = Calc.RotateAround(this.center, origin, radians);
        }

        public static Circle Translate(Circle circle, Vector2 offset)
        {
            return circle + offset;
        }

        public void Translate(Vector2 offset)
        {
            this.center += offset;
        }

        public static Circle FromRectangle(System.Drawing.Rectangle rec)
        {
            float num = System.Math.Max(Calc.Distance(new Vector2((float)rec.X, (float)rec.Y), new Vector2((float)rec.Right, (float)rec.Bottom)), Calc.Distance(new Vector2((float)rec.Right, (float)rec.Y), new Vector2((float)rec.X, (float)rec.Bottom)));
            return new Circle(new Vector2((float)rec.X + (float)rec.Width / 2f, (float)rec.Y + (float)rec.Height / 2f), num);
        }

        public static Circle FromRectangle(System.Drawing.RectangleF rec)
        {
            float num = System.Math.Max(Calc.Distance(new Vector2(rec.X, rec.Y), new Vector2(rec.Right, rec.Bottom)), Calc.Distance(new Vector2(rec.Right, rec.Y), new Vector2(rec.X, rec.Bottom)));
            return new Circle(new Vector2(rec.X + rec.Width / 2f, rec.Y + rec.Height / 2f), num);
        }

        public static Circle operator +(Circle circle, Vector2 offset)
        {
            return new Circle(circle.center + offset, circle.radius);
        }

        public static Circle operator -(Circle circle, Vector2 offset)
        {
            return new Circle(circle.center - offset, circle.radius);
        }
    }
}
