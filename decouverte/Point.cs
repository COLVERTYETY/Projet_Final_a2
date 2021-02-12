using System;

namespace decouverte
{

    public class Point
    {

        public double x;
        public double y;
        public double R
        {
            get
            {
                return Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2));
            }
        }
        public double Theta
        {
            get
            {
                return Math.Atan2(y, x);
            }
        }


        public Point(double _x, double _y)
        {
            x = _x;
            y = _y;
        }

        public static Point operator - (Point a, Point b) {
            return new Point(a.x - b.x, a.y - b.y);
        }

        public static Point operator + (Point a, Point b) {
            return new Point(a.x + b.x, a.y + b.y);
        }

        public static Point PolToCart(double r, double theta, double x = 0, double y  = 0) {
            return new Point(r * Math.Cos(theta) + x, r * Math.Sin(theta) + y);
        }
    }
}