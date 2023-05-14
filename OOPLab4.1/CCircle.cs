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

        public CCircle(int x, int y, Color color, int radius = 50)
        {
            this.x = x;
            this.y = y;
            this.radius = radius;
            standartPen = new Pen(Color.Black, 5);
            selectedPen = new Pen(Color.Red, 5);
            active = false;
            currentColor = color;
        }

        public override void myPaint(in Graphics g)
        {
            if (isActive)
            {
                g.DrawEllipse(selectedPen, x - radius, y - radius, radius * 2, radius * 2);
                g.FillEllipse(new SolidBrush(currentColor), new Rectangle(x - radius, y - radius, radius * 2, radius * 2));
            } else
            {
                g.DrawEllipse(standartPen, x - radius, y - radius, radius * 2, radius * 2);
                g.FillEllipse(new SolidBrush(currentColor), new Rectangle(x - radius, y - radius, radius * 2, radius * 2));
            }
        }

        public override bool intersects(Point coords)
        {
            if ((coords.X - x) * (coords.X - x) + (coords.Y - y) * (coords.Y - y) <= radius * radius)
            {
                return true;
            } else
            {
                return false;
            }
        }

        public override void changeColor(Color newColor)
        {
            currentColor = newColor;
        }

        public override void move(Point direction)
        {
            x += direction.X;
            y += direction.Y;
        }

        public override void getRect(ref Point leftTop, ref Point rightBottom)
        {
            leftTop.X = x - radius;
            leftTop.Y = y - radius;
            rightBottom.X = x + radius;
            rightBottom.Y = y + radius;
        }

        public override void changeScale(float factor)
        {
            radius = Convert.ToInt32(factor * radius);
            //x = Convert.ToInt32(factor * x);
            //y = Convert.ToInt32(factor * y);
        }
    }
}
