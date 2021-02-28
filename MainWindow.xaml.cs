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
        #endregion

        #region Show Window with Settings
        private void Change_Window(object sender, RoutedEventArgs e)
        {
            Window1 CW = new Window1();
            CW.Show();
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
            if(!Ellipse_Check)
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

        private void inTrash(object sender, RoutedEventArgs e)
        {
            CanvasPos.Children.Clear();
        }
        #endregion
    }
}
