using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lycader.Math
{
    using OpenTK;
    public struct Range
    {
        public float min;

        public float max;

        public float Mid
        {
            get
            {
                return (this.min + this.max) / 2f;
            }
        }

        public float Length
        {
            get
            {
                return this.max - this.min;
            }
        }

        public Range(float val1, float val2)
        {
            this.min = System.Math.Min(val1, val2);
            this.max = System.Math.Max(val1, val2);
        }

        public static bool IsInside(Range range, float value, bool inclusive)
        {
            if (inclusive)
            {
                return value >= range.min && value <= range.max;
            }
            return value > range.min && value < range.max;
        }

        public bool IsInside(float value, bool inclusive)
        {
            if (inclusive)
            {
                return value >= this.min && value <= this.max;
            }
            return value > this.min && value < this.max;
        }

        public static bool Intersects(Range r1, Range r2, bool inclusive)
        {
            if (inclusive)
            {
                return r1.max >= r2.min && r1.min <= r2.max;
            }
            return r1.max > r2.min && r1.min < r2.max;
        }

        public bool Intersects(Range range, bool inclusive)
        {
            if (inclusive)
            {
                return this.max >= range.min && this.min <= range.max;
            }
            return this.max > range.min && this.min < range.max;
        }

        public static float OverlapAmount(Range range1, Range range2)
        {
            if (range1.Mid >= range2.Mid)
            {
                return range2.max - range1.min;
            }
            return -(range1.max - range2.min);
        }

        public float OverlapAmount(Range other)
        {
            if (this.Mid >= other.Mid)
            {
                return other.max - this.min;
            }
            return -(this.max - other.min);
        }

        public float TranslationTo(Range other)
        {
            if (this.max <= other.min)
            {
                return other.min - this.max;
            }
            return this.min - other.max;
        }

        public static Range operator +(Range r, float val)
        {
            return new Range(r.min + val, r.max + val);
        }

        public static Range operator -(Range r, float val)
        {
            return new Range(r.min - val, r.max - val);
        }
    }
}
