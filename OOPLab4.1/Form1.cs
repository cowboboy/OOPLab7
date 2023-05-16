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

        Point lastMouseCoords;

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
                        if (e.Button == MouseButtons.Left && storage.getObject(i).intersects(e.Location) && isFirstLayer)
                        {
                            storage.getObject(i).isActive = true;
                            isFirstLayer = false;
                        }
                        // Не затираем активные элементы, так как работает Ctrl
                    } else
                    {
                        if (e.Button == MouseButtons.Left && storage.getObject(i).intersects(e.Location))
                        {
                            storage.getObject(i).isActive = true;
                        }
                    }
                }
                else
                {
                    if (isCollisionActive)
                    {
                        if (e.Button == MouseButtons.Left && storage.getObject(i).intersects(e.Location) && isFirstLayer)
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
                        if (e.Button == MouseButtons.Left && storage.getObject(i).intersects(e.Location))
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
                Point leftTop = new Point(), rightBottom = new Point();
                element.getRect(ref leftTop, ref rightBottom);
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
                case Keys.Up:
                    if (isScale)
                    {
                        changeScale(1.1f);
                    }
                    break;
                case Keys.Down:
                    if (isScale)
                    {
                        changeScale(0.9f);
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

        private void changeScale(float factor)
        {
            Point leftTop = new Point(), rightBottom = new Point();
            //getRect(ref leftTop, ref rightBottom);
            Point testRightBottom = new Point((int)(rightBottom.X * factor), (int)(rightBottom.Y * factor));
            if (isNotCollision(leftTop, testRightBottom) &&
                testRightBottom.X - leftTop.X > 50 && testRightBottom.Y - leftTop.Y > 50)
            {
                for (int i = 0; i < storage.size; ++i)
                {
                    Point direction = new Point();
                    //direction.X = testRightBottom.X - rightBottom.X;
                    //direction.Y = testRightBottom.Y - rightBottom.Y;
                    direction.X = (int)(factor * (storage.getObject(i).x - leftTop.X)) - (storage.getObject(i).x - leftTop.X);
                    direction.Y = (int)(factor * (storage.getObject(i).y - leftTop.Y)) - (storage.getObject(i).y - leftTop.Y);
                    if (storage.getObject(i).isActive)
                    {
                        storage.getObject(i).move(direction);
                        storage.getObject(i).changeScale(factor);
                    }
                }
            }
            PaintBox.Refresh();
        }

        private void move(Point mouseCoords)
        {
            Point leftTop = new Point();
            Point rightBottom = new Point();
            if (isMove)
            {
                int dX = mouseCoords.X - lastMouseCoords.X;
                int dY = mouseCoords.Y - lastMouseCoords.Y;
                for (int i = 0; i < storage.size; ++i)
                {
                    if (storage.getObject(i).isActive)
                    {
                        storage.getObject(i).getRect(ref leftTop, ref rightBottom);
                        leftTop.X += dX;
                        leftTop.Y += dY;
                        rightBottom.X += dX;
                        rightBottom.Y += dY;
                        if (isNotCollision(leftTop, rightBottom))
                        {
                            storage.getObject(i).move(new Point(dX, dY));
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

        private bool isNotCollision(in Point leftTop, in Point rightBottom)
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
            move(e.Location);
        }

        private void PaintBox_Resize(object sender, EventArgs e)
        {
            rightBottomPaintBox.X = PaintBox.Width;
            rightBottomPaintBox.Y = PaintBox.Height;
            for (int i = 0; i < storage.size; ++i)
            {
                Point leftTop = new Point(), rightBottom = new Point();
                storage.getObject(i).getRect(ref leftTop, ref rightBottom);
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