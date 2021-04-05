using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Interface_1._0
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        #region shapes points

        Point startRectangleNW = new Point();
        Point startRectangleSW = new Point();
        Point startRectangleSE = new Point();
        Point startRectangleNE = new Point();

        Point startParabellumNW = new Point();
        Point startParabellumSW = new Point();
        Point startParabellumSE = new Point();
        Point startParabellumNE = new Point();

        Point startRhombN = new Point();
        Point startRhombW = new Point();
        Point strtRhombS = new Point();
        Point startRhombE = new Point();

        Point startCycleNW = new Point();
        Point startCycleW = new Point();
        Point startCycleSW = new Point();
        Point startCycleSE = new Point();
        Point startCycleE = new Point();
        Point startCycleNE = new Point();
        #endregion

        public MainWindow()
        {
            InitializeComponent();

            #region initialized shapes start points

            #region rectangle points
            startRectangleNW.X = 8;
            startRectangleNW.Y = 1;

            startRectangleSW.X = 8;
            startRectangleSW.Y = 30;

            startRectangleSE.X = 60;
            startRectangleSE.Y = 30;

            startRectangleNE.X = 60;
            startRectangleNE.Y = 1;
            #endregion

            #region parrabellum points
            startParabellumNW.X = 8;
            startParabellumNW.Y = 1;

            startParabellumSW.X = 0;
            startParabellumSW.Y = 30;

            startParabellumSE.X = 60;
            startParabellumSE.Y = 30;

            startParabellumNE.X = 68;
            startParabellumNE.Y = 1;
            #endregion

            #region rhomb points
            startRhombW.X = 0;
            startRhombW.Y = 8;

            strtRhombS.X = 40;
            strtRhombS.Y = 20;

            startRhombE.X = 80;
            startRhombE.Y = 8;

            startRhombN.X = 40;
            startRhombN.Y = -4;
            #endregion

            #region cycle points

            startCycleW.X = -1;
            startCycleW.Y = 15;

            startCycleSW.X = 8;
            startCycleSW.Y = 30;

            startCycleSE.X = 60;
            startCycleSE.Y = 30;

            startCycleE.X = 69;
            startCycleE.Y = 15;

            startCycleNE.X = 60;
            startCycleNE.Y = 1;

            startCycleNW.X = 8;
            startCycleNW.Y = 1;
            #endregion

            #endregion
        }

        #region create Class-container

        private Diagramm diagramm = new Diagramm();
        private Diagramm OldDiagramm = new Diagramm();
        private int shapesCounter = 0;
        //метод для извелчения индекса из имени объекта
        private int GetIndexOfShape(Shapes shape, string name)
        {
            int index = 0;
            string temp = "";
            if( shape == Shapes.Rekt)
            {
                if (name.Length == 6)
                {
                    temp = name[5].ToString();
                    index = Convert.ToInt32(temp);
                }
                if (name.Length == 7)
                {
                    temp = name[5].ToString() + name[6].ToString();
                    index = Convert.ToInt32(temp);
                }
                if (name.Length == 8)
                {
                    temp = name[5].ToString() + name[6].ToString() + name[7].ToString();
                    index = Convert.ToInt32(temp);
                }
            }

            if (shape == Shapes.Parrabellum)
            {
                if (name.Length == 13)
                {
                    temp = name[12].ToString();
                    index = Convert.ToInt32(temp);
                }
                if (name.Length == 14)
                {
                    temp = name[12].ToString() + name[13].ToString();
                    index = Convert.ToInt32(temp);
                }
                if (name.Length == 15)
                {
                    temp = name[12].ToString() + name[13].ToString() + name[14].ToString();
                    index = Convert.ToInt32(temp);
                }
            }

            if ((shape == Shapes.Rhomb)||(shape == Shapes.Cycle))
            {
                if (name.Length == 7)
                {
                    temp = name[6].ToString();
                    index = Convert.ToInt32(temp);
                }
                if (name.Length == 8)
                {
                    temp = name[6].ToString() + name[7].ToString();
                    index = Convert.ToInt32(temp);
                }
                if (name.Length == 9)
                {
                    temp = name[6].ToString() + name[7].ToString() + name[8].ToString();
                    index = Convert.ToInt32(temp);
                }
            }

            if (shape == Shapes.Ellipse)
            {
                if (name.Length == 9)
                {
                    temp = name[8].ToString();
                    index = Convert.ToInt32(temp);
                }
                if (name.Length == 10)
                {
                    temp = name[8].ToString() + name[9].ToString();
                    index = Convert.ToInt32(temp);
                }
                if (name.Length == 11)
                {
                    temp = name[8].ToString() + name[9].ToString() + name[10].ToString();
                    index = Convert.ToInt32(temp);
                }
            }
            return index;
        }
        #endregion

        #region DragMainWindow
        private void Mouse_Drag_Window(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
        #endregion

        #region Interactions with Header

        #region right_side
        private void Mouse_Enter(object sender, RoutedEventArgs e)
        {
            ((Label)sender).Background = new SolidColorBrush(Colors.Gray) { Opacity = 0.2 };
        }
        private void Mouse_Leave(object sender, RoutedEventArgs e)
        {
            ((Label)sender).Background = new SolidColorBrush(Colors.Gray) { Opacity = 0 };
        }
        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void Mouse_Enter_Close(object sender, RoutedEventArgs e)
        {
            ((Label)sender).Background = Brushes.Red;
        }
        private void Mouse_Leave_Close(object sender, RoutedEventArgs e)
        {
            lbl_Close.Background = new SolidColorBrush(Colors.Gray) { Opacity = 0 };
        }
        #endregion

        #region left_side
        private void Help_Window(object sender, RoutedEventArgs e) { }
        private void Header_Mouse_Enter(object sender, RoutedEventArgs e)
        {
            ((Label)sender).Foreground = Brushes.White;
        }
        private void Header_Mouse_Leave(object sender, RoutedEventArgs e)
        {
            ((Label)sender).Foreground = Brushes.Gray;
        }
        #endregion

        #endregion

        #region Interactions with SideBar
        private void SideBar_Mouse_Enter(object sender, RoutedEventArgs e)
        {
            if (lbl_Ellipse == sender)
                Ellipse.Stroke = Brushes.White;
            if (lbl_Cylce == sender)
                Cycle.Stroke = Brushes.White;
            if (lbl_Parrabellum == sender)
                Parrabellum.Stroke = Brushes.White;
            if (lbl_Rhomb == sender)
                Rhomb.Stroke = Brushes.White;
            if (lbl_Rect == sender)
                Rekt.Stroke = Brushes.White;
        }
        private void SideBar_Mouse_Leave(object sender, RoutedEventArgs e)
        {
            if (lbl_Ellipse == sender)
                Ellipse.Stroke = Brushes.Gray;
            if (lbl_Cylce == sender)
                Cycle.Stroke = Brushes.Gray;
            if (lbl_Parrabellum == sender)
                Parrabellum.Stroke = Brushes.Gray;
            if (lbl_Rhomb == sender)
                Rhomb.Stroke = Brushes.Gray;
            if (lbl_Rect == sender)
                Rekt.Stroke = Brushes.Gray;
        }
        #endregion

        #region Show Window with Settings
        private void Change_Window(object sender, RoutedEventArgs e)
        {
            //Window1 w1 = new Window1();
            //w1.Show();
        }
        private void HelpWindowOpen(object sender, RoutedEventArgs e)
        {
            //HelpWindow hw = new HelpWindow();
            //hw.Show();
        }
        #endregion

        #region ResizeMainWindow
        private void Resize_Wondow(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Maximized)
                this.WindowState = WindowState.Normal;
            else
                this.WindowState = WindowState.Maximized;
        }
        private void Roll_Up(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
        #endregion

        #region Drag_N_Drop
        //Булевые переменные для определения фигуры
        private bool Rectangle_Check = false;
        private bool Parrabullem_Check = false;
        private bool Rhomb_Check = false;
        private bool Cycle_Check = false;
        private bool Ellipse_Check = false;
        //Определение отправителя объекта №1
        private void DragDrop_MD(object sender, MouseButtonEventArgs e)
        {
            if (Rekt == sender)
                Rectangle_Check = true;
            if (Rhomb == sender)
                Rhomb_Check = true;
            if (Parrabellum == sender)
                Parrabullem_Check = true;
            if (Cycle == sender)
                Cycle_Check = true;
            if (Ellipse == sender)
                Ellipse_Check = true;
            //Запоминаем нажатую точку мыши в фигуре
            var smt = (UIElement)sender;
            lastPoint = e.GetPosition(smt);
            //Запуск DragandDrop
            DragDrop.DoDragDrop(this, this, DragDropEffects.Copy);
        }

        #region Interaction_With_Shapes_Into_Canvas

        Polyline anchor_size;

        Point lastPoint;
        Point lastPoint_anchor;
        Point current_anchor_postion;

        bool is_anchor_create = false;
        //Добавление и обработка логики фигур
        private void RectangleAdd(Polygon polygon, TextBox txt, Point rectangleNW, Point rectangleSE, Point rectangleSW, Point rectnagleNE)
        {
            //Считывание данных о фигуре
            Point LeftTop = new Point()
            {
                X = 0,
                Y = 0
            };
            
            LeftTop.X = Canvas.GetLeft(polygon);
            LeftTop.Y = Canvas.GetTop(polygon);
            diagramm.blocks.Add(new Block(Shapes.Rekt, LeftTop, rectangleNW, rectnagleNE, rectangleSW, rectangleSE, shapesCounter - 1, txt.Text));
            diagramm.ShapesCounter++;
            //Сохранение текста внутри фигуры
            int index = GetIndexOfShape(Shapes.Rekt, polygon.Name);
            txt.MouseLeave += Txt_MouseLeave;
            void Txt_MouseLeave(object sender, MouseEventArgs e)
            {
                Keyboard.ClearFocus();
                diagramm.blocks[index].TextIntoTextBox = txt.Text;
            }

            polygon.MouseDown += IntoCanvasDownPolylineRectangle;



            void IntoCanvasDownPolylineRectangle(object sender, MouseButtonEventArgs e)
            {
                var smt = (Polygon)sender;
                lastPoint = e.GetPosition(smt);

                #region delete shape
                if (e.RightButton == MouseButtonState.Pressed)
                {
                    CanvasPos.Children.Remove(smt);
                    CanvasPos.Children.Remove(anchor_size);
                    CanvasPos.Children.Remove(txt);
                    bool isCanBeLower = false;
                    bool indexNotFound = true;
                    int indexForDeleting = GetIndexOfShape(Shapes.Rekt, polygon.Name);
                    shapesCounter--;
                    
                    foreach (Block block in diagramm.blocks)
                    {
                        if ((block.IndexNumber == indexForDeleting)&&(indexNotFound))
                        {
                            isCanBeLower = true;
                            indexNotFound = false;
                        }
                        if (isCanBeLower)
                        {
                            block.IndexNumber--;
                        }
                        
                    }
                    if (diagramm.blocks.Count > 1)
                        diagramm.blocks.RemoveAt(indexForDeleting);
                    else
                        diagramm.blocks.RemoveAt(0);


                }
                #endregion

                int point_summ_main_X = (int)(startRectangleNE.X + startRectangleSE.X + startRectangleSW.X + startRectangleNW.X);
                int point_summ_main_Y = (int)(startRectangleNE.Y + startRectangleSE.Y + startRectangleSW.Y + startRectangleNW.Y);

                int point_summ_second_X = 0;
                int point_summ_second_Y = 0;

                #region anchor
                if (!is_anchor_create)
                {

                    anchor_size = new Polyline();
                    anchor_size.Points = Anchor.Points;
                    anchor_size.Stroke = Brushes.Red;
                    anchor_size.Fill = Brushes.Transparent;

                    anchor_size.MouseDown += AnchorMouseDown;
                    anchor_size.MouseMove += AnchorMouseMove;
                    anchor_size.MouseUp += AnchorMouseUp;

                    #region anchor_action

                    void AnchorMouseDown(object sndr, MouseButtonEventArgs evnt)
                    {
                        var anchor = (UIElement)sndr;
                        //Позиция мыши в анкоре
                        lastPoint_anchor = evnt.GetPosition(anchor);
                        //Позиция анкора в канвасе = позиция мыши в канвасе
                        current_anchor_postion = evnt.GetPosition(CanvasPos);
                    }
                    void AnchorMouseMove(object sndr, MouseEventArgs evnt)
                    {
                        if (evnt.LeftButton == MouseButtonState.Pressed)
                        {

                            var anchor = (Polyline)sndr;
                            anchor.CaptureMouse();
                            anchor.Stroke = Brushes.Transparent;
                            Cursor = Cursors.SizeNWSE;

                            var pos = e.GetPosition(CanvasPos) - lastPoint_anchor;

                            var text_position_x = Math.Abs(Math.Sqrt((Math.Pow(rectangleNW.X, 2) + Math.Pow(rectangleNW.Y, 2))) - (Math.Sqrt(Math.Pow(rectnagleNE.X, 2) + Math.Pow(rectnagleNE.Y, 2)))) / 2;
                            var text_position_y = Math.Abs(Math.Sqrt((Math.Pow(rectangleNW.X, 2) + Math.Pow(rectangleNW.Y, 2))) - (Math.Sqrt(Math.Pow(rectangleSW.X, 2) + Math.Pow(rectangleSW.Y, 2)))) / 2;
                            //Если фигуру увеличивают
                            if (pos.X < current_anchor_postion.X && pos.Y < current_anchor_postion.Y)
                            {
                                //txt.MaxHeight += .3;
                                //txt.MaxWidth += .7;

                                //++fisrt.X;
                                //++fisrt.Y;

                                //++second.X;
                                rectangleSW.Y += Math.Abs(pos.Y - current_anchor_postion.Y) / 4;

                                rectangleSE.X += Math.Abs(pos.X - current_anchor_postion.X) / 2;
                                rectangleSE.Y += Math.Abs(pos.Y - current_anchor_postion.Y) / 4;

                                rectnagleNE.X += Math.Abs(pos.X - current_anchor_postion.X) / 2;
                                //++fourth.Y;

                                PointCollection points = new PointCollection();
                                points.Add(rectangleNW);
                                points.Add(rectangleSW);
                                points.Add(rectangleSE);
                                points.Add(rectnagleNE);

                                smt.Points = points;
                                current_anchor_postion.X = Canvas.GetLeft(polygon);
                                current_anchor_postion.Y = Canvas.GetTop(polygon);

                                Canvas.SetLeft(polygon, Canvas.GetLeft(polygon) - Math.Abs(pos.X - current_anchor_postion.X) / 2);
                                Canvas.SetTop(polygon, Canvas.GetTop(polygon) - Math.Abs(pos.Y - current_anchor_postion.Y) / 4);

                                Canvas.SetLeft(txt, Canvas.GetLeft(polygon) + text_position_x - 10);
                                Canvas.SetTop(txt, Canvas.GetTop(polygon) + text_position_y - 5);

                                point_summ_second_X = (int)(rectangleNW.X + rectangleSE.X + rectangleSW.X + rectnagleNE.X);
                                point_summ_second_Y = (int)(rectangleNW.Y + rectangleSE.Y + rectangleSW.Y + rectnagleNE.Y);

                                
                                polygon.MouseDown += IntoCanvasDownPolylineRectangle;
                            }
                            else if (point_summ_second_X >= point_summ_main_X && point_summ_second_Y >= point_summ_main_Y)
                            {
                                //txt.MaxHeight -= .3;
                                //txt.MaxWidth -= .7;

                                //++fisrt.X;
                                //++fisrt.Y;

                                //++second.X;
                                rectangleSW.Y -= Math.Abs(pos.Y - current_anchor_postion.Y) / 4;

                                rectangleSE.X -= Math.Abs(pos.Y - current_anchor_postion.Y) / 2;
                                rectangleSE.Y -= Math.Abs(pos.Y - current_anchor_postion.Y) / 4;

                                rectnagleNE.X -= Math.Abs(pos.Y - current_anchor_postion.Y) / 2;
                                //++fourth.Y;

                                PointCollection points = new PointCollection();
                                points.Add(rectangleNW);
                                points.Add(rectangleSW);
                                points.Add(rectangleSE);
                                points.Add(rectnagleNE);

                                smt.Points = points;

                                current_anchor_postion.X = Canvas.GetLeft(polygon);
                                current_anchor_postion.Y = Canvas.GetTop(polygon);

                                Canvas.SetLeft(polygon, Canvas.GetLeft(polygon) + Math.Abs(pos.Y - current_anchor_postion.Y) / 2);
                                Canvas.SetTop(polygon, Canvas.GetTop(polygon) + Math.Abs(pos.Y - current_anchor_postion.Y) / 4);

                                Canvas.SetLeft(txt, Canvas.GetLeft(polygon) + text_position_x - 10);
                                Canvas.SetTop(txt, Canvas.GetTop(polygon) + text_position_y - 5);

                                point_summ_second_X = (int)(rectangleNW.X + rectangleSE.X + rectangleSW.X + rectnagleNE.X);
                                point_summ_second_Y = (int)(rectangleNW.Y + rectangleSE.Y + rectangleSW.Y + rectnagleNE.Y);
                            }
                            //Повторно считываем данных о фигуре
                            LeftTop.X = Canvas.GetLeft(polygon);
                            LeftTop.Y = Canvas.GetTop(polygon);
                            int indexOfShape = GetIndexOfShape(Shapes.Rekt, polygon.Name);
                            foreach (Block block in diagramm.blocks)
                            {
                                if (block.IndexNumber == indexOfShape)
                                {
                                    diagramm.blocks.Insert(block.IndexNumber, new Block(Shapes.Rekt, LeftTop, rectangleNW, rectnagleNE, rectangleSW, rectangleSE, indexOfShape, txt.Text));
                                    diagramm.blocks.Remove(block);
                                    return;
                                }
                            }

                            Canvas.SetLeft(anchor, pos.X);
                            Canvas.SetTop(anchor, pos.Y);
                        }
                    }
                    void AnchorMouseUp(object sndr, MouseButtonEventArgs evnt)
                    {
                        var anchor = (Polyline)sndr;
                        anchor.ReleaseMouseCapture();
                        anchor.Stroke = Brushes.Red;
                        Cursor = Cursors.Arrow;
                        Canvas.SetLeft(anchor, Canvas.GetLeft(polygon));
                        Canvas.SetTop(anchor, Canvas.GetTop(polygon));
                    }

                    #endregion

                    CanvasPos.Children.Add(anchor_size);

                    Canvas.SetLeft(anchor_size, Canvas.GetLeft(smt));
                    Canvas.SetTop(anchor_size, Canvas.GetTop(smt));

                    is_anchor_create = true;
                }
                #endregion

                RectangleIntoCanvasMouseMove(polygon, txt, rectangleNW, rectangleSE, rectangleSW, rectnagleNE);
            }
        }

        

        private void RectangleIntoCanvasMouseMove(Polygon polygon, TextBox txt, Point rectangleNW, Point rectangleSE, Point rectangleSW, Point rectnagleNE)
        {
            polygon.MouseMove += IntoCanvasMove;

            void IntoCanvasMove(object sender, MouseEventArgs e)
            {
                if (e.LeftButton == MouseButtonState.Pressed)
                {
                    if (is_anchor_create)
                    {
                        is_anchor_create = false;
                        CanvasPos.Children.Remove(anchor_size);
                    }

                    var text_position_x = Math.Abs(Math.Sqrt((Math.Pow(rectangleNW.X, 2) + Math.Pow(rectangleNW.Y, 2))) - (Math.Sqrt(Math.Pow(rectnagleNE.X, 2) + Math.Pow(rectnagleNE.Y, 2)))) / 2;
                    var text_position_y = Math.Abs(Math.Sqrt((Math.Pow(rectangleNW.X, 2) + Math.Pow(rectangleNW.Y, 2))) - (Math.Sqrt(Math.Pow(rectangleSW.X, 2) + Math.Pow(rectangleSW.Y, 2)))) / 2;

                    var smt = (UIElement)sender;
                    smt.CaptureMouse();

                    Canvas.SetLeft(smt, e.GetPosition(CanvasPos).X - lastPoint.X);
                    Canvas.SetTop(smt, e.GetPosition(CanvasPos).Y - lastPoint.Y);

                    Canvas.SetLeft(txt, Canvas.GetLeft(smt) + text_position_x - 10);
                    Canvas.SetTop(txt, Canvas.GetTop(smt) + text_position_y - 5);

                    //Повторно считываем данных о фигуре при ее перемещении
                    Point LeftTop = new Point()
                    {
                        X = 0,
                        Y = 0
                    };
                    LeftTop.X = Canvas.GetLeft(polygon);
                    LeftTop.Y = Canvas.GetTop(polygon);
                    int indexOfShape = GetIndexOfShape(Shapes.Rekt, polygon.Name);
                    foreach (Block block in diagramm.blocks)
                    {
                        if (block.IndexNumber == indexOfShape)
                        {
                            diagramm.blocks.Insert(block.IndexNumber, new Block(Shapes.Rekt, LeftTop, rectangleNW, rectnagleNE, rectangleSW, rectangleSE, indexOfShape, txt.Text));
                            diagramm.blocks.Remove(block);
                            return;
                        }
                    }

                }
            }
        }

        private void ParrabellumAdd(Polygon polygon, TextBox txt, Point parabellumNW, Point parabellumSW, Point parabellumSE, Point parabellumNE)
        {
            //Считывание данных о фигуре
            Point LeftTop = new Point()
            {
                X = 0,
                Y = 0
            };

            LeftTop.X = Canvas.GetLeft(polygon);
            LeftTop.Y = Canvas.GetTop(polygon);
            diagramm.blocks.Add(new Block(Shapes.Parrabellum, LeftTop, parabellumNW, parabellumNE, parabellumSW, parabellumSE, shapesCounter - 1, txt.Text));
            diagramm.ShapesCounter++;
            //Сохранение текста внутри фигуры
            int index = GetIndexOfShape(Shapes.Parrabellum, polygon.Name);
            txt.MouseLeave += Txt_MouseLeave;
            void Txt_MouseLeave(object sender, MouseEventArgs e)
            {
                Keyboard.ClearFocus();
                diagramm.blocks[index].TextIntoTextBox = txt.Text;
            }

            polygon.MouseDown += IntoCanvasDownPolylineParrabellum;

            void IntoCanvasDownPolylineParrabellum(object sender, MouseButtonEventArgs e)
            {
                var smt = (Polygon)sender;
                lastPoint = e.GetPosition(smt);

                #region delete shape
                if (e.RightButton == MouseButtonState.Pressed)
                {
                    CanvasPos.Children.Remove(smt);
                    CanvasPos.Children.Remove(anchor_size);
                    CanvasPos.Children.Remove(txt);
                    bool isCanBeLower = false;
                    bool indexNotFound = true;
                    int indexForDeleting = GetIndexOfShape(Shapes.Parrabellum, polygon.Name);
                    shapesCounter--;

                    foreach (Block block in diagramm.blocks)
                    {
                        if ((block.IndexNumber == indexForDeleting) && (indexNotFound))
                        {
                            isCanBeLower = true;
                            indexNotFound = false;
                        }
                        if (isCanBeLower)
                        {
                            block.IndexNumber--;
                        }

                    }
                    if (diagramm.blocks.Count > 1)
                        diagramm.blocks.RemoveAt(indexForDeleting);
                    else
                        diagramm.blocks.RemoveAt(0);
                }
                #endregion

                int point_summ_main_X = (int)(startParabellumNE.X + startParabellumSE.X + startParabellumSW.X + startParabellumNW.X);
                int point_summ_main_Y = (int)(startParabellumNE.Y + startParabellumSE.Y + startParabellumSW.Y + startParabellumNW.Y);

                int point_summ_second_X = 0;
                int point_summ_second_Y = 0;


                #region anchor
                if (!is_anchor_create)
                {

                    anchor_size = new Polyline();
                    anchor_size.Points = Anchor.Points;
                    anchor_size.Stroke = Brushes.Red;
                    anchor_size.Fill = Brushes.Transparent;

                    anchor_size.MouseDown += AnchorMouseDown;
                    anchor_size.MouseMove += AnchorMouseMove;
                    anchor_size.MouseUp += AnchorMouseUp;

                    #region anchor_action
                    void AnchorMouseDown(object sndr, MouseButtonEventArgs evnt)
                    {
                        var anchor = (UIElement)sndr;
                        lastPoint_anchor = evnt.GetPosition(anchor);
                        current_anchor_postion = evnt.GetPosition(CanvasPos);
                    }
                    void AnchorMouseMove(object sndr, MouseEventArgs evnt)
                    {
                        if (evnt.LeftButton == MouseButtonState.Pressed)
                        {
                            var anchor = (Polyline)sndr;
                            anchor.CaptureMouse();

                            Cursor = Cursors.SizeNWSE;

                            anchor.Stroke = Brushes.Transparent;

                            var pos = e.GetPosition(CanvasPos) - lastPoint_anchor;

                            var text_position_x = Math.Abs((Math.Sqrt(Math.Pow(parabellumNW.X, 2) + Math.Pow(parabellumNW.Y, 2))) - (Math.Sqrt(Math.Pow(parabellumNE.X, 2) + Math.Pow(parabellumNE.Y, 2)))) / 2;
                            var text_position_y = Math.Abs((Math.Sqrt(Math.Pow(parabellumNW.X, 2) + Math.Pow(parabellumNW.Y, 2))) - (Math.Sqrt(Math.Pow(parabellumSW.X, 2) + Math.Pow(parabellumSW.Y, 2)))) / 2;


                            if (pos.X < current_anchor_postion.X && pos.Y < current_anchor_postion.Y)
                            {
                                //++fisrt.X;
                                //++fisrt.Y;

                                //++second.X;
                                parabellumSW.Y += Math.Abs(pos.Y - current_anchor_postion.Y) / 4;

                                parabellumSE.X += Math.Abs(pos.X - current_anchor_postion.X) / 2;
                                parabellumSE.Y += Math.Abs(pos.Y - current_anchor_postion.Y) / 4;

                                parabellumNE.X += Math.Abs(pos.X - current_anchor_postion.X) / 2;
                                //++fourth.Y;

                                PointCollection points = new PointCollection();
                                points.Add(parabellumNW);
                                points.Add(parabellumSW);
                                points.Add(parabellumSE);
                                points.Add(parabellumNE);

                                smt.Points = points;

                                current_anchor_postion.X = Canvas.GetLeft(polygon);
                                current_anchor_postion.Y = Canvas.GetTop(polygon);

                                Canvas.SetLeft(polygon, Canvas.GetLeft(polygon) - Math.Abs(pos.X - current_anchor_postion.X) / 2);
                                Canvas.SetTop(polygon, Canvas.GetTop(polygon) - Math.Abs(pos.Y - current_anchor_postion.Y) / 4);

                                Canvas.SetLeft(txt, Canvas.GetLeft(polygon) + text_position_x - 13);
                                Canvas.SetTop(txt, Canvas.GetTop(polygon) + text_position_y - 5);

                                point_summ_second_X = (int)(parabellumNW.X + parabellumSE.X + parabellumSW.X + parabellumNE.X);
                                point_summ_second_Y = (int)(parabellumNW.Y + parabellumSE.Y + parabellumSW.Y + parabellumNE.Y);
                            }
                            else if (point_summ_second_X > point_summ_main_X && point_summ_second_Y > point_summ_main_Y)
                            {
                                //++fisrt.X;
                                //++fisrt.Y;

                                //++second.X;
                                parabellumSW.Y -= Math.Abs(pos.Y - current_anchor_postion.Y) / 4;

                                parabellumSE.X -= Math.Abs(pos.X - current_anchor_postion.X) / 2;
                                parabellumSE.Y -= Math.Abs(pos.Y - current_anchor_postion.Y) / 4;

                                parabellumNE.X -= Math.Abs(pos.X - current_anchor_postion.X) / 2;
                                //++fourth.Y;

                                PointCollection points = new PointCollection();
                                points.Add(parabellumNW);
                                points.Add(parabellumSW);
                                points.Add(parabellumSE);
                                points.Add(parabellumNE);

                                smt.Points = points;

                                current_anchor_postion.X = Canvas.GetLeft(polygon);
                                current_anchor_postion.Y = Canvas.GetTop(polygon);

                                Canvas.SetLeft(polygon, Canvas.GetLeft(polygon) + Math.Abs(pos.X - current_anchor_postion.X) / 2);
                                Canvas.SetTop(polygon, Canvas.GetTop(polygon) + Math.Abs(pos.Y - current_anchor_postion.Y) / 4);

                                Canvas.SetLeft(txt, Canvas.GetLeft(polygon) + text_position_x - 13);
                                Canvas.SetTop(txt, Canvas.GetTop(polygon) + text_position_y - 5);

                                point_summ_second_X = (int)(parabellumNW.X + parabellumSE.X + parabellumSW.X + parabellumNE.X);
                                point_summ_second_Y = (int)(parabellumNW.Y + parabellumSE.Y + parabellumSW.Y + parabellumNE.Y);
                            }
                            //Повторно считываем данных о фигуре при изменении ее размера
                            LeftTop.X = Canvas.GetLeft(polygon);
                            LeftTop.Y = Canvas.GetTop(polygon);
                            int indexOfShape = GetIndexOfShape(Shapes.Parrabellum, polygon.Name);
                            foreach (Block block in diagramm.blocks)
                            {
                                if (block.IndexNumber == indexOfShape)
                                {
                                    diagramm.blocks.Insert(block.IndexNumber, new Block(Shapes.Parrabellum, LeftTop, parabellumNW, parabellumNE, parabellumSW, parabellumSE, indexOfShape, txt.Text));
                                    diagramm.blocks.Remove(block);
                                    return;
                                }
                            }

                            Canvas.SetLeft(anchor, pos.X);
                            Canvas.SetTop(anchor, pos.Y);
                        }
                    }
                    void AnchorMouseUp(object sndr, MouseButtonEventArgs evnt)
                    {
                        var anchor = (Polyline)sndr;
                        anchor.ReleaseMouseCapture();

                        Cursor = Cursors.Arrow;
                        anchor.Stroke = Brushes.Transparent;

                        Canvas.SetLeft(anchor, Canvas.GetLeft(polygon));
                        Canvas.SetTop(anchor, Canvas.GetTop(polygon));
                    }

                    #endregion

                    CanvasPos.Children.Add(anchor_size);


                    Canvas.SetLeft(anchor_size, Canvas.GetLeft(smt));
                    Canvas.SetTop(anchor_size, Canvas.GetTop(smt));

                    is_anchor_create = true;
                }
                #endregion

                ParrabellumIntoCanvasMouseMove(polygon, txt, parabellumNW, parabellumSE, parabellumSE, parabellumNE);
            }
        }
        private void ParrabellumIntoCanvasMouseMove(Polygon polygon, TextBox txt, Point parabellumNW, Point parabellumSW, Point parabellumSE, Point parabellumNE)
        {
            polygon.MouseMove += IntoCanvasMove;

            void IntoCanvasMove(object sender, MouseEventArgs e)
            {
                if (e.LeftButton == MouseButtonState.Pressed)
                {
                    if (is_anchor_create)
                    {
                        is_anchor_create = false;
                        CanvasPos.Children.Remove(anchor_size);
                    }

                    var text_position_x = Math.Abs(Math.Sqrt(Math.Pow(parabellumNW.X, 2) + Math.Pow(parabellumNW.Y, 2)) - Math.Sqrt(Math.Pow(parabellumNE.X, 2) + Math.Pow(parabellumNE.Y, 2))) / 2;
                    var text_position_y = Math.Abs(Math.Sqrt(Math.Pow(parabellumNW.X, 2) + Math.Pow(parabellumNW.Y, 2)) - Math.Sqrt(Math.Pow(parabellumSW.X, 2) + Math.Pow(parabellumSW.Y, 2))) / 4;

                    var smt = (Polygon)sender;
                    smt.CaptureMouse();

                    Canvas.SetLeft(smt, e.GetPosition(CanvasPos).X - lastPoint.X);
                    Canvas.SetTop(smt, e.GetPosition(CanvasPos).Y - lastPoint.Y);

                    Canvas.SetLeft(txt, Canvas.GetLeft(smt) + text_position_x - 13);
                    Canvas.SetTop(txt, Canvas.GetTop(smt) + text_position_y - 6);

                    //Повторно считываем данных о фигуре при перемещении
                    Point LeftTop = new Point()
                    {
                        X = 0,
                        Y = 0
                    };
                    LeftTop.X = Canvas.GetLeft(polygon);
                    LeftTop.Y = Canvas.GetTop(polygon);
                    int indexOfShape = GetIndexOfShape(Shapes.Parrabellum, polygon.Name);
                    foreach (Block block in diagramm.blocks)
                    {
                        if (block.IndexNumber == indexOfShape)
                        {
                            diagramm.blocks.Insert(block.IndexNumber, new Block(Shapes.Parrabellum, LeftTop, parabellumNW, parabellumNE, parabellumSW, parabellumSE, indexOfShape, txt.Text));
                            diagramm.blocks.Remove(block);
                            return;
                        }
                    }

                }
            }
        }

        private void RhombAdd(Polygon polygon, TextBox txt, Point rhombN, Point rhombW, Point rhombS, Point rhombE)
        {
            //Считывание данных о фигуре
            Point LeftTop = new Point()
            {
                X = 0,
                Y = 0
            };
            LeftTop.X = Canvas.GetLeft(polygon);
            LeftTop.Y = Canvas.GetTop(polygon);
            diagramm.blocks.Add(new Block(Shapes.Rhomb, LeftTop, rhombN, rhombW, rhombS, rhombE, shapesCounter - 1, txt.Text));
            diagramm.ShapesCounter++;
            //Сохранение текста внутри фигуры
            int index = GetIndexOfShape(Shapes.Rhomb, polygon.Name);
            txt.MouseLeave += Txt_MouseLeave;
            void Txt_MouseLeave(object sender, MouseEventArgs e)
            {
                Keyboard.ClearFocus();
                diagramm.blocks[index].TextIntoTextBox = txt.Text;
            }

            polygon.MouseDown += IntoCanvasDownPolylineRhomb;

            void IntoCanvasDownPolylineRhomb(object sender, MouseButtonEventArgs e)
            {
                var smt = (Polygon)sender;
                lastPoint = e.GetPosition(smt);

                #region anchor
                if (!is_anchor_create)
                {
                    anchor_size = new Polyline();
                    anchor_size.Points = Anchor.Points;
                    anchor_size.Stroke = Brushes.Red;
                    anchor_size.Fill = Brushes.Transparent;

                    anchor_size.MouseDown += AnchorMouseDown;
                    anchor_size.MouseMove += AnchorMouseMove;
                    anchor_size.MouseUp += AnchorMouseUp;

                    #region anchor_action

                    void AnchorMouseDown(object sndr, MouseButtonEventArgs evnt)
                    {
                        var anchor = (UIElement)sndr;
                        lastPoint_anchor = evnt.GetPosition(anchor);
                        current_anchor_postion = evnt.GetPosition(CanvasPos);
                    }
                    void AnchorMouseMove(object sndr, MouseEventArgs evnt)
                    {
                        if (evnt.LeftButton == MouseButtonState.Pressed)
                        {
                            var anchor = (Polyline)sndr;
                            anchor.CaptureMouse();

                            anchor.Stroke = Brushes.Transparent;
                            Cursor = Cursors.SizeNWSE;

                            var pos = e.GetPosition(CanvasPos) - lastPoint_anchor;


                            if (pos.X < current_anchor_postion.X && pos.Y < current_anchor_postion.Y)
                            {
                                rhombW.X -= 1;
                                //rhombW.Y += .5;

                                rhombS.Y += .5;
                                //rhombS.X += .5;

                                rhombE.X += 1;
                                //rhombE.Y += .5;

                                //rhombN.X += .5;
                                rhombN.Y -= .5;

                                PointCollection points = new PointCollection();
                                points.Add(rhombW);
                                points.Add(rhombS);
                                points.Add(rhombE);
                                points.Add(rhombN);

                                smt.Points = points;

                                /*                                Canvas.SetLeft(polygon, Canvas.GetLeft(polygon));
                                                                Canvas.SetTop(polygon, Canvas.GetTop(polygon));
                                */
                                current_anchor_postion.X = Canvas.GetLeft(polygon);
                                current_anchor_postion.Y = Canvas.GetTop(polygon);
                            }
                            else if (startRhombE != rhombE)
                            {
                                rhombW.X += 1;
                                rhombS.Y -= .5;
                                rhombE.X -= 1;
                                rhombN.Y += .5;

                                PointCollection points = new PointCollection();
                                points.Add(rhombW);
                                points.Add(rhombS);
                                points.Add(rhombE);
                                points.Add(rhombN);

                                smt.Points = points;

                                /*                                Canvas.SetLeft(polygon, Canvas.GetLeft(polygon) - 1);
                                                                Canvas.SetTop(polygon, Canvas.GetTop(polygon) - .5);
                                */
                                current_anchor_postion.X = Canvas.GetLeft(polygon);
                                current_anchor_postion.Y = Canvas.GetTop(polygon);
                            }

                            //Повторно считываем данных о фигуре при изменении ее размера
                            LeftTop.X = Canvas.GetLeft(polygon);
                            LeftTop.Y = Canvas.GetTop(polygon);
                            int indexOfShape = GetIndexOfShape(Shapes.Rhomb, polygon.Name);
                            foreach (Block block in diagramm.blocks)
                            {
                                if (block.IndexNumber == indexOfShape)
                                {
                                    diagramm.blocks.Insert(block.IndexNumber, new Block(Shapes.Rhomb, LeftTop, rhombN, rhombW, rhombS, rhombE, indexOfShape, txt.Text));
                                    diagramm.blocks.Remove(block);
                                    return;
                                }
                            }

                            Canvas.SetLeft(anchor, pos.X);
                            Canvas.SetTop(anchor, pos.Y);
                        }
                    }
                    void AnchorMouseUp(object sndr, MouseButtonEventArgs evnt)
                    {
                        var anchor = (Polyline)sndr;
                        anchor.ReleaseMouseCapture();

                        anchor.Stroke = Brushes.Transparent;
                        Cursor = Cursors.Arrow;

                        Canvas.SetLeft(anchor, Canvas.GetLeft(polygon));
                        Canvas.SetTop(anchor, Canvas.GetTop(polygon));
                    }

                    #endregion

                    CanvasPos.Children.Add(anchor_size);

                    Canvas.SetLeft(anchor_size, Canvas.GetLeft(smt));
                    Canvas.SetTop(anchor_size, Canvas.GetTop(smt));

                    is_anchor_create = true;
                }
                #endregion

                #region delete shape
                if (e.RightButton == MouseButtonState.Pressed)
                {
                    CanvasPos.Children.Remove(smt);
                    CanvasPos.Children.Remove(anchor_size);
                    CanvasPos.Children.Remove(txt);
                    bool isCanBeLower = false;
                    bool indexNotFound = true;
                    int indexForDeleting = GetIndexOfShape(Shapes.Rhomb, polygon.Name);
                    shapesCounter--;

                    foreach (Block block in diagramm.blocks)
                    {
                        if ((block.IndexNumber == indexForDeleting) && (indexNotFound))
                        {
                            isCanBeLower = true;
                            indexNotFound = false;
                        }
                        if (isCanBeLower)
                        {
                            block.IndexNumber--;
                        }

                    }
                    if (diagramm.blocks.Count > 1)
                        diagramm.blocks.RemoveAt(indexForDeleting);
                    else
                        diagramm.blocks.RemoveAt(0);
                }
                #endregion

                RhombIntoCanvasMouseMove(polygon, txt, rhombN, rhombW, rhombS, rhombE);
            }
        }
        private void RhombIntoCanvasMouseMove(Polygon polygon, TextBox txt, Point rhombN, Point rhombW, Point rhombS, Point rhombE)
        {
            polygon.MouseMove += IntoCanvasMove;

            void IntoCanvasMove(object sender, MouseEventArgs e)
            {
                if (e.LeftButton == MouseButtonState.Pressed)
                {
                    if (is_anchor_create)
                    {
                        is_anchor_create = false;
                        CanvasPos.Children.Remove(anchor_size);
                    }

                    var smt = (UIElement)sender;
                    smt.CaptureMouse();

                    Canvas.SetLeft(smt, e.GetPosition(CanvasPos).X - lastPoint.X);
                    Canvas.SetTop(smt, e.GetPosition(CanvasPos).Y - lastPoint.Y);

                    Canvas.SetLeft(txt, Canvas.GetLeft(smt) + 25);
                    Canvas.SetTop(txt, Canvas.GetTop(smt) - 1);

                    //Повторно считываем данных о фигуре при изменении ее размера
                    Point LeftTop = new Point()
                    {
                        X = 0,
                        Y = 0
                    };
                    LeftTop.X = Canvas.GetLeft(polygon);
                    LeftTop.Y = Canvas.GetTop(polygon);
                    int indexOfShape = GetIndexOfShape(Shapes.Rhomb, polygon.Name);
                    foreach (Block block in diagramm.blocks)
                    {
                        if (block.IndexNumber == indexOfShape)
                        {
                            diagramm.blocks.Insert(block.IndexNumber, new Block(Shapes.Rhomb, LeftTop, rhombN, rhombW, rhombS, rhombE, indexOfShape, txt.Text));
                            diagramm.blocks.Remove(block);
                            return;
                        }
                    }

                }
            }
        }

        private void CycleAdd(Polygon polygon, TextBox txt, Point cycleNW, Point cycleW, Point cycleSW, Point cycleSE, Point cycleE, Point cycleNE)
        {

            //Считывание данных о фигуре
            Point LeftTop = new Point()
            {
                X = 0,
                Y = 0
            };

            LeftTop.X = Canvas.GetLeft(polygon);
            LeftTop.Y = Canvas.GetTop(polygon);
            diagramm.blocks.Add(new Block(Shapes.Cycle, LeftTop, cycleNW, cycleNE, cycleSW, cycleSE, cycleW, cycleE, shapesCounter - 1, txt.Text));
            diagramm.ShapesCounter++;
            //Сохранение текста внутри фигуры
            int index = GetIndexOfShape(Shapes.Cycle, polygon.Name);
            txt.MouseLeave += Txt_MouseLeave;
            void Txt_MouseLeave(object sender, MouseEventArgs e)
            {
                Keyboard.ClearFocus();
                diagramm.blocks[index].TextIntoTextBox = txt.Text;
            }

            polygon.MouseDown += IntoCanvasDownPolylineCycle;

            void IntoCanvasDownPolylineCycle(object sender, MouseButtonEventArgs e)
            {
                var smt = (Polygon)sender;
                lastPoint = e.GetPosition(smt);

                #region anchor
                if (!is_anchor_create)
                {

                    anchor_size = new Polyline();
                    anchor_size.Points = Anchor.Points;
                    anchor_size.Stroke = Brushes.Red;
                    anchor_size.Fill = Brushes.Transparent;

                    anchor_size.MouseDown += AnchorMouseDown;
                    anchor_size.MouseMove += AnchorMouseMove;
                    anchor_size.MouseUp += AnchorMouseUp;

                    #region anchor_action

                    void AnchorMouseDown(object sndr, MouseButtonEventArgs evnt)
                    {
                        var anchor = (UIElement)sndr;
                        lastPoint_anchor = evnt.GetPosition(anchor);
                        current_anchor_postion = evnt.GetPosition(CanvasPos);
                    }
                    void AnchorMouseMove(object sndr, MouseEventArgs evnt)
                    {
                        if (evnt.LeftButton == MouseButtonState.Pressed)
                        {

                            var anchor = (Polyline)sndr;
                            anchor.CaptureMouse();

                            anchor.Stroke = Brushes.Transparent;
                            Cursor = Cursors.SizeNWSE;

                            var pos = e.GetPosition(CanvasPos) - lastPoint_anchor;

                            if (pos.X < current_anchor_postion.X && pos.Y < current_anchor_postion.Y)
                            {
                                cycleW.X -= .2;
                                cycleW.Y += .25;

                                cycleSW.Y += .5;

                                cycleSE.X += 1;
                                cycleSE.Y += .5;

                                cycleE.X += 1.2;
                                cycleE.Y += .25;

                                cycleNE.X += 1;
                                //cycleNE.Y -= .5;

                                //cycleNW.Y -= .5;

                                PointCollection points = new PointCollection();
                                points.Add(cycleW);
                                points.Add(cycleSW);
                                points.Add(cycleSE);
                                points.Add(cycleE);
                                points.Add(cycleNE);
                                points.Add(cycleNW);

                                smt.Points = points;

                                current_anchor_postion.X = Canvas.GetLeft(polygon);
                                current_anchor_postion.Y = Canvas.GetTop(polygon);

                                var text_position_x = Math.Abs(Math.Sqrt(Math.Pow(cycleNW.X, 2) + Math.Pow(cycleNW.Y, 2)) - Math.Sqrt(Math.Pow(cycleNE.X, 2) + Math.Pow(cycleNE.Y, 2))) / 2;
                                var text_position_y = Math.Abs(Math.Sqrt(Math.Pow(cycleNW.X, 2) + Math.Pow(cycleNW.Y, 2)) - Math.Sqrt(Math.Pow(cycleSW.X, 2) + Math.Pow(cycleSW.Y, 2))) / 2;

                                Canvas.SetLeft(polygon, Canvas.GetLeft(polygon) - 1);
                                Canvas.SetTop(polygon, Canvas.GetTop(polygon) - .5);

                                Canvas.SetLeft(txt, Canvas.GetLeft(polygon) + text_position_x - 10);
                                Canvas.SetTop(txt, Canvas.GetTop(polygon) + text_position_y - 5);
                            }
                            else if (startCycleNE != cycleNE)
                            {
                                cycleW.X += .2;
                                cycleW.Y -= .25;

                                cycleSW.Y -= .5;

                                cycleSE.X -= 1;
                                cycleSE.Y -= .5;

                                cycleE.X -= 1.2;
                                cycleE.Y -= .25;

                                cycleNE.X -= 1;
                                //cycleNE.Y += .5;

                                //cycleNW.Y += .5;

                                PointCollection points = new PointCollection();
                                points.Add(cycleW);
                                points.Add(cycleSW);
                                points.Add(cycleSE);
                                points.Add(cycleE);
                                points.Add(cycleNE);
                                points.Add(cycleNW);

                                smt.Points = points;

                                current_anchor_postion.X = Canvas.GetLeft(polygon);
                                current_anchor_postion.Y = Canvas.GetTop(polygon);

                                var text_position_x = Math.Abs(Math.Sqrt(Math.Pow(cycleNW.X, 2) + Math.Pow(cycleNW.Y, 2)) - Math.Sqrt(Math.Pow(cycleNE.X, 2) + Math.Pow(cycleNE.Y, 2))) / 2;
                                var text_position_y = Math.Abs(Math.Sqrt(Math.Pow(cycleNW.X, 2) + Math.Pow(cycleNW.Y, 2)) - Math.Sqrt(Math.Pow(cycleSW.X, 2) + Math.Pow(cycleSW.Y, 2))) / 2;

                                Canvas.SetLeft(polygon, Canvas.GetLeft(polygon) + 1);
                                Canvas.SetTop(polygon, Canvas.GetTop(polygon) + .5);

                                Canvas.SetLeft(txt, Canvas.GetLeft(polygon) + text_position_x - 10);
                                Canvas.SetTop(txt, Canvas.GetTop(polygon) + text_position_y - 5);
                            }
                            //Повторно считываем данных о фигуре при изменении ее размера
                            LeftTop.X = Canvas.GetLeft(polygon);
                            LeftTop.Y = Canvas.GetTop(polygon);
                            int indexOfShape = GetIndexOfShape(Shapes.Cycle, polygon.Name);
                            foreach (Block block in diagramm.blocks)
                            {
                                if (block.IndexNumber == indexOfShape)
                                {
                                    diagramm.blocks.Insert(block.IndexNumber, new Block(Shapes.Cycle, LeftTop, cycleNW, cycleNE, cycleSW, cycleSE, cycleW, cycleE, indexOfShape, txt.Text));
                                    diagramm.blocks.Remove(block);
                                    return;
                                }
                            }

                            Canvas.SetLeft(anchor, pos.X);
                            Canvas.SetTop(anchor, pos.Y);
                        }
                    }
                    void AnchorMouseUp(object sndr, MouseButtonEventArgs evnt)
                    {
                        var anchor = (Polyline)sndr;
                        anchor.ReleaseMouseCapture();

                        anchor.Stroke = Brushes.Red;
                        Cursor = Cursors.Arrow;

                        Canvas.SetLeft(anchor, Canvas.GetLeft(smt));
                        Canvas.SetTop(anchor, Canvas.GetTop(smt));
                    }

                    #endregion

                    CanvasPos.Children.Add(anchor_size);

                    Canvas.SetLeft(anchor_size, Canvas.GetLeft(smt));
                    Canvas.SetTop(anchor_size, Canvas.GetTop(smt));

                    is_anchor_create = true;
                }
                #endregion

                #region delete shape
                if (e.RightButton == MouseButtonState.Pressed)
                {
                    CanvasPos.Children.Remove(smt);
                    CanvasPos.Children.Remove(anchor_size);
                    CanvasPos.Children.Remove(txt);
                    bool isCanBeLower = false;
                    bool indexNotFound = true;
                    int indexForDeleting = GetIndexOfShape(Shapes.Cycle, polygon.Name);
                    shapesCounter--;

                    foreach (Block block in diagramm.blocks)
                    {
                        if ((block.IndexNumber == indexForDeleting) && (indexNotFound))
                        {
                            isCanBeLower = true;
                            indexNotFound = false;
                        }
                        if (isCanBeLower)
                        {
                            block.IndexNumber--;
                        }

                    }
                    if (diagramm.blocks.Count > 1)
                        diagramm.blocks.RemoveAt(indexForDeleting);
                    else
                        diagramm.blocks.RemoveAt(0);
                }
                #endregion

                CycleIntoCanvasMouseMove(polygon, txt, cycleNW, cycleW, cycleSW, cycleSE, cycleE, cycleNE);
            }
        }
        private void CycleIntoCanvasMouseMove(Polygon polygon, TextBox txt, Point cycleNW, Point cycleW, Point cycleSW, Point cycleSE, Point cycleE, Point cycleNE)
        {
            polygon.MouseMove += IntoCanvasMove;

            void IntoCanvasMove(object sender, MouseEventArgs e)
            {
                if (e.LeftButton == MouseButtonState.Pressed)
                {
                    if (is_anchor_create)
                    {
                        is_anchor_create = false;
                        CanvasPos.Children.Remove(anchor_size);
                    }

                    var smt = (UIElement)sender;
                    smt.CaptureMouse();

                    var text_position_x = Math.Abs(Math.Sqrt(Math.Pow(cycleNW.X, 2) + Math.Pow(cycleNW.Y, 2)) - Math.Sqrt(Math.Pow(cycleNE.X, 2) + Math.Pow(cycleNE.Y, 2))) / 2;
                    var text_position_y = Math.Abs(Math.Sqrt(Math.Pow(cycleNW.X, 2) + Math.Pow(cycleNW.Y, 2)) - Math.Sqrt(Math.Pow(cycleSW.X, 2) + Math.Pow(cycleSW.Y, 2))) / 2;

                    Canvas.SetLeft(smt, e.GetPosition(CanvasPos).X - lastPoint.X);
                    Canvas.SetTop(smt, e.GetPosition(CanvasPos).Y - lastPoint.Y);

                    Canvas.SetLeft(txt, Canvas.GetLeft(smt) + text_position_x - 10);
                    Canvas.SetTop(txt, Canvas.GetTop(smt) + text_position_y - 5);

                    //Повторно считываем данных о фигуре при передвижении
                    Point LeftTop = new Point()
                    {
                        X = 0,
                        Y = 0
                    };
                    LeftTop.X = Canvas.GetLeft(polygon);
                    LeftTop.Y = Canvas.GetTop(polygon);
                    int indexOfShape = GetIndexOfShape(Shapes.Cycle, polygon.Name);
                    foreach (Block block in diagramm.blocks)
                    {
                        if (block.IndexNumber == indexOfShape)
                        {
                            diagramm.blocks.Insert(block.IndexNumber, new Block(Shapes.Cycle, LeftTop, cycleNW, cycleNE, cycleSW, cycleSE, cycleW, cycleE, indexOfShape,txt.Text));
                            diagramm.blocks.Remove(block);
                            return;
                        }
                    }
                }
            }
        }

        private void EllipseAdd(Rectangle polygon, TextBox txt, double width, double height)
        {
            //Считывание данных о фигуре
            Point LeftTop = new Point()
            {
                X = 0,
                Y = 0
            };

            LeftTop.X = Canvas.GetLeft(polygon);
            LeftTop.Y = Canvas.GetTop(polygon);
            diagramm.blocks.Add(new Block(Shapes.Ellipse, LeftTop, width, height, shapesCounter - 1, txt.Text));
            diagramm.ShapesCounter++;
            //Сохранение текста внутри фигуры
            int index = GetIndexOfShape(Shapes.Ellipse, polygon.Name);
            txt.MouseLeave += Txt_MouseLeave;
            void Txt_MouseLeave(object sender, MouseEventArgs e)
            {
                Keyboard.ClearFocus();
                diagramm.blocks[index].TextIntoTextBox = txt.Text;
            }

            polygon.MouseDown += IntoCanvasMouseDownRectangle;

            void IntoCanvasMouseDownRectangle(object sender, MouseButtonEventArgs e)
            {
                var smt = (Rectangle)sender;
                lastPoint = e.GetPosition(smt);

                #region anchor
                if (!is_anchor_create)
                {
                    anchor_size = new Polyline();
                    anchor_size.Points = Anchor.Points;
                    anchor_size.Stroke = Brushes.Red;
                    anchor_size.Fill = Brushes.Transparent;

                    anchor_size.MouseDown += AnchorMouseDown;
                    anchor_size.MouseMove += AnchorMouseMove;
                    anchor_size.MouseUp += AnchorMouseUp;

                    #region anchor_action

                    void AnchorMouseDown(object sndr, MouseButtonEventArgs evnt)
                    {
                        var anchor = (UIElement)sndr;
                        lastPoint_anchor = evnt.GetPosition(anchor);
                        current_anchor_postion = evnt.GetPosition(CanvasPos);
                    }
                    void AnchorMouseMove(object sndr, MouseEventArgs evnt)
                    {
                        if (evnt.LeftButton == MouseButtonState.Pressed)
                        {
                            var anchor = (Polyline)sndr;
                            anchor.CaptureMouse();

                            anchor.Stroke = Brushes.Transparent;
                            Cursor = Cursors.SizeNWSE;

                            var pos = e.GetPosition(CanvasPos) - lastPoint_anchor;

                            if (pos.X < current_anchor_postion.X && pos.Y < current_anchor_postion.Y)
                            {
                                smt.Width += Math.Abs(pos.X - current_anchor_postion.X) / 2;
                                smt.Height += Math.Abs(pos.Y - current_anchor_postion.Y) / 4;

                                current_anchor_postion.X = Canvas.GetLeft(polygon);
                                current_anchor_postion.Y = Canvas.GetTop(polygon);

                                Canvas.SetLeft(polygon, Canvas.GetLeft(polygon) - Math.Abs(pos.X - current_anchor_postion.X) / 2);
                                Canvas.SetTop(polygon, Canvas.GetTop(polygon) - Math.Abs(pos.Y - current_anchor_postion.Y) / 4);

                                Canvas.SetLeft(txt, Canvas.GetLeft(polygon) + polygon.Width / 2 - 15);
                                Canvas.SetTop(txt, Canvas.GetTop(polygon) + polygon.Height / 2 - 10);
                            }
                            else if (polygon.Width > 75 && polygon.Height > 25)
                            {
                                smt.Width -= Math.Abs(pos.X - current_anchor_postion.X) / 2;
                                smt.Height -= Math.Abs(pos.Y - current_anchor_postion.Y) / 2;

                                current_anchor_postion.X = Canvas.GetLeft(polygon);
                                current_anchor_postion.Y = Canvas.GetTop(polygon);

                                Canvas.SetLeft(polygon, Canvas.GetLeft(polygon) + Math.Abs(pos.X - current_anchor_postion.X) / 2);
                                Canvas.SetTop(polygon, Canvas.GetTop(polygon) + Math.Abs(pos.X - current_anchor_postion.X) / 4);

                                Canvas.SetLeft(txt, Canvas.GetLeft(polygon) + polygon.Width / 2 - 15);
                                Canvas.SetTop(txt, Canvas.GetTop(polygon) + polygon.Height / 2 - 10);

                            }

                            //Повторно считываем данных о фигуре при изменении ее размера
                            LeftTop.X = Canvas.GetLeft(polygon);
                            LeftTop.Y = Canvas.GetTop(polygon);
                            int indexOfShape = GetIndexOfShape(Shapes.Ellipse, polygon.Name);
                            foreach (Block block in diagramm.blocks)
                            {
                                if (block.IndexNumber == indexOfShape)
                                {
                                    diagramm.blocks.Insert(block.IndexNumber, new Block(Shapes.Ellipse, LeftTop, smt.Width, smt.Height, indexOfShape, txt.Text));
                                    diagramm.blocks.Remove(block);
                                    return;
                                }
                            }

                            Canvas.SetLeft(anchor, pos.X);
                            Canvas.SetTop(anchor, pos.Y);
                        }
                    }
                    void AnchorMouseUp(object sndr, MouseButtonEventArgs evnt)
                    {
                        var anchor = (Polyline)sndr;
                        anchor.ReleaseMouseCapture();

                        Cursor = Cursors.Arrow;
                        anchor.Stroke = Brushes.Red;

                        Canvas.SetLeft(anchor, Canvas.GetLeft(polygon));
                        Canvas.SetTop(anchor, Canvas.GetTop(polygon));
                    }

                    #endregion

                    CanvasPos.Children.Add(anchor_size);

                    Canvas.SetLeft(anchor_size, Canvas.GetLeft(smt));
                    Canvas.SetTop(anchor_size, Canvas.GetTop(smt));

                    is_anchor_create = true;
                }
                #endregion

                #region delete shape
                if (e.RightButton == MouseButtonState.Pressed)
                {
                    CanvasPos.Children.Remove(smt);
                    CanvasPos.Children.Remove(anchor_size);
                    CanvasPos.Children.Remove(txt);
                    bool isCanBeLower = false;
                    bool indexNotFound = true;
                    int indexForDeleting = GetIndexOfShape(Shapes.Ellipse, polygon.Name);
                    shapesCounter--;

                    foreach (Block block in diagramm.blocks)
                    {
                        if ((block.IndexNumber == indexForDeleting) && (indexNotFound))
                        {
                            isCanBeLower = true;
                            indexNotFound = false;
                        }
                        if (isCanBeLower)
                        {
                            block.IndexNumber--;
                        }

                    }
                    if (diagramm.blocks.Count > 1)
                        diagramm.blocks.RemoveAt(indexForDeleting);
                    else
                        diagramm.blocks.RemoveAt(0);
                }
                #endregion

                EllipseIntoCanvasMouseMove(polygon, txt, width, height);
            }
        }
        private void EllipseIntoCanvasMouseMove(Rectangle polygon, TextBox txt, double width, double height)
        {
            polygon.MouseMove += IntoCanvasMove;

            void IntoCanvasMove(object sender, MouseEventArgs e)
            {
                if (e.LeftButton == MouseButtonState.Pressed)
                {
                    if (is_anchor_create)
                    {
                        is_anchor_create = false;
                        CanvasPos.Children.Remove(anchor_size);
                    }

                    var smt = (UIElement)sender;
                    smt.CaptureMouse();

                    Canvas.SetLeft(smt, e.GetPosition(CanvasPos).X - lastPoint.X);
                    Canvas.SetTop(smt, e.GetPosition(CanvasPos).Y - lastPoint.Y);

                    Canvas.SetLeft(txt, Canvas.GetLeft(smt) + polygon.Width / 2 - 15);
                    Canvas.SetTop(txt, Canvas.GetTop(smt) + polygon.Height / 2 - 10);

                    //Повторно считываем данных о фигуре при передвижении
                    Point LeftTop = new Point() { X = 0, Y = 0 };
                    LeftTop.X = Canvas.GetLeft(polygon);
                    LeftTop.Y = Canvas.GetTop(polygon);
                    int indexOfShape = GetIndexOfShape(Shapes.Ellipse, polygon.Name);
                    foreach (Block block in diagramm.blocks)
                    {
                        if (block.IndexNumber == indexOfShape)
                        {
                            diagramm.blocks.Insert(block.IndexNumber, new Block(Shapes.Ellipse, LeftTop, width, height, indexOfShape, txt.Text));
                            diagramm.blocks.Remove(block);
                            return;
                        }
                    }
                }
            }
        }
        //Отменить захват на фигуре
        public void IntoCanvasUp(object sender, MouseButtonEventArgs e)
        {
            var smt = (UIElement)sender;
            smt.ReleaseMouseCapture();
        }
        #endregion
        //Метод, происходящий при сбросе фигуры в Canvas №2
        private void DnD_Drop(object sender, DragEventArgs e)
        {
            #region points
            //Создание точек для всех возможных фигур
            Point rectangleNW = new Point();
            Point rectangleSW = new Point();
            Point rectangleSE = new Point();
            Point rectnagleNE = new Point();

            Point parabellumNW = new Point();
            Point parabellumSW = new Point();
            Point parabellumSE = new Point();
            Point parabellumNE = new Point();

            Point rhombN = new Point();
            Point rhombW = new Point();
            Point rhombS = new Point();
            Point rhombE = new Point();

            Point cycleNW = new Point();
            Point cycleW = new Point();
            Point cycleSW = new Point();
            Point cycleSE = new Point();
            Point cycleE = new Point();
            Point cycleNE = new Point();

            #endregion

            #region initialized shapes points

            #region rectangle points
            rectangleNW.X = 8;
            rectangleNW.Y = 1;

            rectangleSW.X = 8;
            rectangleSW.Y = 30;

            rectangleSE.X = 60;
            rectangleSE.Y = 30;

            rectnagleNE.X = 60;
            rectnagleNE.Y = 1;
            #endregion

            #region parrabellum points
            parabellumNW.X = 8;
            parabellumNW.Y = 1;

            parabellumSW.X = 0;
            parabellumSW.Y = 30;

            parabellumSE.X = 60;
            parabellumSE.Y = 30;

            parabellumNE.X = 68;
            parabellumNE.Y = 1;
            #endregion

            #region rhomb points
            rhombW.X = 0;
            rhombW.Y = 8;

            rhombS.X = 40;
            rhombS.Y = 20;

            rhombE.X = 80;
            rhombE.Y = 8;

            rhombN.X = 40;
            rhombN.Y = -4;
            #endregion

            #region cycle points

            cycleW.X = -1;
            cycleW.Y = 15;

            cycleSW.X = 8;
            cycleSW.Y = 30;

            cycleSE.X = 60;
            cycleSE.Y = 30;

            cycleE.X = 69;
            cycleE.Y = 15;

            cycleNE.X = 60;
            cycleNE.Y = 1;

            cycleNW.X = 8;
            cycleNW.Y = 1;
            #endregion

            #endregion
            //Создание фигур
            if (Rectangle_Check)
            {
                
                Polygon polyline = new Polygon();
                TextBox text_into_shapes = new TextBox();
                //Индексация фигур              
                polyline.Name = "Rect_" + shapesCounter.ToString();
                shapesCounter++;

                Rectangle_Check = false;
                polyline.Points = Rekt.Points;

                text_into_shapes.Text = "Text";
                text_into_shapes.MinWidth = 40;
                text_into_shapes.MinHeight = 20;
                text_into_shapes.FontSize = 10;
                text_into_shapes.BorderBrush = Brushes.Transparent;
                text_into_shapes.Foreground = Brushes.White;
                text_into_shapes.Background = Brushes.Transparent;

                polyline.Stroke = Brushes.White;
                polyline.Fill = Brushes.Transparent;

                polyline.MouseUp += IntoCanvasUp;

                var text_position_x = Math.Abs(Math.Sqrt((Math.Pow(rectangleNW.X, 2) + Math.Pow(rectangleNW.Y, 2))) - (Math.Sqrt(Math.Pow(rectnagleNE.X, 2) + Math.Pow(rectnagleNE.Y, 2)))) / 2;
                var text_position_y = Math.Abs(Math.Sqrt((Math.Pow(rectangleNW.X, 2) + Math.Pow(rectangleNW.Y, 2))) - (Math.Sqrt(Math.Pow(rectangleSW.X, 2) + Math.Pow(rectangleSW.Y, 2)))) / 2;

                CanvasPos.Children.Add(polyline);
                CanvasPos.Children.Add(text_into_shapes);

                Canvas.SetLeft(polyline, e.GetPosition(CanvasPos).X - lastPoint.X);
                Canvas.SetTop(polyline, e.GetPosition(CanvasPos).Y - lastPoint.Y);

                Canvas.SetLeft(text_into_shapes, Canvas.GetLeft(polyline) + text_position_x - 10);
                Canvas.SetTop(text_into_shapes, Canvas.GetTop(polyline) + text_position_y - 5);

                RectangleAdd(polyline, text_into_shapes, rectangleNW, rectangleSE, rectangleSW, rectnagleNE);

                
            }
            if (Parrabullem_Check)
            {


                Polygon polyline = new Polygon();
                TextBox text_into_shapes = new TextBox();

                //Индексация фигур              
                polyline.Name = "Parrabullem_" + shapesCounter.ToString();
                shapesCounter++;

                Parrabullem_Check = false;
                polyline.Points = Parrabellum.Points;

                text_into_shapes.Text = "Text";
                text_into_shapes.MinWidth = 40;
                text_into_shapes.MinHeight = 20;
                text_into_shapes.FontSize = 10;
                text_into_shapes.BorderBrush = Brushes.Transparent;
                text_into_shapes.Foreground = Brushes.White;
                text_into_shapes.Background = Brushes.Transparent;

                polyline.Stroke = Brushes.White;
                polyline.Fill = Brushes.Transparent;

                polyline.MouseUp += IntoCanvasUp;

                CanvasPos.Children.Add(polyline);
                CanvasPos.Children.Add(text_into_shapes);

                var text_position_x = Math.Abs((Math.Sqrt(Math.Pow(parabellumNW.X, 2) + Math.Pow(parabellumNW.Y, 2))) - (Math.Sqrt(Math.Pow(parabellumNE.X, 2) + Math.Pow(parabellumNE.Y, 2)))) / 2;
                var text_position_y = Math.Abs(Math.Sqrt(Math.Pow(parabellumNW.X, 2) + Math.Pow(parabellumNW.Y, 2)) - Math.Sqrt(Math.Pow(parabellumSW.X, 2) + Math.Pow(parabellumSW.Y, 2))) / 2;

                Canvas.SetLeft(polyline, e.GetPosition(CanvasPos).X - lastPoint.X);
                Canvas.SetTop(polyline, e.GetPosition(CanvasPos).Y - lastPoint.Y);

                Canvas.SetLeft(text_into_shapes, Canvas.GetLeft(polyline) + text_position_x - 13);
                Canvas.SetTop(text_into_shapes, Canvas.GetTop(polyline) + text_position_y - 2);

                ParrabellumAdd(polyline, text_into_shapes, parabellumNW, parabellumSW, parabellumSE, parabellumNE);
            }
            if (Rhomb_Check)
            {
                Rhomb_Check = false;

                Polygon polyline = new Polygon();
                TextBox text_into_shapes = new TextBox();

                //Индексация фигур              
                polyline.Name = "Rhomb_" + shapesCounter.ToString();
                shapesCounter++;

                Parrabullem_Check = false;
                polyline.Points = Rhomb.Points;

                text_into_shapes.Text = "Text";
                text_into_shapes.MinWidth = 40;
                text_into_shapes.MinHeight = 20;
                text_into_shapes.FontSize = 10;
                text_into_shapes.BorderBrush = Brushes.Transparent;
                text_into_shapes.Foreground = Brushes.White;
                text_into_shapes.Background = Brushes.Transparent;

                polyline.Stroke = Brushes.White;
                polyline.Fill = Brushes.Transparent;

                polyline.MouseUp += IntoCanvasUp;

                CanvasPos.Children.Add(polyline);
                CanvasPos.Children.Add(text_into_shapes);

                Canvas.SetLeft(polyline, e.GetPosition(CanvasPos).X - lastPoint.X);
                Canvas.SetTop(polyline, e.GetPosition(CanvasPos).Y - lastPoint.Y);

                Canvas.SetLeft(text_into_shapes, Canvas.GetLeft(polyline) + 25);
                Canvas.SetTop(text_into_shapes, Canvas.GetTop(polyline) - 1);

                RhombAdd(polyline, text_into_shapes, rhombN, rhombW, rhombS, rhombE);
            }

            if (Cycle_Check)
            {
                Cycle_Check = false;

                Polygon polyline = new Polygon();
                TextBox text_into_shapes = new TextBox();

                //Индексация фигур              
                polyline.Name = "Cycle_" + shapesCounter.ToString();
                shapesCounter++;


                Parrabullem_Check = false;
                polyline.Points = Cycle.Points;

                text_into_shapes.Text = "Text";
                text_into_shapes.MinWidth = 40;
                text_into_shapes.MinHeight = 20;
                text_into_shapes.FontSize = 10;
                text_into_shapes.BorderBrush = Brushes.Transparent;
                text_into_shapes.Foreground = Brushes.White;
                text_into_shapes.Background = Brushes.Transparent;

                polyline.Stroke = Brushes.White;
                polyline.Fill = Brushes.Transparent;

                polyline.MouseUp += IntoCanvasUp;

                CanvasPos.Children.Add(polyline);
                CanvasPos.Children.Add(text_into_shapes);

                var text_position_x = Math.Abs(Math.Sqrt(Math.Pow(cycleNW.X, 2) + Math.Pow(cycleNW.Y, 2)) - Math.Sqrt(Math.Pow(cycleNE.X, 2) + Math.Pow(cycleNE.Y, 2))) / 2;
                var text_position_y = Math.Abs(Math.Sqrt(Math.Pow(cycleNW.X, 2) + Math.Pow(cycleNW.Y, 2)) - Math.Sqrt(Math.Pow(cycleSW.X, 2) + Math.Pow(cycleSW.Y, 2))) / 2;

                Canvas.SetLeft(polyline, e.GetPosition(CanvasPos).X - lastPoint.X);
                Canvas.SetTop(polyline, e.GetPosition(CanvasPos).Y - lastPoint.Y);

                Canvas.SetLeft(text_into_shapes, Canvas.GetLeft(polyline) + text_position_x - 10);
                Canvas.SetTop(text_into_shapes, Canvas.GetTop(polyline) + text_position_y - 5);

                CycleAdd(polyline, text_into_shapes, cycleNW, cycleW, cycleSW, cycleSE, cycleE, cycleNE);
            }
            if (Ellipse_Check)
            {
                Ellipse_Check = false;

                Rectangle rectangle = new Rectangle();
                TextBox text_into_shapes = new TextBox();

                //Индексация фигур
                rectangle.Name = "Ellipse_" + shapesCounter.ToString();
                shapesCounter++;

                rectangle.Width = Ellipse.Width;
                rectangle.Height = Ellipse.Height;
                rectangle.RadiusX = Ellipse.RadiusX;
                rectangle.RadiusY = Ellipse.RadiusY;
                rectangle.Fill = Brushes.Transparent;
                rectangle.Stroke = Brushes.White;

                text_into_shapes.Text = "Text";
                text_into_shapes.MinWidth = 40;
                text_into_shapes.MinHeight = 20;
                text_into_shapes.FontSize = 10;
                text_into_shapes.BorderBrush = Brushes.Transparent;
                text_into_shapes.Foreground = Brushes.White;
                text_into_shapes.Background = Brushes.Transparent;

                rectangle.MouseUp += IntoCanvasUp;

                CanvasPos.Children.Add(rectangle);
                CanvasPos.Children.Add(text_into_shapes);

                Canvas.SetLeft(rectangle, e.GetPosition(CanvasPos).X - lastPoint.X);
                Canvas.SetTop(rectangle, e.GetPosition(CanvasPos).Y - lastPoint.Y);

                Canvas.SetLeft(text_into_shapes, Canvas.GetLeft(rectangle) + rectangle.Width / 2 - 15);
                Canvas.SetTop(text_into_shapes, Canvas.GetTop(rectangle) + rectangle.Height / 2 - 10);

                EllipseAdd(rectangle, text_into_shapes, rectangle.Width, rectangle.Height);
            }
        }

        private void Clear_Mouse_Enter(object sender, RoutedEventArgs e)
        {

            ((Label)sender).Foreground = Brushes.White;
        }
        private void Clear_Mouse_Leave(object sender, RoutedEventArgs e)
        {
            ((Label)sender).Foreground = Brushes.Gray;
        }
        private void inTrash(object sender, RoutedEventArgs e)
        {
            CanvasPos.Children.Clear();
            OldDiagramm = diagramm;
            diagramm = new Diagramm();
            shapesCounter = 0;
        }
        #endregion

        private void DownLoad(object sender, MouseButtonEventArgs e)
        {
           
            foreach (Block block in OldDiagramm.blocks)
            {
                if (block.Shape == Shapes.Rekt)
                {
                    Point rectangleNW = block.NW;
                    Point rectangleSW = block.SW;
                    Point rectangleSE = block.SE;
                    Point rectnagleNE = block.NE;

                    PointCollection RectPoints = new PointCollection();
                    RectPoints.Add(rectangleNW);
                    RectPoints.Add(rectangleSW);
                    RectPoints.Add(rectangleSE);
                    RectPoints.Add(rectnagleNE);
                    Polygon polyline = new Polygon();
                    TextBox text_into_shapes = new TextBox();
                    text_into_shapes.Text = block.TextIntoTextBox;
                    //Индексация фигур              
                    polyline.Name = "Rect_" + shapesCounter.ToString();
                    shapesCounter++;

                    polyline.Points = RectPoints;
                    text_into_shapes.MinWidth = 40;
                    text_into_shapes.MinHeight = 20;
                    text_into_shapes.FontSize = 10;
                    text_into_shapes.BorderBrush = Brushes.Transparent;
                    text_into_shapes.Foreground = Brushes.White;
                    text_into_shapes.Background = Brushes.Transparent;

                    polyline.Stroke = Brushes.White;
                    polyline.Fill = Brushes.Transparent;

                    polyline.MouseUp += IntoCanvasUp;

                    var text_position_x = Math.Abs(Math.Sqrt((Math.Pow(rectangleNW.X, 2) + Math.Pow(rectangleNW.Y, 2))) - (Math.Sqrt(Math.Pow(rectnagleNE.X, 2) + Math.Pow(rectnagleNE.Y, 2)))) / 2;
                    var text_position_y = Math.Abs(Math.Sqrt((Math.Pow(rectangleNW.X, 2) + Math.Pow(rectangleNW.Y, 2))) - (Math.Sqrt(Math.Pow(rectangleSW.X, 2) + Math.Pow(rectangleSW.Y, 2)))) / 2;

                    CanvasPos.Children.Add(polyline);
                    CanvasPos.Children.Add(text_into_shapes);

                    Canvas.SetLeft(polyline, block.LeftTop.X);
                    Canvas.SetTop(polyline, block.LeftTop.Y);

                    Canvas.SetLeft(text_into_shapes, Canvas.GetLeft(polyline) + text_position_x - 10);
                    Canvas.SetTop(text_into_shapes, Canvas.GetTop(polyline) + text_position_y - 5);

                    RectangleAdd(polyline, text_into_shapes, rectangleNW, rectangleSE, rectangleSW, rectnagleNE);
                }
                if (block.Shape == Shapes.Parrabellum)
                {
                    Point parabellumNW = block.NW;
                    Point parabellumSW = block.SW;
                    Point parabellumSE = block.SE;
                    Point parabellumNE = block.NE;

                    PointCollection parabellumPoints = new PointCollection();
                    parabellumPoints.Add(parabellumNW);
                    parabellumPoints.Add(parabellumSW);
                    parabellumPoints.Add(parabellumSE);
                    parabellumPoints.Add(parabellumNE);

                    Polygon polyline = new Polygon();
                    TextBox text_into_shapes = new TextBox();

                    //Индексация фигур              
                    polyline.Name = "Parrabullem_" + shapesCounter.ToString();
                    shapesCounter++;

                    polyline.Points = parabellumPoints;

                    text_into_shapes.Text = block.TextIntoTextBox;
                    text_into_shapes.MinWidth = 40;
                    text_into_shapes.MinHeight = 20;
                    text_into_shapes.FontSize = 10;
                    text_into_shapes.BorderBrush = Brushes.Transparent;
                    text_into_shapes.Foreground = Brushes.White;
                    text_into_shapes.Background = Brushes.Transparent;

                    polyline.Stroke = Brushes.White;
                    polyline.Fill = Brushes.Transparent;

                    polyline.MouseUp += IntoCanvasUp;

                    CanvasPos.Children.Add(polyline);
                    CanvasPos.Children.Add(text_into_shapes);

                    var text_position_x = Math.Abs((Math.Sqrt(Math.Pow(parabellumNW.X, 2) + Math.Pow(parabellumNW.Y, 2))) - (Math.Sqrt(Math.Pow(parabellumNE.X, 2) + Math.Pow(parabellumNE.Y, 2)))) / 2;
                    var text_position_y = Math.Abs(Math.Sqrt(Math.Pow(parabellumNW.X, 2) + Math.Pow(parabellumNW.Y, 2)) - Math.Sqrt(Math.Pow(parabellumSW.X, 2) + Math.Pow(parabellumSW.Y, 2))) / 2;

                    Canvas.SetLeft(polyline, block.LeftTop.X);
                    Canvas.SetTop(polyline, block.LeftTop.Y);

                    Canvas.SetLeft(text_into_shapes, Canvas.GetLeft(polyline) + text_position_x - 13);
                    Canvas.SetTop(text_into_shapes, Canvas.GetTop(polyline) + text_position_y - 2);

                    ParrabellumAdd(polyline, text_into_shapes, parabellumNW, parabellumSW, parabellumSE, parabellumNE);

                }
                if (block.Shape == Shapes.Rhomb)
                {
                    Point rhombN = block.NW;
                    Point rhombW = block.NE;
                    Point rhombS = block.SW;
                    Point rhombE = block.SE;

                    PointCollection rhombPoints = new PointCollection();
                    rhombPoints.Add(rhombN);
                    rhombPoints.Add(rhombW);
                    rhombPoints.Add(rhombS);
                    rhombPoints.Add(rhombE);

                    Polygon polyline = new Polygon();
                    TextBox text_into_shapes = new TextBox();

                    //Индексация фигур              
                    polyline.Name = "Rhomb_" + shapesCounter.ToString();
                    shapesCounter++;

                    polyline.Points = rhombPoints;

                    text_into_shapes.Text = block.TextIntoTextBox;
                    text_into_shapes.MinWidth = 40;
                    text_into_shapes.MinHeight = 20;
                    text_into_shapes.FontSize = 10;
                    text_into_shapes.BorderBrush = Brushes.Transparent;
                    text_into_shapes.Foreground = Brushes.White;
                    text_into_shapes.Background = Brushes.Transparent;

                    polyline.Stroke = Brushes.White;
                    polyline.Fill = Brushes.Transparent;

                    polyline.MouseUp += IntoCanvasUp;

                    CanvasPos.Children.Add(polyline);
                    CanvasPos.Children.Add(text_into_shapes);

                    Canvas.SetLeft(polyline, block.LeftTop.X);
                    Canvas.SetTop(polyline, block.LeftTop.Y);

                    Canvas.SetLeft(text_into_shapes, Canvas.GetLeft(polyline) + 25);
                    Canvas.SetTop(text_into_shapes, Canvas.GetTop(polyline) - 1);

                    RhombAdd(polyline, text_into_shapes, rhombN, rhombW, rhombS, rhombE);
                }
                if (block.Shape == Shapes.Cycle)
                {
                    Point cycleNW = block.NW;
                    Point cycleW = block.AddPoint1;
                    Point cycleSW = block.SW;
                    Point cycleSE = block.SE;
                    Point cycleE = block.AddPoint2;
                    Point cycleNE = block.NE;
                    PointCollection cyclePoints = new PointCollection()
                    {
                        cycleNW,
                        cycleW,
                        cycleSW,
                        cycleSE,
                        cycleE,
                        cycleNE
                    };

                    Polygon polyline = new Polygon();
                    TextBox text_into_shapes = new TextBox();

                    //Индексация фигур              
                    polyline.Name = "Cycle_" + shapesCounter.ToString();
                    shapesCounter++;

                    polyline.Points = cyclePoints;

                    text_into_shapes.Text = block.TextIntoTextBox;
                    text_into_shapes.MinWidth = 40;
                    text_into_shapes.MinHeight = 20;
                    text_into_shapes.FontSize = 10;
                    text_into_shapes.BorderBrush = Brushes.Transparent;
                    text_into_shapes.Foreground = Brushes.White;
                    text_into_shapes.Background = Brushes.Transparent;

                    polyline.Stroke = Brushes.White;
                    polyline.Fill = Brushes.Transparent;

                    polyline.MouseUp += IntoCanvasUp;

                    CanvasPos.Children.Add(polyline);
                    CanvasPos.Children.Add(text_into_shapes);

                    var text_position_x = Math.Abs(Math.Sqrt(Math.Pow(cycleNW.X, 2) + Math.Pow(cycleNW.Y, 2)) - Math.Sqrt(Math.Pow(cycleNE.X, 2) + Math.Pow(cycleNE.Y, 2))) / 2;
                    var text_position_y = Math.Abs(Math.Sqrt(Math.Pow(cycleNW.X, 2) + Math.Pow(cycleNW.Y, 2)) - Math.Sqrt(Math.Pow(cycleSW.X, 2) + Math.Pow(cycleSW.Y, 2))) / 2;

                    Canvas.SetLeft(polyline, block.LeftTop.X);
                    Canvas.SetTop(polyline, block.LeftTop.Y);

                    Canvas.SetLeft(text_into_shapes, Canvas.GetLeft(polyline) + text_position_x - 10);
                    Canvas.SetTop(text_into_shapes, Canvas.GetTop(polyline) + text_position_y - 5);

                    CycleAdd(polyline, text_into_shapes, cycleNW, cycleW, cycleSW, cycleSE, cycleE, cycleNE);
                }
                if (block.Shape == Shapes.Ellipse)
                {
                    Rectangle rectangle = new Rectangle();
                    TextBox text_into_shapes = new TextBox();

                    //Индексация фигур
                    rectangle.Name = "Ellipse_" + shapesCounter.ToString();
                    shapesCounter++;

                    rectangle.Width = block.Width;
                    rectangle.Height = block.Height;
                    rectangle.RadiusX = Ellipse.RadiusX;
                    rectangle.RadiusY = Ellipse.RadiusY;
                    rectangle.Fill = Brushes.Transparent;
                    rectangle.Stroke = Brushes.White;

                    text_into_shapes.Text = block.TextIntoTextBox;
                    text_into_shapes.MinWidth = 40;
                    text_into_shapes.MinHeight = 20;
                    text_into_shapes.FontSize = 10;
                    text_into_shapes.BorderBrush = Brushes.Transparent;
                    text_into_shapes.Foreground = Brushes.White;
                    text_into_shapes.Background = Brushes.Transparent;

                    rectangle.MouseUp += IntoCanvasUp;

                    CanvasPos.Children.Add(rectangle);
                    CanvasPos.Children.Add(text_into_shapes);

                    Canvas.SetLeft(rectangle, block.LeftTop.X);
                    Canvas.SetTop(rectangle, block.LeftTop.Y);

                    Canvas.SetLeft(text_into_shapes, Canvas.GetLeft(rectangle) + rectangle.Width / 2 - 15);
                    Canvas.SetTop(text_into_shapes, Canvas.GetTop(rectangle) + rectangle.Height / 2 - 10);

                    EllipseAdd(rectangle, text_into_shapes, rectangle.Width, rectangle.Height);
                }
            }
        }
    }
}
