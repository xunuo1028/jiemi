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
    public struct factor 
    {
        public long numerator;
        public long denominator;

        public static readonly factor zero;
        public static readonly factor one;
        public static readonly factor pi;
        public static readonly factor twoPi;

        public factor(long numerator, long denominator)
        {
            this.numerator = numerator;
            this.denominator = denominator;
        }

        static factor()
        {
            zero = new factor(0L, 1L);
            one = new factor(1L, 1L);
            pi = new factor(31416L, FixedMath.PRECISION);
            pi.Reduce();
            twoPi = new factor(62832L, FixedMath.PRECISION);
            twoPi.Reduce();
        }

        public factor Reduce()
        {
            long temp = 1L;
            long a = numerator;
            long b = denominator;

            if (a < b)
            {
                b = numerator;
                a = denominator;
            }

            while (b != 0)
            {
                temp = a % b;
                a = b;
                b = temp;
            }
            this.numerator = numerator / a;
            this.denominator = denominator / a;
            return this;
        }

        public float ToFloat()
        {
            return (float)numerator / (float)denominator;
        }

        public override string ToString()
        {
            return ToFloat().ToString();
        }

        public override bool Equals(object o)
        {
            if (o == null)
            {
                return false;
            }
            factor num = (factor)o;
            return (this.numerator * num.denominator == this.denominator * num.numerator);
        }

        public override int GetHashCode()
        {
            this.Reduce();
            return (int)(this.numerator * 100000000L + this.denominator);
        }

        public static bool operator ==(factor lhs, factor rhs)
        {
            return (lhs.numerator * rhs.denominator == lhs.denominator * rhs.numerator);
        }

        public static bool operator !=(factor lhs, factor rhs)
        {
            return (lhs.numerator * rhs.denominator != lhs.denominator * rhs.numerator);
        }

        public static bool operator <(factor a, factor b)
        {
            long num = a.numerator * b.denominator;
            long num2 = b.numerator * a.denominator;
            return (!((b.denominator > 0L) ^ (a.denominator > 0L)) ? (num < num2) : (num > num2));
        }

        public static bool operator >(factor a, factor b)
        {
            long num = a.numerator * b.denominator;
            long num2 = b.numerator * a.denominator;
            return (!((b.denominator > 0L) ^ (a.denominator > 0L)) ? (num > num2) : (num < num2));
        }

        public static bool operator <=(factor a, factor b)
        {
            long num = a.numerator * b.denominator;
            long num2 = b.numerator * a.denominator;
            return (!((b.denominator > 0L) ^ (a.denominator > 0L)) ? (num <= num2) : (num >= num2));
        }

        public static bool operator >=(factor a, factor b)
        {
            long num = a.numerator * b.denominator;
            long num2 = b.numerator * a.denominator;
            return (!((b.denominator > 0L) ^ (a.denominator > 0L)) ? (num >= num2) : (num <= num2));
        }

        public static bool operator <(factor a, long b)
        {
            long nom = a.numerator;
            long num2 = b * a.denominator;
            return ((a.denominator <= 0L) ? (nom > num2) : (nom < num2));
        }

        public static bool operator >(factor a, long b)
        {
            long nom = a.numerator;
            long num2 = b * a.denominator;
            return ((a.denominator <= 0L) ? (nom < num2) : (nom > num2));
        }

        public static bool operator <=(factor a, long b)
        {
            long nom = a.numerator;
            long num2 = b * a.denominator;
            return ((a.denominator <= 0L) ? (nom >= num2) : (nom <= num2));
        }

        public static bool operator >=(factor a, long b)
        {
            long nom = a.numerator;
            long num2 = b * a.denominator;
            return ((a.denominator <= 0L) ? (nom <= num2) : (nom >= num2));
        }

        public static bool operator ==(factor a, long b)
        {
            return (a.numerator == (b * a.denominator));
        }

        public static bool operator !=(factor a, long b)
        {
            return (a.numerator != (b * a.denominator));
        }

        public static factor operator +(factor a, factor b)
        {
            return new factor { numerator = (a.numerator * b.denominator) + (b.numerator * a.denominator), denominator = a.denominator * b.denominator };
        }

        public static factor operator +(factor a, long b)
        {
            a.numerator += b * a.denominator;
            return a;
        }

        public static factor operator -(factor a, factor b)
        {
            return new factor { numerator = (a.numerator * b.denominator) - (b.numerator * a.denominator), denominator = a.denominator * b.denominator };
        }

        public static factor operator -(factor a, long b)
        {
            a.numerator -= b * a.denominator;
            return a;
        }

        public static factor operator *(factor a, long b)
        {
            a.numerator *= b;
            return a;
        }

        public static factor operator /(factor a, long b)
        {
            a.denominator *= b;
            return a;
        }

        public static fixed3 operator *(fixed3 v, factor f)
        {
            return FixedMath.Divide(v, f);
        }

        public static fixed2 operator *(fixed2 v, factor f)
        {
            return FixedMath.Divide(v, f);
        }

        public static fixed3 operator /(fixed3 v, factor f)
        {
            return FixedMath.Divide(v, f);
        }

        public static fixed2 operator /(fixed2 v, factor f)
        {
            return FixedMath.Divide(v, f);
        }

        public static int operator *(int i, factor f)
        {
            return (int) FixedMath.Divide((long) (i * f.numerator), f.denominator);
        }

        public static factor operator -(factor a)
        {
            a.numerator = -a.numerator;
            return a;
        }
    } 
}

