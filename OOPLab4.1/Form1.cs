using OOPLab4._1.OOPLab4._1;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace OOPLab4._1
{
    public partial class Form1 : Form
    {
        Storage storage; // Хранилище с нарисованными окружностями

        bool isCtrlActive = true;
        bool isCollisionActive = true;
        bool pressedCtrl = false;
        bool isMove = false;
        bool isScale = false;

        Point leftTopPaintBox;
        Point rightBottomPaintBox;

        MyFigureFactory factory;
        string currentFileName = "";

        enum Figures
        {
            Circle,
            Square,
            Triangle,
        }
        Figures currentFigure;

        Color currentColor;

        MyVector lastMouseCoords;

        public Form1()
        {
            InitializeComponent();
            storage = new Storage();
            setFigure.DataSource = Enum.GetValues(typeof(Figures));
            setFigure.SelectedItem = Figures.Circle;
            currentFigure = Figures.Circle;
            currentColor = Color.White;
            leftTopPaintBox = new Point(0, 0);
            rightBottomPaintBox.X = PaintBox.Width;
            rightBottomPaintBox.Y = PaintBox.Height;
            factory = new MyFigureFactory();
            PaintBox.Refresh();
        }

        private void PaintBox_MouseClick(object sender, MouseEventArgs e)
        {
            bool isFirstLayer = true;
            for (int i = storage.size - 1; i >= 0; --i)
            {
                // Нажатие мышки вместе с активным checkBoxCtrl
                if (isCtrlActive && pressedCtrl) 
                {
                    if (isCollisionActive)
                    {
                        // Делаем активными все выбранные левой кнопкой мыши элементы
                        if (e.Button == MouseButtons.Left && storage.getObject(i).intersects(new MyVector(e.Location)) && isFirstLayer)
                        {
                            storage.getObject(i).isActive = true;
                            isFirstLayer = false;
                        }
                        // Не затираем активные элементы, так как работает Ctrl
                    } else
                    {
                        if (e.Button == MouseButtons.Left && storage.getObject(i).intersects(new MyVector(e.Location)))
                        {
                            storage.getObject(i).isActive = true;
                        }
                    }
                }
                else
                {
                    if (isCollisionActive)
                    {
                        if (e.Button == MouseButtons.Left && storage.getObject(i).intersects(new MyVector(e.Location)) && isFirstLayer)
                        {
                            storage.getObject(i).isActive = true;
                            isFirstLayer = false;
                        }
                        else
                        {
                            storage.getObject(i).isActive = false;
                        }
                    }
                    else
                    {
                        if (e.Button == MouseButtons.Left && storage.getObject(i).intersects(new MyVector(e.Location)))
                        {
                            storage.getObject(i).isActive = true;
                        }
                        else
                        {
                            storage.getObject(i).isActive = false;
                        }
                    }
                }
            }

            // Добавление окружности на полотно
            if (e.Button == MouseButtons.Right) 
            {
                Figure element = null;
                switch (currentFigure)
                {
                    case Figures.Circle:
                        element = new CCircle(e.Location.X, e.Location.Y, currentColor);
                        break;
                    case Figures.Square:
                        element = new Square(e.Location.X, e.Location.Y, currentColor);
                        break;
                    case Figures.Triangle:
                        element = new Triangle(e.Location.X, e.Location.Y, currentColor);
                        break;
                }
                MyVector leftTop = new MyVector(), rightBottom = new MyVector();
                element.getRect(leftTop, rightBottom);
                if (isNotCollision(leftTop, rightBottom))
                {
                    storage.push_back(element);
                }
            }
            // Перерисовка
            PaintBox.Refresh();
        }

        private void PaintBox_Paint(object sender, PaintEventArgs e)
        {
            for (int i = 0; i < storage.size; ++i)
            {
                storage.getObject(i).myPaint(e.Graphics);
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control)
            {
                pressedCtrl = true;
            }
            switch (e.KeyCode)
            {
                case Keys.G:
                    isMove = true;
                    break;
                case Keys.S:
                    isScale = true;
                    break;
                case Keys.C:
                    setColor();
                    break;
                case Keys.Back:
                    deleteActiveElements();
                    PaintBox.Refresh();
                    break;
                case Keys.B:
                    bindFigures();
                    break;
                case Keys.U:
                    unBindFigures();
                    break;
                case Keys.Up:
                    if (isScale)
                    {
                        changeScale(1.1f, true);
                    }
                    break;
                case Keys.Down:
                    if (isScale)
                    {
                        changeScale(1.1f, false);
                    }
                    break;
            }
        }

        private void bindFigures()
        {
            if (sizeActive() > 1)
            {
                Group group = new Group();
                for (int i = 0; i < storage.size; ++i)
                {
                    if (storage.getObject(i).isActive)
                    {
                        group.addFigure(storage.pop(i));
                        --i;
                    }
                }
                storage.push_back(group);
            }
        }

        private void unBindFigures()
        {
            List<Figure> tempFigures = new List<Figure>();
            if (sizeActive() > 0)
            {
                for (int i = 0; i < storage.size; ++i)
                {
                    if (storage.getObject(i).isActive && storage.getObject(i) is Group)
                    {
                        Figure result;
                        do
                        {
                            result = (Figure)((Group)storage.getObject(i)).popFigure();
                            tempFigures.Add(result);
                        }
                        while (result != null);
                        storage.pop(i);
                        --i;

                    }
                }
            }
            foreach (var figure in tempFigures)
            {
                storage.push_back(figure);
            }
        }

        private void deleteActiveElements()
        {
            for (int i = 0; i < storage.size; i++)
            {
                if (storage.getObject(i).isActive)
                {
                    storage.pop(i);
                    --i;
                }
            }
        }

        private int sizeActive()
        {
            int result = 0;
            for (int i = 0; i < storage.size; ++i)
            {
                if (storage.getObject(i).isActive)
                {
                    ++result;
                }
            }
            return result;
        }

        private void setColor()
        {
            if (sizeActive() > 0) { 
                if (colorDialog1.ShowDialog() == DialogResult.Cancel)
                    return;
                // установка цвета формы
                for (int i = 0; i < storage.size; ++i)
                {
                    if (storage.getObject(i).isActive)
                    {
                        storage.getObject(i).setColor(colorDialog1.Color);
                    }
                }
                currentColor = colorDialog1.Color;
                PaintBox.Refresh();
            }
        }
        private void getRect(MyVector leftTop, MyVector rightBottom)
        {
            storage.getObject(0).getRect(leftTop, rightBottom);
            MyVector curLeftTop = new MyVector();
            MyVector curRightBottom = new MyVector();
            for (int i = 1; i < storage.size; ++i)
            {
                Figure curElem = storage.getObject(i);
                curElem.getRect(curLeftTop, curRightBottom);
                if (curElem.isActive)
                {
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
        }

        private void changeScale(float factor, bool increase = true)
        {
            MyVector leftTop = new MyVector();
            MyVector rightBottom = new MyVector();
            getRect(leftTop, rightBottom);
            MyVector center = (leftTop + rightBottom) / 2;

            MyVector ray = new MyVector(), factorRay = new MyVector();

            ray = leftTop - center;
            if (increase)
            {
                factorRay = (leftTop - center) * factor;
            } else
            {
                factorRay = (leftTop - center) / factor;
            }
            MyVector testLeftTop = leftTop + factorRay - ray;

            ray = rightBottom - center;
            if (increase)
            {
                factorRay = (rightBottom - center) * factor;
            } else
            {
                factorRay = (rightBottom - center) / factor;
            }
            MyVector testRightBottom = rightBottom + factorRay - ray;
            // testRightBottom.X - leftTop.X > 70 && testRightBottom.Y - leftTop.Y > 70

            if (isNotCollision(testLeftTop, testRightBottom))
            {
                for (int i = 0; i < storage.size; ++i)
                {
                    ray = new MyVector(storage.getObject(i).x, storage.getObject(i).y) - center;
                    if (increase)
                    {
                        factorRay = (new MyVector(storage.getObject(i).x, storage.getObject(i).y) - center) * factor;
                    } else
                    {
                        factorRay = (new MyVector(storage.getObject(i).x, storage.getObject(i).y) - center) / factor;
                    }
                    MyVector direction = factorRay - ray;
                    if (storage.getObject(i).isActive)
                    {
                        //storage.getObject(i).move(direction);
                        storage.getObject(i).changeScale(factor, increase);
                    }
                }
            }
            PaintBox.Refresh();
        }

        private void move(MyVector mouseCoords)
        {
            MyVector leftTop = new MyVector();
            MyVector rightBottom = new MyVector();
            if (isMove)
            {
                int dX = mouseCoords.X - lastMouseCoords.X;
                int dY = mouseCoords.Y - lastMouseCoords.Y;
                for (int i = 0; i < storage.size; ++i)
                {
                    if (storage.getObject(i).isActive)
                    {
                        storage.getObject(i).getRect(leftTop, rightBottom);
                        leftTop.X += dX;
                        leftTop.Y += dY;
                        rightBottom.X += dX;
                        rightBottom.Y += dY;
                        if (isNotCollision(leftTop, rightBottom))
                        {
                            storage.getObject(i).move(new MyVector(dX, dY));
                        }

                    }
                }
                PaintBox.Refresh();
            }
            lastMouseCoords = mouseCoords;
        }

        private void checkBoxCtrl_CheckedChanged(object sender, EventArgs e)
        {
            isCtrlActive = !isCtrlActive;
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.ControlKey:
                    pressedCtrl = false;
                    break;
                case Keys.G:
                    isMove = false;
                    break;
                case Keys.S:
                    isScale = false;
                    break;
            }
        }

        private void checkBoxCollision_CheckedChanged(object sender, EventArgs e)
        {
            isCollisionActive = !isCollisionActive;
        }

        private void setFigure_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentFigure = (Figures)setFigure.SelectedItem;
        }

        private bool isNotCollision(MyVector leftTop, MyVector rightBottom)
        {
            if (leftTop.X > leftTopPaintBox.X && leftTop.Y > leftTopPaintBox.Y &&
                rightBottom.X < rightBottomPaintBox.X && rightBottom.Y < rightBottomPaintBox.Y)
            {
                return true;
            }
            return false;
        }

        private void PaintBox_MouseMove(object sender, MouseEventArgs e)
        {
            move(new MyVector(e.Location));
        }

        private void PaintBox_Resize(object sender, EventArgs e)
        {
            rightBottomPaintBox.X = PaintBox.Width;
            rightBottomPaintBox.Y = PaintBox.Height;
            for (int i = 0; i < storage.size; ++i)
            {
                MyVector leftTop = new MyVector(), rightBottom = new MyVector();
                storage.getObject(i).getRect(leftTop, rightBottom);
                if (!isNotCollision(leftTop, rightBottom))
                {
                    storage.pop(i);
                }
            }
            PaintBox.Refresh();
        }

        private void Save()
        {
            if (storage.size > 0)
            {
                if (currentFileName == "")
                {
                    if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
                        return;
                    string filename = saveFileDialog1.FileName;
                    currentFileName = filename;
                }
                storage.saveFigures(currentFileName);
                MessageBox.Show("Файл сохранен");
                PaintBox.Refresh();
            }
        }

        private void Load()
        {
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            string filename = openFileDialog1.FileName;
            currentFileName = filename;
            storage.loadFigures(filename, factory);
            MessageBox.Show("Файл открыт");
            PaintBox.Refresh();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Save();
        }

        private void loadButton_Click(object sender, EventArgs e)
        {
            Load();
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            Save();
        }
    }
}