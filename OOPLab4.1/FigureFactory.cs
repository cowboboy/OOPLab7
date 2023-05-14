using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPLab4._1
{
    internal abstract class FigureFactory
    {
        public abstract Figure createFigure(string code);
    }
}
