using OOPLab4._1.OOPLab4._1;
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

        public virtual void setColor(Color color)
        {
            this.color = color;
        }
        public abstract void myPaint(in Graphics g);
        public abstract bool intersects(MyVector coords);
        public abstract void move(MyVector direction);
        public abstract void changeScale(float speed, bool increase = true);
        public abstract void getRect(MyVector leftTop, MyVector rightBottom);
        public abstract void load(StreamReader reader, FigureFactory factory);
        public abstract void save(StreamWriter writer);
    }
}
