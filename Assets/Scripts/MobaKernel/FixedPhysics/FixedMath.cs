using UnityEngine;
using System.Collections;

namespace Medusa
{
    public class FixedMath 
    {
        public const long PRECISION = 10000L;
        public const long SQR_PRECISION = 100L;

        public static long Sqrt(long value)
        {
            ulong a = (ulong)value;
            ulong num = 0L;
            ulong num2 = 0L;
            for (int i = 0; i < 0x20; i++)
            {
                num2 = num2 << 1;
                num = num << 2;
                num += a >> 0x3e;
                a = a << 2;
                if (num2 < num)
                {
                    num2 += 1L;
                    num -= num2;
                    num2 += 1L;
                }
            }
            return (long)(((num2 >> 1) & 0xffffffffL));
        }

        public static long Divide(long a, long b)
        {
            long num = ((a ^ b) & -9223372036854775808L) >> 0x3f;
            long num2 = (num * -2L) + 1L;
            return ((a + ((b / 2L) * num2)) / b);
        }

        public static fixed3 Divide(fixed3 vector,factor factor)
        {
            fixed3 value = fixed3.zero;
            value.x = (vector.x * factor.numerator / factor.denominator);
            value.y = (vector.y * factor.numerator / factor.denominator);
            value.z = (vector.z * factor.numerator / factor.denominator);
            return value;
        }

        public static fixed2 Divide(fixed2 vector,factor factor)
        {
            fixed2 value = fixed2.zero;
            value.x = (vector.x * factor.numerator / factor.denominator);
            value.y = (vector.y * factor.numerator / factor.denominator);
            return value;
        }

        public static long Abs(long a)
        {
            return a > 0 ? a : -a;
        }

        public fixed2 Lerp(fixed2 fromValue, fixed2 toValue, factor lerpT)
        {
            lerpT = lerpT > factor.one ? factor.one : lerpT;
            lerpT = lerpT < factor.zero ? factor.zero : lerpT;
            return (toValue - fromValue) * lerpT + fromValue;
        }

        public fixed3 Lerp(fixed3 fromValue, fixed3 toValue, factor lerpT)
        {
            lerpT = lerpT > factor.one ? factor.one : lerpT;
            lerpT = lerpT < factor.zero ? factor.zero : lerpT;
            return (toValue - fromValue) * lerpT + fromValue;
        }
    }
}

