using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lycader.Math.Shapes
{
    using OpenTK;

    public struct Polygon
    {
        public Vector2[] verts;

        public int NumVerts
        {
            get
            {
                return this.verts.Length;
            }
        }

        public bool IsComplex
        {
            get
            {
                for (int i = 0; i < this.NumVerts - 1; i++)
                {
                    int num = (i + 1) % this.NumVerts;
                    for (int j = i + 1; j < this.NumVerts; j++)
                    {
                        int num2 = (j + 1) % this.NumVerts;
                        Vector2 vector;
                        if (Calc.LineVLine(this.verts[i], this.verts[num], this.verts[j], this.verts[num2], out vector, false))
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
                if (this.NumVerts < 3)
                {
                    return false;
                }
                for (int i = 0; i < this.NumVerts; i++)
                {
                    int num2 = (i + 1) % this.NumVerts;
                    int num3 = (i + 2) % this.NumVerts;
                    double num4 = (double)((this.verts[num2].X - this.verts[i].X) * (this.verts[num3].Y - this.verts[num2].Y));
                    num4 -= (double)((this.verts[num2].Y - this.verts[i].Y) * (this.verts[num3].X - this.verts[num2].X));
                    if (num4 < 0.0)
                    {
                        num--;
                    }
                    else if (num4 > 0.0)
                    {
                        num++;
                    }
                }
                return num < 0;
            }
        }

        public bool IsConvex
        {
            get
            {
                int num = 0;
                if (this.NumVerts < 3)
                {
                    return true;
                }
                for (int i = 0; i < this.NumVerts; i++)
                {
                    int num2 = (i + 1) % this.NumVerts;
                    int num3 = (i + 2) % this.NumVerts;
                    double num4 = (double)((this.verts[num2].X - this.verts[i].X) * (this.verts[num3].Y - this.verts[num2].Y));
                    num4 -= (double)((this.verts[num2].Y - this.verts[i].Y) * (this.verts[num3].X - this.verts[num2].X));
                    if (num4 < 0.0)
                    {
                        num |= 1;
                    }
                    else if (num4 > 0.0)
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
                Vector2 vector = Vector2.Zero;
                for (int i = 0; i < this.NumVerts; i++)
                {
                    vector += this.verts[i];
                }
                return vector / (float)this.NumVerts;
            }
        }

        public Vector2 GetVertex(int index)
        {
            if (index < 0)
            {
                return this.verts[0];
            }
            if (index >= this.NumVerts)
            {
                return this.verts[this.NumVerts - 1];
            }
            return this.verts[index];
        }

        public Line GetEdge(int index)
        {
            if (index < 0)
            {
                return new Line(this.verts[0], this.verts[1], false);
            }
            if (index >= this.NumVerts)
            {
                return new Line(this.verts[this.NumVerts - 2], this.verts[this.NumVerts - 1], false);
            }
            return new Line(this.verts[index], this.verts[(index + 1) % this.NumVerts], false);
        }

        public Polygon(int numberOfVertices)
        {
            this.verts = new Vector2[numberOfVertices];
        }

        public Polygon(Vector2 p1, Vector2? p2 = null, Vector2? p3 = null, Vector2? p4 = null, Vector2? p5 = null)
        {
            int num = 1;
            if (p2.HasValue)
            {
                num++;
            }
            if (p3.HasValue)
            {
                num++;
            }
            if (p4.HasValue)
            {
                num++;
            }
            if (p5.HasValue)
            {
                num++;
            }
            this.verts = new Vector2[num];
            this.verts[0] = p1;
            int num2 = 1;
            if (p2.HasValue)
            {
                this.verts[num2] = p2.Value;
                num2++;
            }
            if (p3.HasValue)
            {
                this.verts[num2] = p3.Value;
                num2++;
            }
            if (p4.HasValue)
            {
                this.verts[num2] = p4.Value;
                num2++;
            }
            if (p5.HasValue)
            {
                this.verts[num2] = p5.Value;
                num2++;
            }
        }

        public Polygon(Vector2[] vertices)
        {
            this.verts = vertices;
        }
    }
}
