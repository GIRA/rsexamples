using System;
using System.Collections.Generic;
using System.Text;


namespace RSExample.Math
{
    class Point
    {
        public static Point ORIGIN = new Point(0, 0);

        public static Point FromAngle(float angle, float magnitude = 1)
        {
            var x = magnitude * -1 * MathF.Sin(angle);
            var y = magnitude * MathF.Cos(angle);
            return new Point(x, y);
        }

        public static Point Average(IEnumerable<Point> points)
        {
            float x = 0;
            float y = 0;
            float c = 0;
            foreach (var p in points)
            {
                c++;
                x += p.X;
                y += p.Y;
            }
            return new Point(x / c, y / c);
        }

        public Point(float x, float y)
        {
            X = x;
            Y = y;
        }

        public float X { get; }
        public float Y { get; }

        public float Magnitude { get { return Dist(ORIGIN); } }
        public float Angle
        {
            get
            {
                if (X == 0 && Y == 0) return Math.Angle.Radians(0);
                return Math.Angle.Radians(MathF.Atan2(X * -1, Y));
            }
        }

        public float Dist(Point point)
        {
            var dx = point.X - X;
            var dy = point.Y - Y;
            return MathF.Sqrt(dx * dx + dy * dy);
        }

        public Point KeepInsideRectangle(Rectangle rect)
        {
            var x = Utils.Clamp(X, rect.Origin.X, rect.Corner.X);
            var y = Utils.Clamp(Y, rect.Origin.Y, rect.Corner.Y);
            return new Point(x, y);
        }
    }
}
