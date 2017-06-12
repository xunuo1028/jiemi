using System;
using UnityEngine;
using System.Collections;

namespace Medusa
{
    [System.Serializable]
    public struct fixed3 
    {
        public long x;
        public long y;
        public long z;

        public static readonly fixed3 zero;
        public static readonly fixed3 one;
        public static readonly fixed3 half;
        public static readonly fixed3 forward;
        public static readonly fixed3 up;
        public static readonly fixed3 right;

        static fixed3()
        {
            zero = new fixed3 (0, 0, 0);
            one = new fixed3 (FixedMath.PRECISION, FixedMath.PRECISION, FixedMath.PRECISION);
            half = new fixed3 (FixedMath.PRECISION / 2, FixedMath.PRECISION / 2, FixedMath.PRECISION / 2);
            forward = new fixed3 (0, 0, FixedMath.PRECISION);
            up = new fixed3 (0, FixedMath.PRECISION, 0);
            right = new fixed3 (FixedMath.PRECISION, 0, 0);
        }

        public fixed3 (UnityEngine.Vector3 v3)
        {
            this.x = UnityEngine.Mathf.RoundToInt (v3.x * FixedMath.PRECISION);
            this.y = UnityEngine.Mathf.RoundToInt (v3.y * FixedMath.PRECISION);
            this.z = UnityEngine.Mathf.RoundToInt (v3.z * FixedMath.PRECISION);
        }

        public fixed3(long x, long y, long z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public fixed3 GetHalf()
        {
            this.x = this.x >> 1;
            this.y = this.y >> 1;
            this.z = this.z >> 1;
            return this;
        }

        public long this[int i]
        {
            get
            {
                return ((i != 0) ? ((i != 1) ? this.z : this.y) : this.x);
            }
            set
            {
                if (i == 0)
                {
                    this.x = value;
                }
                else if (i == 1)
                {
                    this.y = value;
                }
                else
                {
                    this.z = value;
                }
            }
        }

        public long GetMax()
        {
            if (x >= y && x >= z)
            {
                return x;
            }
            if (y >= x && y >= z)
            {
                return y;
            }
            return z;
        }

        public static long Dot (fixed3 lhs, fixed3 rhs)
        {
            return FixedMath.Divide ((lhs.x * rhs.x) + (lhs.y * rhs.y) + (lhs.z * rhs.z), FixedMath.PRECISION);
        }

        public static fixed3 Cross(fixed3 lhs, fixed3 rhs)
        {
            return new fixed3 (FixedMath.Divide ((lhs.y * rhs.z) - (lhs.z * rhs.y), FixedMath.PRECISION), FixedMath.Divide ((lhs.z * rhs.x) - (lhs.x * rhs.z), FixedMath.PRECISION), FixedMath.Divide ((lhs.x * rhs.y) - (lhs.y * rhs.x), FixedMath.PRECISION));
        }

        public long sqrMagnitude
        {
            get
            {
                return (x * x) + (y * y) + (z * z);
            }
        }

        public long magnitude
        {
            get
            {
                return (long)FixedMath.Sqrt((uint)sqrMagnitude);
            }
        }

        public void Normalize(out long magnitudeBeforeNormalize)
        {
            magnitudeBeforeNormalize = magnitude;
            if (magnitudeBeforeNormalize == 0L)
            {
                return;
            }
            else if (magnitudeBeforeNormalize == FixedMath.PRECISION)
            {
                return;
            }
            this.x = FixedMath.Divide(x, magnitudeBeforeNormalize);
            this.y = FixedMath.Divide(y, magnitudeBeforeNormalize);
            this.z = FixedMath.Divide(z, magnitudeBeforeNormalize);
        }

        public void Normalize()
        {
            long magnitudeBeforeNormalize = magnitude;
            if (magnitudeBeforeNormalize == 0L)
            {
                return;
            }
            else if (magnitudeBeforeNormalize == FixedMath.PRECISION)
            {
                return;
            }
            this.x = FixedMath.Divide(x, magnitudeBeforeNormalize);
            this.y = FixedMath.Divide(y, magnitudeBeforeNormalize);
            this.z = FixedMath.Divide(z, magnitudeBeforeNormalize);
        }

        public override bool Equals(object o)
        {
            if (o == null)
            {
                return false;
            }
            fixed3 num = (fixed3)o;
            return (((this.x == num.x) && (this.y == num.y)) && (this.z == num.z));
        }

        public override int GetHashCode()
        {
            return (int)(((this.x * 4616005L) ^ (this.y * 1209353L)) ^ (this.z * 83492791L));
        }

        public static bool operator ==(fixed3 lhs, fixed3 rhs)
        {
            return (((lhs.x == rhs.x) && (lhs.y == rhs.y)) && (lhs.z == rhs.z));
        }

        public static bool operator !=(fixed3 lhs, fixed3 rhs)
        {
            return (((lhs.x != rhs.x) || (lhs.y != rhs.y)) || (lhs.z != rhs.z));
        }

        public static fixed3 operator -(fixed3 lhs, fixed3 rhs)
        {
            lhs.x -= rhs.x;
            lhs.y -= rhs.y;
            lhs.z -= rhs.z;
            return lhs;
        }

        public static fixed3 operator -(fixed3 lhs)
        {
            lhs.x = -lhs.x;
            lhs.y = -lhs.y;
            lhs.z = -lhs.z;
            return lhs;
        }

        public static fixed3 operator +(fixed3 lhs, fixed3 rhs)
        {
            lhs.x += rhs.x;
            lhs.y += rhs.y;
            lhs.z += rhs.z;
            return lhs;
        }

        public static fixed3 operator *(fixed3 lhs, long rhs)
        {
            lhs.x *= rhs;
            lhs.y *= rhs;
            lhs.z *= rhs;
            return lhs;
        }

        public Vector3 ToVector3()
        {
            Vector3 value;
            value.x = (float)x / (float)FixedMath.PRECISION;
            value.y = (float)y / (float)FixedMath.PRECISION;
            value.z = (float)z / (float)FixedMath.PRECISION;
            return value;
        }

        public fixed2 xz
        {
            get
            {
                fixed2 value = new fixed2(x, z);
                return value;
            }

            set
            {
                x = value.x;
                z = value.y;
            }
        }

        public static fixed3 Lerp(fixed3 fromValue, fixed3 toValue, factor lerpT)
        {
            fixed3 returnValue = fromValue;
            returnValue.x = lerpT.numerator * (toValue.x - fromValue.x) / lerpT.denominator + fromValue.x;
            returnValue.y = lerpT.numerator * (toValue.y - fromValue.y) / lerpT.denominator + fromValue.y;
            returnValue.z = lerpT.numerator * (toValue.z - fromValue.z) / lerpT.denominator + fromValue.z;
            return returnValue;
        }
    }
}