//-----------------------------------------------------------------------
// <copyright file="Quad.cs" company="Mooglegiant" >
//      Copyright (c) Mooglegiant. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Lycader.Math.Shapes
{
    using OpenTK;

    public struct Quad
    {
        public Vector2 v1;

        public Vector2 v2;

        public Vector2 v3;

        public Vector2 v4;

        public bool IsComplex
        {
            get
            {
                for (int i = 0; i < 3; i++)
                {
                    int index = (i + 1) % 4;
                    for (int j = i + 1; j < 4; j++)
                    {
                        int index2 = (j + 1) % 4;
                        Vector2 vector;
                        if (Calculate.LineVLine(this.GetVertex(i), this.GetVertex(index), this.GetVertex(j), this.GetVertex(index2), out vector, false))
                        {
                            return true;
                        }
                    }
                }

                return false;
            }
        }

        public bool IsClockwise
        {
            get
            {
                int num = 0;
                for (int i = 0; i < 4; i++)
                {
                    int index = (i + 1) % 4;
                    int index2 = (i == 0) ? 3 : (i - 1);
                    bool flag = new Line(Vector2.Zero, this.GetVertex(index) - this.GetVertex(i), false).IsOnLeft(this.GetVertex(i) - this.GetVertex(index2));
                    num += flag ? 1 : -1;
                }

                return num >= 0;
            }
        }

        public bool IsConvex
        {
            get
            {
                int num = 0;
                for (int i = 0; i < 4; i++)
                {
                    int index = (i + 1) % 4;
                    int index2 = (i + 2) % 4;
                    double num2 = (double)((this.GetVertex(index).X - this.GetVertex(i).X) * (this.GetVertex(index2).Y - this.GetVertex(index).Y));
                    num2 -= (double)((this.GetVertex(index).Y - this.GetVertex(i).Y) * (this.GetVertex(index2).X - this.GetVertex(index).X));

                    if (num2 < 0.0)
                    {
                        num |= 1;
                    }
                    else if (num2 > 0.0)
                    {
                        num |= 2;
                    }

                    if (num == 3)
                    {
                        return false;
                    }
                }

                return num == 0 || true;
            }
        }

        public Vector2 Center
        {
            get
            {
                return (this.v1 + this.v2 + this.v3 + this.v4) / 4f;
            }
        }

        public Vector2 GetVertex(int index)
        {
            if (index < 0 || index > 3)
            {
                throw new System.ArgumentOutOfRangeException("index");
            }

            switch (index)
            {
                case 1:
                    return this.v2;
                case 2:
                    return this.v3;
                case 3:
                    return this.v4;
                default:
                    return this.v1;
            }
        }

        public void SetVertex(int index, Vector2 value)
        {
            if (index < 0 || index > 3)
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
                case 3:
                    this.v4 = value;
                    return;
                default:
                    this.v1 = value;
                    return;
            }
        }

        public Line GetEdge(int index)
        {
            if (index < 0 || index > 3)
            {
                throw new System.ArgumentOutOfRangeException("index");
            }

            return new Line(this.GetVertex(index), this.GetVertex((index + 1) % 4), false);
        }

        /// <summary>
        /// Initializes a new instance of the Quad struct
        /// </summary>
        public Quad(Vector2 p1, Vector2 p2, Vector2 p3, Vector2 p4)
        {
            this.v1 = p1;
            this.v2 = p2;
            this.v3 = p3;
            this.v4 = p4;
        }

        /// <summary>
        /// Initializes a new instance of the Quad struct
        /// </summary>
        public Quad(System.Drawing.Rectangle rec)
        {
            this.v1 = new Vector2((float)rec.X, (float)rec.Y);
            this.v2 = new Vector2((float)rec.Right, (float)rec.Y);
            this.v3 = new Vector2((float)rec.Right, (float)rec.Bottom);
            this.v4 = new Vector2((float)rec.X, (float)rec.Bottom);
        }

        /// <summary>
        /// Initializes a new instance of the Quad struct
        /// </summary>
        public Quad(System.Drawing.RectangleF rec)
        {
            this.v1 = new Vector2(rec.X, rec.Y);
            this.v2 = new Vector2(rec.Right, rec.Y);
            this.v3 = new Vector2(rec.Right, rec.Bottom);
            this.v4 = new Vector2(rec.X, rec.Bottom);
        }

        /// <summary>
        /// Initializes a new instance of the Quad struct
        /// </summary>
        public Quad(Vector2[] verts)
        {
            if (verts.Length < 4 || verts.Length > 4)
            {
                throw new System.ArgumentException("The vertex array was not 4 nodes long.");
            }

            this.v1 = verts[0];
            this.v2 = verts[1];
            this.v3 = verts[2];
            this.v4 = verts[3];
        }

        public static explicit operator Polygon(Quad q)
        {
            Polygon result = new Polygon(4);
            result.verts[0] = q.v1;
            result.verts[1] = q.v2;
            result.verts[2] = q.v3;
            result.verts[3] = q.v4;
            return result;
        }
    }
}
