using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPLab4._1
{
    internal class Group : Figure
    {
        private Storage storage;
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
                for (int i = 0; i < storage.size; ++i)
                {
                    storage.getObject(i).isActive = value;
                }
            } 
        }

        public Group()
        {
            storage = new Storage();
        }

        public void addFigure(ref Figure figure)
        {
            storage.push_back(figure);
        }

        public override void myPaint(in Graphics g)
        {
            for (int i = 0; i < storage.size; ++i)
            {
                storage.getObject(i).myPaint(g);
            }
        }

        public override bool intersects(Point coords)
        {
            for (int i = 0; i < storage.size; ++i)
            {
                if (storage.getObject(i).intersects(coords))
                {
                    return true;
                }
            }
            return false;
        }

        public override void changeColor(Color newColor)
        {
            for (int i = 0; i < storage.size; ++i)
            {
                storage.getObject(i).currentColor = newColor;
            }
        }

        public override void move(Point direction)
        {
            for (int i = 0; i < storage.size; ++i)
            {
                storage.getObject(i).move(direction);
            }
        }

        public override void getRect(ref Point leftTop, ref Point rightBottom)
        {
            Point curLeftTop = new Point();
            Point curRightBottom = new Point();
            storage.getObject(0).getRect(ref leftTop, ref rightBottom);
            for (int i = 1; i < storage.size; ++i)
            {
                Figure curElem = storage.getObject(i);
                curElem.getRect(ref curLeftTop, ref curRightBottom);
                if (curLeftTop.X < leftTop.X)
                {
                    leftTop.X = curLeftTop.X;
                }
                if (curLeftTop.Y < leftTop.Y)
                {
                    leftTop.Y = curLeftTop.Y;
                }
                if (curRightBottom.X > rightBottom.X)
                {
                    rightBottom.X = curRightBottom.X;
                }
                if (curRightBottom.Y > rightBottom.Y)
                {
                    rightBottom.Y = curRightBottom.Y;
                }
            }
        }

        public override void changeScale(float factor)
        {
            for (int i = 0; i < storage.size; ++i)
            {
                storage.getObject(i).changeScale(factor);
            }
        }
    }
}
