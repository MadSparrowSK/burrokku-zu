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

        private bool light = false;

        public MainWindow()
        {
            InitializeComponent();
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
            ((Label)sender).Background = Brushes.Gray;
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
            if (lbl_RWL == sender)
                RWL_Main.Stroke = Brushes.White;
        }
        private void SideBar_Mouse_Leave(Object sender, RoutedEventArgs e)
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
            if (lbl_RWL == sender)
                RWL_Main.Stroke = Brushes.Gray;
        }
        private void Switch_Light(object sender, RoutedEventArgs e)
        {

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

        /*private enum HitType
        {
            None, Body, UL, UR, LR, LL, L, R, B, T
        };

        HitType MouseHitType = HitType.None;
        private bool DragInProgress = false;
        private Point LastPoint;

        private HitType SetHitType(Polyline polyline, Point point)
        {
            double left = Canvas.GetLeft(polyline);
            double top = Canvas.GetTop(polyline);
            double right = Canvas.GetRight(polyline);
            double bottom = Canvas.GetBottom(polyline);

            if (point.X < left)   return HitType.None;
            if (point.X > right)  return HitType.None;
            if (point.Y < top)    return HitType.None;
            if (point.Y > bottom) return HitType.None;

            const double GAP = 10;
            if(point.X - left < GAP)
            {
                if (point.Y - top < GAP) return HitType.UL;
                if (bottom - point.Y < GAP) return HitType.LL;
                return HitType.L;
            }
            else if(right - point.X < GAP) 
            {
                if (point.Y - top < GAP) return HitType.UR;
                if (bottom - point.Y < GAP) return HitType.LR;
                return HitType.R;
            }
            if (point.Y - top < GAP) return HitType.T;
            if (bottom - point.Y < GAP) return HitType.B;
            return HitType.Body;
        }
        private void SetMouseCursor()
        {
            Cursor desired_cursor = Cursors.Arrow;

            switch(MouseHitType)
            {
                case HitType.None: desired_cursor = Cursors.Arrow; break;
                case HitType.Body: desired_cursor = Cursors.ScrollAll; break;
                case HitType.UL:
                case HitType.LR: desired_cursor = Cursors.SizeNESW; break;
                case HitType.T:
                case HitType.B: desired_cursor = Cursors.SizeNS; break;
                case HitType.L:
                case HitType.R: desired_cursor = Cursors.SizeWE; break;
            }

            if (Cursor != desired_cursor) Cursor = desired_cursor;
        }
        private void Test_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var obj = (Polyline)sender;
            MouseHitType = SetHitType(obj, Mouse.GetPosition(CanvasPos));
            SetMouseCursor();
            if (MouseHitType == HitType.None) return;

            LastPoint = Mouse.GetPosition(CanvasPos);
            DragInProgress = true;
        }
        private void Test_MouseMove(object sender, MouseEventArgs e)
        {
            var obj = (Polyline)sender;
            if (DragInProgress)
            {
                // See how much the mouse has moved.
                Point point = Mouse.GetPosition(CanvasPos);
                double offset_x = point.X - LastPoint.X;
                double offset_y = point.Y - LastPoint.Y;

                // Get the rectangle's current position.
                double new_x = Canvas.GetLeft(obj);
                double new_y = Canvas.GetTop(obj);
                double new_width = obj.Width;
                double new_height = obj.Height;

                // Update the rectangle.
                switch (MouseHitType)
                {
                    case HitType.Body:
                        new_x += offset_x;
                        new_y += offset_y;
                        break;
                    case HitType.UL:
                        new_x += offset_x;
                        new_y += offset_y;
                        new_width -= offset_x;
                        new_height -= offset_y;
                        break;
                    case HitType.UR:
                        new_y += offset_y;
                        new_width += offset_x;
                        new_height -= offset_y;
                        break;
                    case HitType.LR:
                        new_width += offset_x;
                        new_height += offset_y;
                        break;
                    case HitType.LL:
                        new_x += offset_x;
                        new_width -= offset_x;
                        new_height += offset_y;
                        break;
                    case HitType.L:
                        new_x += offset_x;
                        new_width -= offset_x;
                        break;
                    case HitType.R:
                        new_width += offset_x;
                        break;
                    case HitType.B:
                        new_height += offset_y;
                        break;
                    case HitType.T:
                        new_y += offset_y;
                        new_height -= offset_y;
                        break;
                }

                // Don't use negative width or height.
                if ((new_width > 0) && (new_height > 0))
                {
                    // Update the rectangle.
                    Canvas.SetLeft(obj, new_x);
                    Canvas.SetTop(obj, new_y);
                    obj.Width = new_width;
                    obj.Height = new_height;

                    // Save the mouse's new location.
                    LastPoint = point;
                }
            }
            else
            {
                MouseHitType = SetHitType(obj,
                    Mouse.GetPosition(CanvasPos));
                SetMouseCursor();
            }
        }
        private void Test_MouseUp(object sender, MouseButtonEventArgs e)
        {
            DragInProgress = false;
        }*/



        private bool RWL_Check = false;
        private bool Rectangle_Check = false;
        private bool Parrabullem_Check = false;
        private bool Rhomb_Check = false;
        private bool Cycle_Check = false;
        private bool Ellipse_Check = false;
        private void DragDrop_MD(object sender, MouseButtonEventArgs e)
        {
            if (RWL_Main == sender)
                RWL_Check = true;
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

            DragDrop.DoDragDrop(this, this, DragDropEffects.Copy);
        }

        #region Interaction_With_Shapes_Into_Canvas
        private void IntoCanvasDown(object sender, MouseButtonEventArgs e)
        {
            var smt = (UIElement)sender;
            smt.CaptureMouse();
        }
        private void IntoCanvasMove(object sender, MouseEventArgs e)
        {
            if(e.LeftButton == MouseButtonState.Pressed)
            {
                var smt = (UIElement)sender;
                var pos = e.GetPosition(CanvasPos);

                Canvas.SetLeft(smt, pos.X);
                Canvas.SetTop(smt, pos.Y);
            }
        }
        private void IntoCanvasUp(object sender, MouseButtonEventArgs e)
        {
            var smt = (UIElement)sender;
            smt.ReleaseMouseCapture();
        }
        #endregion

        private void DnD_Drop(object sender, DragEventArgs e)
        {
            if(!light)
            {
                if (!Ellipse_Check)
                {
                    Polyline polyline = new Polyline();
                    polyline.Stroke = Brushes.White;
                    polyline.Fill = Brushes.Transparent;
                    polyline.StrokeThickness = 1.5;

                    polyline.MouseMove += new MouseEventHandler(IntoCanvasMove);
                    polyline.MouseDown += new MouseButtonEventHandler(IntoCanvasDown);
                    polyline.MouseUp += new MouseButtonEventHandler(IntoCanvasUp);

                    var pos = e.GetPosition(CanvasPos);
                    CanvasPos.Children.Add(polyline);

                    if (RWL_Check)
                    {
                        polyline.Points = RWL_Main.Points;
                        RWL_Check = false;
                    }
                    if (Rectangle_Check)
                    {
                        polyline.Points = Rekt.Points;
                        Rectangle_Check = false;
                    }
                    if (Cycle_Check)
                    {
                        polyline.Points = Cycle.Points;
                        Cycle_Check = false;
                    }
                    if (Rhomb_Check)
                    {
                        polyline.Points = Rhomb.Points;
                        Rhomb_Check = false;
                    }
                    if (Parrabullem_Check)
                    {
                        polyline.Points = Parrabellum.Points;
                        Parrabullem_Check = false;
                    }

                    Canvas.SetLeft(polyline, pos.X);
                    Canvas.SetTop(polyline, pos.Y);
                }
                else
                {
                    Ellipse_Check = false;

                    Rectangle rekt = new Rectangle();
                    rekt.Width = Ellipse.Width;
                    rekt.Height = Ellipse.Height;
                    rekt.Fill = Brushes.Transparent;
                    rekt.Stroke = Brushes.White;
                    rekt.StrokeThickness = 1.5;
                    rekt.RadiusX = Ellipse.RadiusX;
                    rekt.RadiusY = Ellipse.RadiusY;

                    rekt.MouseMove += new MouseEventHandler(IntoCanvasMove);
                    rekt.MouseDown += new MouseButtonEventHandler(IntoCanvasDown);
                    rekt.MouseUp += new MouseButtonEventHandler(IntoCanvasUp);

                    var pos = e.GetPosition(CanvasPos);
                    CanvasPos.Children.Add(rekt);

                    Canvas.SetLeft(rekt, pos.X);
                    Canvas.SetTop(rekt, pos.Y);
                }
            }
            else
            {
                if (!Ellipse_Check)
                {
                    Polyline polyline = new Polyline();
                    polyline.Stroke = Brushes.Black;
                    polyline.Fill = Brushes.Transparent;
                    polyline.StrokeThickness = 1.5;

                    polyline.MouseMove += new MouseEventHandler(IntoCanvasMove);
                    polyline.MouseDown += new MouseButtonEventHandler(IntoCanvasDown);
                    polyline.MouseUp += new MouseButtonEventHandler(IntoCanvasUp);

                    var pos = e.GetPosition(CanvasPos);
                    CanvasPos.Children.Add(polyline);

                    if (RWL_Check)
                    {
                        polyline.Points = RWL_Main.Points;
                        RWL_Check = false;
                    }
                    if (Rectangle_Check)
                    {
                        polyline.Points = Rekt.Points;
                        Rectangle_Check = false;
                    }
                    if (Cycle_Check)
                    {
                        polyline.Points = Cycle.Points;
                        Cycle_Check = false;
                    }
                    if (Rhomb_Check)
                    {
                        polyline.Points = Rhomb.Points;
                        Rhomb_Check = false;
                    }
                    if (Parrabullem_Check)
                    {
                        polyline.Points = Parrabellum.Points;
                        Parrabullem_Check = false;
                    }

                    Canvas.SetLeft(polyline, pos.X);
                    Canvas.SetTop(polyline, pos.Y);
                }
                else
                {
                    Ellipse_Check = false;

                    Rectangle rekt = new Rectangle();
                    rekt.Width = Ellipse.Width;
                    rekt.Height = Ellipse.Height;
                    rekt.Fill = Brushes.Transparent;
                    rekt.Stroke = Brushes.Black;
                    rekt.StrokeThickness = 1.5;
                    rekt.RadiusX = Ellipse.RadiusX;
                    rekt.RadiusY = Ellipse.RadiusY;

                    rekt.MouseMove += new MouseEventHandler(IntoCanvasMove);
                    rekt.MouseDown += new MouseButtonEventHandler(IntoCanvasDown);
                    rekt.MouseUp += new MouseButtonEventHandler(IntoCanvasUp);

                    var pos = e.GetPosition(CanvasPos);
                    CanvasPos.Children.Add(rekt);

                    Canvas.SetLeft(rekt, pos.X);
                    Canvas.SetTop(rekt, pos.Y);
                }
            }
        }


        private void Clear_Mouse_Enter(object sender, RoutedEventArgs e)
        {
            if(!light)
            {
                Clear.Foreground = Brushes.White;
                Clear.Background = new SolidColorBrush(Colors.Purple) { Opacity = 0.5 };
            }
            else
            {
                Clear.Foreground = Brushes.Black;
                Clear.Background = new SolidColorBrush(Colors.Purple) { Opacity = 0.5 };
            }
        }
        private void Clear_Mouse_Leave(object sender, RoutedEventArgs e)
        {
            Clear.Foreground = Brushes.Gray;
            Clear.Background = Brushes.Transparent;
        }
        private void inTrash(object sender, RoutedEventArgs e)
        {
            CanvasPos.Children.Clear();
        }
        #endregion
    }
}
