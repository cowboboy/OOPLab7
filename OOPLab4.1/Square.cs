using OOPLab4._1.OOPLab4._1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPLab4._1
{
    internal class Square : Figure
    {
        public int sideLength { get; set; }
        private bool active;
        public override bool isActive
        {
            get
            {
                return active;
            }
            set
            {
                active = value;
            }
        }
        public Square()
        {
            standartPen = new Pen(Color.Black, 5);
            selectedPen = new Pen(Color.Red, 5);
            active = false;
        }

        public Square(int x, int y, Color color, int sideLength = 50)
        {
            this.x = x;
            this.y = y;
            this.sideLength = sideLength;
            standartPen = new Pen(Color.Black, 5);
            selectedPen = new Pen(Color.Red, 5);
            active = false;
            setColor(color);
        }

        public override void myPaint(in Graphics g)
        {
            if (isActive)
            {
                g.DrawRectangle(selectedPen, x - sideLength / 2, y - sideLength / 2, sideLength, sideLength);
                g.FillRectangle(new SolidBrush(getColor()), new Rectangle(x - sideLength / 2, y - sideLength / 2, sideLength, sideLength));
            }
            else
            {
                g.DrawRectangle(standartPen, x - sideLength / 2, y - sideLength / 2, sideLength, sideLength);
                g.FillRectangle(new SolidBrush(getColor()), new Rectangle(x - sideLength / 2, y - sideLength / 2, sideLength, sideLength));
            }
        }

        public override bool intersects(MyVector coords)
        {
            if (coords.X >= x - sideLength / 2 && coords.X <= x + sideLength / 2 && coords.Y >= y - sideLength / 2 && coords.Y <= y + sideLength / 2)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public override void move(MyVector direction)
        {
            x += direction.X;
            y += direction.Y;
        }

        public override void getRect(MyVector leftTop, MyVector rightBottom)
        {
            leftTop.X = x - sideLength / 2;
            leftTop.Y = y - sideLength / 2;
            rightBottom.X = x + sideLength / 2;
            rightBottom.Y = y + sideLength / 2;
        }

        public override void changeScale(float factor, bool increase)
        {
            if (increase)
            {
                sideLength = Convert.ToInt32(factor * sideLength);
            }
            else
            {
                sideLength = Convert.ToInt32(sideLength / factor);
            }
        }

        public override void load(StreamReader reader, FigureFactory factory)
        {
            x = Convert.ToInt32(reader.ReadLine());
            y = Convert.ToInt32(reader.ReadLine());
            sideLength = Convert.ToInt32(reader.ReadLine());
            setColor(Color.FromArgb(Convert.ToInt32(reader.ReadLine())));
        }

        public override void save(StreamWriter writer)
        {
            writer.WriteLine("Square");
            writer.WriteLine(x.ToString());
            writer.WriteLine(y.ToString());
            writer.WriteLine(sideLength.ToString());
            writer.WriteLine(getColor().ToArgb());
        }
    }
}
