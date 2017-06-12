/// <summary>
/// DISCRIPTION:
/// CODERNAME(CN):jackal
/// CREATEDATE:#CREATIONDATE#
/// </summary>

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Medusa
{
    [System.Serializable]
    public struct fixed2 
    {
        public long x;
        public long y;

        public static readonly fixed2 zero;
        public static readonly fixed2 one;
        public static readonly fixed2 half;

        static fixed2()
        {
            zero = new fixed2 (0, 0);
            one = new fixed2 (FixedMath.PRECISION, FixedMath.PRECISION);
            half = new fixed2 (FixedMath.PRECISION / 2, FixedMath.PRECISION / 2);
        }

        public fixed2(long x, long y)
        {
            this.x = x;
            this.y = y;
        }

        public fixed2(Vector2 v2)
        {
            this.x = UnityEngine.Mathf.RoundToInt(v2.x * FixedMath.PRECISION);
            this.y = UnityEngine.Mathf.RoundToInt(v2.y * FixedMath.PRECISION);
        }

        public long sqrMagnitude
        {
            get
            {
                return (x * x) + (y * y);
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
        }

        public static long Dot (fixed2 lhs, fixed2 rhs)
        {
            return FixedMath.Divide ((lhs.x * rhs.x) + (lhs.y * rhs.y), FixedMath.PRECISION);
        }

        public override bool Equals(object o)
        {
            if (o == null)
            {
                return false;
            }
            fixed2 num = (fixed2)o;
            return ((this.x == num.x) && (this.y == num.y));
        }

        public override int GetHashCode()
        {
            return (int)((this.x * 4616005L) ^ (this.y * 1209353L));
        }

        public static bool operator ==(fixed2 lhs, fixed2 rhs)
        {
            return ((lhs.x == rhs.x) && (lhs.y == rhs.y));
        }

        public static bool operator !=(fixed2 lhs, fixed2 rhs)
        {
            return ((lhs.x != rhs.x) || (lhs.y != rhs.y));
        }

        public static fixed2 operator -(fixed2 lhs, fixed2 rhs)
        {
            lhs.x -= rhs.x;
            lhs.y -= rhs.y;
            return lhs;
        }

        public static fixed2 operator -(fixed2 lhs)
        {
            lhs.x = -lhs.x;
            lhs.y = -lhs.y;
            return lhs;
        }

        public static fixed2 operator +(fixed2 lhs, fixed2 rhs)
        {
            lhs.x += rhs.x;
            lhs.y += rhs.y;
            return lhs;
        }

        public static fixed2 operator *(fixed2 lhs, long rhs)
        {
            lhs.x *= rhs;
            lhs.y *= rhs;
            return lhs;
        }

        public Vector2 ToVector2()
        {
            Vector2 value;
            value.x = (float)x / (float)FixedMath.PRECISION;
            value.y = (float)y / (float)FixedMath.PRECISION;
            return value;
        }

        public Vector3 ToVector3(float givenY)
        {
            Vector3 value;
            value.x = (float)x / (float)FixedMath.PRECISION;
            value.y = givenY;
            value.z = (float)y / (float)FixedMath.PRECISION;
            return value;
        }

        public static fixed2 Lerp(fixed2 fromValue, fixed2 toValue, factor lerpT)
        {
            fixed2 returnValue = fromValue;
            returnValue.x = lerpT.numerator * (toValue.x - fromValue.x) / lerpT.denominator + fromValue.x;
            returnValue.y = lerpT.numerator * (toValue.y - fromValue.y) / lerpT.denominator + fromValue.y;
            return returnValue;
        }
    }
}


