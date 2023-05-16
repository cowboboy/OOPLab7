using OOPLab4._1.OOPLab4._1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPLab4._1
{
    internal class CCircle : Figure
    {
        public int radius { get; set; }
        private bool active = false;

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
        public CCircle()
        {
            standartPen = new Pen(Color.Black, 5);
            selectedPen = new Pen(Color.Red, 5);
            active = false;
            radius = 50;
        }
        public CCircle(int x, int y, Color color, int radius = 50)
        {
            this.x = x;
            this.y = y;
            this.radius = radius;
            standartPen = new Pen(Color.Black, 5);
            selectedPen = new Pen(Color.Red, 5);
            active = false;
            setColor(color);
        }

        public override void myPaint(in Graphics g)
        {
            if (isActive)
            {
                g.DrawEllipse(selectedPen, x - radius, y - radius, radius * 2, radius * 2);
                g.FillEllipse(new SolidBrush(getColor()), new Rectangle(x - radius, y - radius, radius * 2, radius * 2));
            } else
            {
                g.DrawEllipse(standartPen, x - radius, y - radius, radius * 2, radius * 2);
                g.FillEllipse(new SolidBrush(getColor()), new Rectangle(x - radius, y - radius, radius * 2, radius * 2));
            }
        }

        public override bool intersects(MyVector coords)
        {
            if ((coords.X - x) * (coords.X - x) + (coords.Y - y) * (coords.Y - y) <= radius * radius)
            {
                return true;
            } else
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
            leftTop.X = x - radius;
            leftTop.Y = y - radius;
            rightBottom.X = x + radius;
            rightBottom.Y = y + radius;
        }

        public override void changeScale(float factor, bool increase)
        {
            if (increase)
            {
                radius = Convert.ToInt32(factor * radius);
            } else
            {
                radius = Convert.ToInt32(radius / factor);
            }
            //x = Convert.ToInt32(factor * x);
            //y = Convert.ToInt32(factor * y);
        }

        public override void load(StreamReader reader, FigureFactory factory)
        {
            x = Convert.ToInt32(reader.ReadLine());
            y = Convert.ToInt32(reader.ReadLine());
            radius = Convert.ToInt32(reader.ReadLine());
            setColor(Color.FromArgb(Convert.ToInt32(reader.ReadLine())));
        }

        public override void save(StreamWriter writer)
        {
            writer.WriteLine("Circle");
            writer.WriteLine(x.ToString());
            writer.WriteLine(y.ToString());
            writer.WriteLine(radius.ToString());
            writer.WriteLine(getColor().ToArgb());
        }
    }
}
