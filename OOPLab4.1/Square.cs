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

        public Square(int x, int y, Color color, int sideLength = 50)
        {
            this.x = x;
            this.y = y;
            this.sideLength = sideLength;
            standartPen = new Pen(Color.Black, 5);
            selectedPen = new Pen(Color.Red, 5);
            active = false;
            currentColor = color;
        }

        public override void myPaint(in Graphics g)
        {
            if (isActive)
            {
                g.DrawRectangle(selectedPen, x - sideLength / 2, y - sideLength / 2, sideLength, sideLength);
                g.FillRectangle(new SolidBrush(currentColor), new Rectangle(x - sideLength / 2, y - sideLength / 2, sideLength, sideLength));
            }
            else
            {
                g.DrawRectangle(standartPen, x - sideLength / 2, y - sideLength / 2, sideLength, sideLength);
                g.FillRectangle(new SolidBrush(currentColor), new Rectangle(x - sideLength / 2, y - sideLength / 2, sideLength, sideLength));
            }
        }

        public override bool intersects(Point coords)
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
            leftTop.X = x - sideLength / 2;
            leftTop.Y = y - sideLength / 2;
            rightBottom.X = x + sideLength / 2;
            rightBottom.Y = y + sideLength / 2;
        }

        public override void changeScale(float factor)
        {
            sideLength = Convert.ToInt32(factor * sideLength);
        }
    }
}
