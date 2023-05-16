using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPLab4._1
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    namespace OOPLab4._1
    {
        internal class MyVector
        {
            public int X { get; set; }
            public int Y { get; set; }
            public MyVector()
            {
                X = 0;
                Y = 0;
            }
            public MyVector(int x, int y)
            {
                X = x;
                Y = y;
            }

            public MyVector(MyVector vector)
            {
                X = vector.X;
                Y = vector.Y;
            }

            public MyVector(Point point)
            {
                X = point.X;
                Y = point.Y;
            }

            public void changeCoords(in Point point)
            {
                X = point.X;
                Y = point.Y;
            }

            public void changeCoords(in Size size)
            {
                X = size.Width;
                Y = size.Height;
            }

            public double getLenght()
            {
                return Math.Sqrt(X * X + Y * Y);
            }

            public static MyVector operator +(MyVector a, MyVector b)
            {
                return new MyVector(a.X + b.X, a.Y + b.Y);
            }

            public static MyVector operator +(MyVector a, int b)
            {
                return new MyVector(a.X + b, a.Y + b);
            }

            public static MyVector operator -(MyVector a, MyVector b)
            {
                return new MyVector(a.X - b.X, a.Y - b.Y);
            }

            public static MyVector operator *(MyVector a, float b)
            {
                return new MyVector(Convert.ToInt32(a.X * b), Convert.ToInt32(a.Y * b));
            }

            public static MyVector operator /(MyVector a, float b)
            {
                return new MyVector(Convert.ToInt32(a.X / b), Convert.ToInt32(a.Y / b));
            }
        }
    }
}
