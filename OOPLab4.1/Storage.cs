using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPLab4._1
{
    internal class Storage
    {
        Figure[] elements;
        public int size { get; set; }

        public Storage()
        {
            size = 0;
        }

        public void push_back(in Figure element)
        {
            Figure[] changedElements = new Figure[size + 1];
            int i = 0;
            for (; i < size; i++)
            {
                changedElements[i] = elements[i];
            }
            changedElements[i] = element;
            elements = changedElements;
            ++size;
        }

        public void push_front(in Figure element)
        {
            Figure[] changedElements = new Figure[size + 1];
            changedElements[0] = element;
            for (int i = 1; i < changedElements.Length; i++)
            {
                changedElements[i] = elements[i];
            }
            elements = changedElements;
            ++size;
        }

        public ref Figure pop(int index)
        {
            ref Figure result1 = ref elements[0];
            if (size == 1)
            {
                elements = null;
                --size;
                return ref result1;
            }
            Figure[] changedElements = new Figure[size - 1];
            for (int i = 0; i < index; ++i)
            {
                changedElements[i] = elements[i];
            }
            ref Figure result2 = ref elements[index];
            for (int i = index+1; i < size ; ++i)
            {
                changedElements[i-1] = elements[i];
            }
            elements = changedElements;
            --size;
            return ref result2;
        }

        public ref Figure getObject(int index)
        {
            return ref elements[index];
        }

        public void loadFigures(string fileName, FigureFactory factory)
        {
            if (File.Exists(fileName))
            {
                using (StreamReader reader = new StreamReader(fileName))
                {
                    Figure figure;
                    string? line;

                    while ((line = reader.ReadLine()) != null)
                    {
                        figure = factory.createFigure(Convert.ToString(line));
                        if (figure != null)
                        {
                            figure.load(reader);
                            push_back(figure);
                        }
                    }
                }
            }
        }

        public void saveFigures(string fileName)
        {
            if (!File.Exists(fileName))
            {
                File.Create(fileName).Close();
            }
            using (StreamWriter writer = new StreamWriter(fileName))
            {
                for (int i = 0; i < size; ++i)
                {
                    elements[i].save(writer);
                }
            }
        }
    }
}
