using OOPLab4._1.OOPLab4._1;
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

        public void addFigure(Figure figure)
        {
            storage.push_back(figure);
            MyVector leftTop = new MyVector();
            MyVector rightBottom = new MyVector();
            getRect(leftTop, rightBottom);
            MyVector center = (leftTop + rightBottom) / 2;
            x = center.X;
            y = center.Y;
        }

        public Figure popFigure()
        {
            if (storage.size > 0)
            {
                return storage.pop(storage.size - 1);
            } else
            {
                return null;
            }
        }

        public override void myPaint(in Graphics g)
        {
            for (int i = 0; i < storage.size; ++i)
            {
                storage.getObject(i).myPaint(g);
            }
        }

        public override bool intersects(MyVector coords)
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

        public override void setColor(Color color)
        {
            for (int i = 0; i < storage.size; ++i)
            {
                storage.getObject(i).setColor(color);
            }
        }

        public override void move(MyVector direction)
        {
            for (int i = 0; i < storage.size; ++i)
            {
                storage.getObject(i).move(direction);
            }
        }

        public override void getRect(MyVector leftTop, MyVector rightBottom)
        {
            MyVector curLeftTop = new MyVector();
            MyVector curRightBottom = new MyVector();
            storage.getObject(0).getRect(leftTop, rightBottom);
            for (int i = 1; i < storage.size; ++i)
            {
                Figure curElem = storage.getObject(i);
                curElem.getRect(curLeftTop, curRightBottom);
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

        public override void changeScale(float factor, bool increase)
        {
            MyVector leftTop = new MyVector();
            MyVector rightBottom = new MyVector();
            getRect(leftTop, rightBottom);

            MyVector ray = new MyVector();
            MyVector factorRay = new MyVector();

            for (int i = 0; i < storage.size; ++i)
            {
                ray = new MyVector(storage.getObject(i).x, storage.getObject(i).y) - new MyVector(x, y);
                if (increase) {
                    factorRay = (new MyVector(storage.getObject(i).x, storage.getObject(i).y) - new MyVector(x, y)) * factor;
                } else
                {
                    factorRay = (new MyVector(storage.getObject(i).x, storage.getObject(i).y) - new MyVector(x, y)) / factor;
                }
                MyVector direction = factorRay - ray;
                storage.getObject(i).move(direction);
                storage.getObject(i).changeScale(factor, increase);
            }
        }

        public override void load(StreamReader reader, FigureFactory factory)
        {
            int len = Convert.ToInt32(reader.ReadLine());
            for (int i = 0; i < len; ++i)
            {
                Figure figure = factory.createFigure(reader.ReadLine());
                if (figure != null)
                {
                    figure.load(reader, factory);
                    addFigure(figure);
                }
            }
        }

        public override void save(StreamWriter writer)
        {
            writer.WriteLine("Group");
            writer.WriteLine(storage.size);
            for (int i = 0; i < storage.size; ++i)
            {
                storage.getObject(i).save(writer);
            }
        }
    }
}
