using System;
using System.Collections.Generic;
using System.Text;

namespace RSExample.Math
{
    class Angle
    {
        const float RADIANS_PER_DEGREE = MathF.PI / 180;

        public static float D2R(float deg) { return deg * RADIANS_PER_DEGREE; }

        public static float R2D(float rad) { return rad / RADIANS_PER_DEGREE; }

        public static float Normalize(float rad)
        {
            return Utils.Mod(rad, MathF.PI * 2);
        }

        public static float Radians(float rad)
        {
            return Normalize(rad);
        }

        public static float Degrees(float deg)
        {
            return Normalize(D2R(deg));
        }

        public static float Opposite(float rad)
        {
            return Normalize(rad + MathF.PI);
        }

        public static float DiffClockwise(float a, float b)
        {
            return Normalize(a - b);
        }

        public static float DiffCounterclockwise(float a, float b)
        {
            return Normalize(b - a);
        }

        public static float Diff(float a, float b)
        {
            return MathF.Min(DiffClockwise(a, b), DiffCounterclockwise(a, b));
        }
    }
}
