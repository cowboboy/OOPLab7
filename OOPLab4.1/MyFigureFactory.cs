using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPLab4._1
{
    internal class MyFigureFactory : FigureFactory
    {
        public override Figure createFigure(string code)
        {
            Figure figure = null;
            switch (code)
            {
                case "Circle":
                    figure = new CCircle();
                    break;
                case "Square":
                    figure = new Square();
                    break;
                case "Triangle":
                    figure = new Triangle();
                    break;
                case "Group":
                    figure = new Group();
                    break;
            }
            return figure;
        }
    }
}
