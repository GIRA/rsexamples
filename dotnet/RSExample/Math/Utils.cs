using System;
using System.Collections.Generic;
using System.Text;

namespace RSExample.Math
{
    class Utils
    {
        public static float Mod(float n, float d)
        {
            return ((n % d) + d) % d;
        }

        public static float Clamp(float n, float min, float max)
        {
            return MathF.Max(min, MathF.Min(max, n));
        }
    }
}
