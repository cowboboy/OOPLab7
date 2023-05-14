namespace OOPLab4._1
{
    public partial class Form1 : Form
    {
        Storage storage; // Хранилище с нарисованными окружностями

        bool isCtrlActive = false;
        bool isCollisionActive = true;
        bool pressedCtrl = false;
        bool isMove = false;
        bool isScale = false;
        bool isBound = false;

        Point leftTopPaintBox;
        Point rightBottomPaintBox;

        enum Figures
        {
            Circle,
            Square,
            Triangle,
        }
        Figures currentFigure;

        Object[] colors = {Color.White, Color.Blue, Color.Green, Color.Yellow};
        Color currentColor;

        Point lastMouseCoords;

        public Form1()
        {
            InitializeComponent();
            storage = new Storage();
            setFigure.DataSource = Enum.GetValues(typeof(Figures));
            setFigure.SelectedItem = Figures.Circle;
            currentFigure = Figures.Circle;
            setColor.Items.AddRange(colors);
            setColor.SelectedItem = Color.White;
            currentColor = Color.White;
            leftTopPaintBox = new Point(0, 0);
            rightBottomPaintBox.X = PaintBox.Width;
            rightBottomPaintBox.Y = PaintBox.Height;
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
            label1.Text = Convert.ToString(storage.size);
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
            if (e.KeyCode == Keys.G)
            {
                isMove = true;
            }
            if (e.KeyCode == Keys.S)
            {
                isScale = true;
            }
            if (e.KeyCode == Keys.Back)
            {
                deleteActiveElements();

                // Перерисовка
                PaintBox.Refresh();
            }
            if (e.KeyCode == Keys.B)
            {
                bindFigures();
            }
            if (e.KeyCode == Keys.Up && isScale)
            {
                changeScale(1.1f);
            } else if (e.KeyCode == Keys.Down && isScale)
            {
                changeScale(0.9f);
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
                        group.addFigure(ref storage.pop(i));
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

        private void checkBoxCtrl_CheckedChanged(object sender, EventArgs e)
        {
            isCtrlActive = !isCtrlActive;
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.ControlKey)
            {
                pressedCtrl = false;
            }
            if (e.KeyCode == Keys.G)
            {
                isMove = false;
            }
            if (e.KeyCode == Keys.S)
            {
                isScale = false;
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

        private void setColor_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentColor = (Color)setColor.SelectedItem;
            for (int i = 0; i < storage.size; ++i)
            {
                if (storage.getObject(i).isActive)
                {
                    storage.getObject(i).changeColor((Color)setColor.SelectedItem);
                }
            }
            PaintBox.Refresh();
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
            Point leftTop = new Point();
            Point rightBottom = new Point();
            if (isMove)
            {
                int dX = e.Location.X - lastMouseCoords.X;
                int dY = e.Location.Y - lastMouseCoords.Y;
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
            lastMouseCoords = e.Location;
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
    }
}