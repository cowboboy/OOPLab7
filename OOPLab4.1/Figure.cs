using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPLab4._1
{
    internal abstract class Figure
    {
        public int x { get; set; }
        public int y { get; set; }
        public abstract bool isActive { get; set; }

        protected Color color;

        protected Pen selectedPen, standartPen;

        public Color getColor()
        {
            return color;
        }

        public virtual void setColor(string colorName)
        {
            switch (colorName)
            {
                case "Green":
                    color = Color.Green;
                    break;
                case "Yellow":
                    color = Color.Yellow;
                    break;
                case "Blue":
                    color = Color.Blue;
                    break;
                default:
                    color = Color.White;
                    break;
            }
        }
        public abstract void myPaint(in Graphics g);
        public abstract bool intersects(Point coords);
        public abstract void move(Point direction);
        public abstract void changeScale(float factor);
        public abstract void getRect(ref Point leftTop, ref Point rightBottom);
        public abstract void load(StreamReader reader);
        public abstract void save(StreamWriter writer);
    }
}
