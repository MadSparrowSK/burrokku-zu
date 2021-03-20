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

        public MainWindow()
        {
            InitializeComponent();

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
            cycleW.X = 1;
            cycleW.Y = 8;

            cycleSW.X = 7;
            cycleSW.Y = 20;

            cycleSE.X = 60;
            cycleSE.Y = 20;

            cycleE.X = 67;
            cycleE.Y = 8;

            cycleNE.X = 60;
            cycleNE.Y = -6;

            cycleNW.X = 7;
            cycleNW.Y = -6;
            #endregion

            #endregion
        }

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
            ((Label)sender).Background = new SolidColorBrush(Colors.Gray) { Opacity = 0};
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

        private bool Rectangle_Check = false;
        private bool Parrabullem_Check = false;
        private bool Rhomb_Check = false;
        private bool Cycle_Check = false;
        private bool Ellipse_Check = false;

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

            var smt = (UIElement)sender;
            lastPoint = e.GetPosition(smt);

            DragDrop.DoDragDrop(this, this, DragDropEffects.Copy);
        }

        #region Interaction_With_Shapes_Into_Canvas

        Polyline anchor_size;

        Point lastPoint;
        Point lastPoint_anchor;
        Point current_anchor_postion;

        bool is_anchor_create = false;

        private void RectangleAdd(Polygon polygon, TextBox txt)
        {
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
                }
                #endregion

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

                            var anchor = (UIElement)sndr;
                            anchor.CaptureMouse();

                            var pos = e.GetPosition(CanvasPos) - lastPoint_anchor;

                            if (pos.X < current_anchor_postion.X && pos.Y < current_anchor_postion.Y)
                            {
                                //text_into_shapes.MaxHeight += .3;
                                //text_into_shapes.MaxWidth += .7;

                                //++fisrt.X;
                                //++fisrt.Y;

                                //++second.X;
                                rectangleSW.Y += .5;

                                ++rectangleSE.X;
                                rectangleSE.Y += .5;

                                ++rectnagleNE.X;
                                //++fourth.Y;

                                PointCollection points = new PointCollection();
                                points.Add(rectangleNW);
                                points.Add(rectangleSW);
                                points.Add(rectangleSE);
                                points.Add(rectnagleNE);

                                smt.Points = points;

                            }
                            else
                            {
                                //text_into_shapes.MaxHeight -= .3;
                                //text_into_shapes.MaxWidth -= .7;

                                //++fisrt.X;
                                //++fisrt.Y;

                                //++second.X;
                                rectangleSW.Y -= .5;

                                --rectangleSE.X;
                                rectangleSE.Y -= .5;

                                --rectnagleNE.X;
                                //++fourth.Y;

                                PointCollection points = new PointCollection();
                                points.Add(rectangleNW);
                                points.Add(rectangleSW);
                                points.Add(rectangleSE);
                                points.Add(rectnagleNE);

                                smt.Points = points;
                            }

                            Canvas.SetLeft(anchor, pos.X);
                            Canvas.SetTop(anchor, pos.Y);
                        }
                    }
                    void AnchorMouseUp(object sndr, MouseButtonEventArgs evnt)
                    {
                        var anchor = (UIElement)sndr;
                        anchor.ReleaseMouseCapture();
                    }

                    #endregion

                    CanvasPos.Children.Add(anchor_size);

                    Canvas.SetLeft(anchor_size, Canvas.GetLeft(smt));
                    Canvas.SetTop(anchor_size, Canvas.GetTop(smt));

                    is_anchor_create = true;
                }
                #endregion

                REPCIntoCanvasMouseMove(polygon, txt);
            }
        }
        private void ParrabellumAdd(Polygon polygon, TextBox txt)
        {
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
                }
                #endregion

                #region anchor
                if (!is_anchor_create)
                {

                    anchor_size = new Polyline();
                    anchor_size.Points = Anchor.Points;
                    anchor_size.Stroke = Brushes.Red;
                    anchor_size.Fill = Brushes.Transparent;

                    anchor_size.MouseDown += AnchorMouseDown;
                    anchor_size.MouseMove += AnchorMouseMove;
                    anchor_size.MouseUp  += AnchorMouseUp;

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

                            var anchor = (UIElement)sndr;
                            anchor.CaptureMouse();

                            var pos = e.GetPosition(CanvasPos) - lastPoint_anchor;

                            if (pos.X < current_anchor_postion.X && pos.Y < current_anchor_postion.Y)
                            {
                                //++fisrt.X;
                                //++fisrt.Y;

                                //++second.X;
                                parabellumSW.Y += .5;

                                ++parabellumSE.X;
                                parabellumSE.Y += .5;

                                ++parabellumNE.X;
                                //++fourth.Y;

                                PointCollection points = new PointCollection();
                                points.Add(parabellumNW);
                                points.Add(parabellumSW);
                                points.Add(parabellumSE);
                                points.Add(parabellumNE);

                                smt.Points = points;

                            }
                            else
                            {
                                //++fisrt.X;
                                //++fisrt.Y;

                                //++second.X;
                                parabellumSW.Y -= .5;

                                --parabellumSE.X;
                                parabellumSE.Y -= .5;

                                --parabellumNE.X;
                                //++fourth.Y;

                                PointCollection points = new PointCollection();
                                points.Add(parabellumNW);
                                points.Add(parabellumSW);
                                points.Add(parabellumSE);
                                points.Add(parabellumNE);

                                smt.Points = points;
                            }

                            Canvas.SetLeft(anchor, pos.X);
                            Canvas.SetTop(anchor, pos.Y);
                        }
                    }
                    void AnchorMouseUp(object sndr, MouseButtonEventArgs evnt)
                    {
                        var anchor = (UIElement)sndr;
                        anchor.ReleaseMouseCapture();
                    }

                    #endregion

                    CanvasPos.Children.Add(anchor_size);

                    Canvas.SetLeft(anchor_size, Canvas.GetLeft(smt));
                    Canvas.SetTop(anchor_size, Canvas.GetTop(smt));

                    is_anchor_create = true;
                }
                #endregion

                REPCIntoCanvasMouseMove(polygon, txt);
            }
        }
        private void RhombAdd(Polygon polygon, TextBox txt)
        {
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
                    anchor_size.MouseUp   += AnchorMouseUp;

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

                            var anchor = (UIElement)sndr;
                            anchor.CaptureMouse();

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

                            }
                            else
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
                            }

                            Canvas.SetLeft(anchor, pos.X);
                            Canvas.SetTop(anchor, pos.Y);
                        }
                    }
                    void AnchorMouseUp(object sndr, MouseButtonEventArgs evnt)
                    {
                        var anchor = (UIElement)sndr;
                        anchor.ReleaseMouseCapture();
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
                }
                #endregion

                RhombIntoCanvasMouseMove(polygon, txt);
            }
        } 
        private void CycleAdd(Polygon polygon, TextBox txt)
        {
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

                            var anchor = (UIElement)sndr;
                            anchor.CaptureMouse();

                            var pos = e.GetPosition(CanvasPos) - lastPoint_anchor;

                            if (pos.X < current_anchor_postion.X && pos.Y < current_anchor_postion.Y)
                            {
                                cycleW.X -= .2;

                                cycleSW.Y += .5;

                                cycleSE.X += 1;
                                cycleSE.Y += .5;

                                cycleE.X += 1.2;

                                cycleNE.X += 1;
                                cycleNE.Y -= .5;

                                cycleNW.Y -= .5;

                                PointCollection points = new PointCollection();
                                points.Add(cycleW);
                                points.Add(cycleSW);
                                points.Add(cycleSE);
                                points.Add(cycleE);
                                points.Add(cycleNE);
                                points.Add(cycleNW);

                                smt.Points = points;
                            }
                            else
                            {
                                cycleW.X += .2;

                                cycleSW.Y -= .5;

                                cycleSE.X -= 1;
                                cycleSE.Y -= .5;

                                cycleE.X -= 1.2;

                                cycleNE.X -= 1;
                                cycleNE.Y += .5;

                                cycleNW.Y += .5;

                                PointCollection points = new PointCollection();
                                points.Add(cycleW);
                                points.Add(cycleSW);
                                points.Add(cycleSE);
                                points.Add(cycleE);
                                points.Add(cycleNE);
                                points.Add(cycleNW);

                                smt.Points = points;
                            }

                            Canvas.SetLeft(anchor, pos.X);
                            Canvas.SetTop(anchor, pos.Y);
                        }
                    }
                    void AnchorMouseUp(object sndr, MouseButtonEventArgs evnt)
                    {
                        var anchor = (UIElement)sndr;
                        anchor.ReleaseMouseCapture();
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
                }
                #endregion

                REPCIntoCanvasMouseMove(polygon, txt);
            }
        }
        private void EllipseAdd(Polygon polygon, TextBox txt)
        {
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
                    anchor_size.MouseUp   += AnchorMouseUp;

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
                            var anchor = (UIElement)sndr;
                            anchor.CaptureMouse();

                            var pos = e.GetPosition(CanvasPos) - lastPoint_anchor;

                            if (pos.X < current_anchor_postion.X && pos.Y < current_anchor_postion.Y)
                            {
                                smt.Width += 1;
                                smt.Height += 1;
                            }
                            else if (smt.Width > 0 && smt.Height > 0)
                            {
                                smt.Width -= 1;
                                smt.Height -= 1;
                            }

                            Canvas.SetLeft(anchor, pos.X);
                            Canvas.SetTop(anchor, pos.Y);
                        }
                    }
                    void AnchorMouseUp(object sndr, MouseButtonEventArgs evnt)
                    {
                        var anchor = (UIElement)sndr;
                        anchor.ReleaseMouseCapture();
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
                }
                #endregion

                REPCIntoCanvasMouseMove(polygon, txt);
            }
        }


        private void REPCIntoCanvasMouseMove(Polygon polygon, TextBox txt)
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

                    Canvas.SetLeft(txt, Canvas.GetLeft(smt) + 10);
                    Canvas.SetTop(txt, Canvas.GetTop(smt) + 5);
                }
            }
        }
        private void RhombIntoCanvasMouseMove(Polygon polygon, TextBox txt)
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

                    Canvas.SetLeft(txt, Canvas.GetLeft(smt) + 10);
                    Canvas.SetTop(txt, Canvas.GetTop(smt) + 5);
                }
            }
        }
        

        public void IntoCanvasUp(object sender, MouseButtonEventArgs e)
        {
            var smt = (UIElement)sender;
            smt.ReleaseMouseCapture();
        }
        #endregion

        private void DnD_Drop(object sender, DragEventArgs e)
        { 
            if(Rectangle_Check)
            {
                Polygon polyline = new Polygon();
                TextBox text_into_shapes = new TextBox();

                Rectangle_Check = false;
                polyline.Points = Rekt.Points;

                text_into_shapes.Text = "Text";
                text_into_shapes.MaxWidth = 40;
                text_into_shapes.MaxHeight = 20;
                text_into_shapes.TextWrapping = TextWrapping.Wrap;
                text_into_shapes.FontSize = 10;
                text_into_shapes.BorderBrush = Brushes.Transparent;
                text_into_shapes.Foreground = Brushes.White;
                text_into_shapes.Background = Brushes.Transparent;

                polyline.Stroke = Brushes.White;
                polyline.Fill   = Brushes.Transparent;

                polyline.MouseUp   += IntoCanvasUp;

                CanvasPos.Children.Add(polyline);
                CanvasPos.Children.Add(text_into_shapes);

                Canvas.SetLeft(polyline, e.GetPosition(CanvasPos).X - lastPoint.X);
                Canvas.SetTop(polyline, e.GetPosition(CanvasPos).Y  - lastPoint.Y);

                Canvas.SetLeft(text_into_shapes, Canvas.GetLeft(polyline) + 10);
                Canvas.SetTop(text_into_shapes, Canvas.GetTop(polyline) + 5);

                RectangleAdd(polyline, text_into_shapes);
            }
            if(Parrabullem_Check)
            {
                Polygon polyline = new Polygon();
                TextBox text_into_shapes = new TextBox();

                Parrabullem_Check = false;
                polyline.Points = Parrabellum.Points;

                text_into_shapes.Text = "Text";
                text_into_shapes.MaxWidth = 40;
                text_into_shapes.MaxHeight = 20;
                text_into_shapes.TextWrapping = TextWrapping.Wrap;
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

                Canvas.SetLeft(text_into_shapes, Canvas.GetLeft(polyline) + 10);
                Canvas.SetTop(text_into_shapes, Canvas.GetTop(polyline) + 5);

                ParrabellumAdd(polyline, text_into_shapes);
            }
            if(Rhomb_Check)
            {
                Rhomb_Check = false;

                Polygon polyline = new Polygon();
                TextBox text_into_shapes = new TextBox();

                Parrabullem_Check = false;
                polyline.Points = Rhomb.Points;

                text_into_shapes.Text = "Text";
                text_into_shapes.MaxWidth = 40;
                text_into_shapes.MaxHeight = 20;
                text_into_shapes.TextWrapping = TextWrapping.Wrap;
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

                Canvas.SetLeft(text_into_shapes, Canvas.GetLeft(polyline) + 10);
                Canvas.SetTop(text_into_shapes, Canvas.GetTop(polyline) + 5);

                RhombAdd(polyline, text_into_shapes);
            }
            if(Cycle_Check)
            {
                Cycle_Check = false;

                Polygon polyline = new Polygon();
                TextBox text_into_shapes = new TextBox();

                Parrabullem_Check = false;
                polyline.Points = Cycle.Points;

                text_into_shapes.Text = "Text";
                text_into_shapes.MaxWidth = 40;
                text_into_shapes.MaxHeight = 20;
                text_into_shapes.TextWrapping = TextWrapping.Wrap;
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

                Canvas.SetLeft(text_into_shapes, Canvas.GetLeft(polyline) + 10);
                Canvas.SetTop(text_into_shapes, Canvas.GetTop(polyline) + 5);

                CycleAdd(polyline, text_into_shapes);
            }

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
            cycleW.X = 1;
            cycleW.Y = 8;

            cycleSW.X = 7;
            cycleSW.Y = 20;

            cycleSE.X = 60;
            cycleSE.Y = 20;

            cycleE.X = 67;
            cycleE.Y = 8;

            cycleNE.X = 60;
            cycleNE.Y = -6;

            cycleNW.X = 7;
            cycleNW.Y = -6;
            #endregion

            #endregion
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
            cycleW.X = 1;
            cycleW.Y = 8;

            cycleSW.X = 7;
            cycleSW.Y = 20;

            cycleSE.X = 60;
            cycleSE.Y = 20;

            cycleE.X = 67;
            cycleE.Y = 8;

            cycleNE.X = 60;
            cycleNE.Y = -6;

            cycleNW.X = 7;
            cycleNW.Y = -6;
            #endregion

            #endregion
        }
        #endregion
    }
}
