using System;
using System.Collections.Generic;
using System.Text;

namespace RSExample.Math
{
    class Rectangle
    {
        public Rectangle(Point origin, Point corner)
        {
            Origin = origin;
            Corner = corner;
        }

        public Point Origin { get; }
        public Point Corner { get; }

        public Rectangle GrowBy(float x, float y)
        {
            var ox = Origin.X - x;
            var oy = Origin.Y - y;
            var cx = Corner.X + x;
            var cy = Corner.Y + y;
            return new Rectangle(new Point(ox, oy), new Point(cx, cy));
        }

        public Rectangle ShrinkBy(float x, float y)
        {
            var ox = Origin.X + x;
            var oy = Origin.Y + y;
            var cx = Corner.X - x;
            var cy = Corner.Y - y;
            return new Rectangle(new Point(ox, oy), new Point(cx, cy));
        }

        public bool ContainsPoint(Point point)
        {
            var x = point.X;
            var y = point.Y;
            var x0 = Origin.X;
            var y0 = Origin.Y;
            var x1 = Corner.X;
            var y1 = Corner.Y;
            return (x0 < x) && (y0 < y)
                && (x1 > x) && (y1 > y);
        }
    }
}
