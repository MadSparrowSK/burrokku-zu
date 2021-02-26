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

        /*private bool CheckRekt = false;
        private bool CheckParall = false;
        private bool CheckEllipse = false;
        private bool CheckFigure = false;
        private bool Check = false;
        private static int ID = 0;*/

        private bool Check = false;

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
            {
                RWL_1.Stroke = Brushes.White;
                RWL_2.Stroke = Brushes.White;
                RWL_3.Stroke = Brushes.White;
            }
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
            {
                RWL_1.Stroke = Brushes.Gray;
                RWL_2.Stroke = Brushes.Gray;
                RWL_3.Stroke = Brushes.Gray;
            }
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

        public static Image global_sender;
        private void TestDnD(object sender, MouseButtonEventArgs e)
        {

            Check = true;
            DataObject data = new DataObject();
            data.SetData("Object", this);

            DragDrop.DoDragDrop(this, data, DragDropEffects.Move);
        }


        private void DragDrop_MD(object sender, MouseButtonEventArgs e)
        {
           /* if (Rekt == sender)
                CheckRekt = true;
            if (Parrabellum == sender)
                CheckParall = true;
            if (Ellipse == sender)
                CheckEllipse = true;
            if (Cycle == sender)
                CheckFigure = true;*/


            DataObject data = new DataObject();
            data.SetData("Object", this);

            DragDrop.DoDragDrop(this, data, DragDropEffects.Copy);
        }

        private void DnD_Drop(object sender, DragEventArgs e)
        {

            if(!Check)
            {
                Polygon polygon = new Polygon();
                polygon.Points = Rekt.Points;
                polygon.Fill = Brushes.Transparent;
                polygon.Stroke = Brushes.White;

                var pos = e.GetPosition(CanvasPos);
                CanvasPos.Children.Add(polygon);

                Canvas.SetLeft(polygon, pos.X);
                Canvas.SetTop(polygon, pos.Y);
            }


            /*rekkkt = new Rectangle();

            rekkkt.Fill = Brushes.Transparent;
            rekkkt.Stroke = Brushes.White;
            rekkkt.Width = Ellipse.Width;
            rekkkt.Height = Ellipse.Height;

            var pos = e.GetPosition(CanvasPos);

            CanvasPos.Children.Add(rekkkt);
            Canvas.SetLeft(rekkkt, pos.X);
            Canvas.SetTop(rekkkt, pos.Y);*/

            /*rekt_copy.Fill = Brushes.Transparent;
            rekt_copy.Stroke = Brushes.White;
            rekt_copy.Width = Ellipse.Width;
            rekt_copy.Height = Ellipse.Height;
            rekt_copy.RadiusX = Ellipse.RadiusX;
            rekt_copy.RadiusY = Ellipse.RadiusY;

            var pos = e.GetPosition(CanvasPos);

            Canvas.SetLeft(rekt_copy, pos.X);
            Canvas.SetTop(rekt_copy, pos.Y);
*/


            /*if (CheckRekt)
            {
                Polygon pol = new Polygon();
                pol.Points = Rekt.Points;
                pol.Stroke = Brushes.White;

                var pos = e.GetPosition(CanvasPos);

                CanvasPos.Children.Add(pol);
                Canvas.SetLeft(pol, pos.X);
                Canvas.SetTop(pol, pos.Y);

                CheckRekt = false;
            }
            if (CheckParall)
            {
                Polygon pol = new Polygon();
                pol.Points = Parrabellum.Points;
                pol.Stroke = Brushes.White;

                var pos = e.GetPosition(CanvasPos);

                CanvasPos.Children.Add(pol);
                Canvas.SetLeft(pol, pos.X);
                Canvas.SetTop(pol, pos.Y);

                CheckParall = false;
            }
            if (CheckEllipse)
            {
                Rectangle rekt = new Rectangle()
                {
                    Width = Ellipse.Width,
                    Height = Ellipse.Height,
                    Stroke = Brushes.White,
                    RadiusX = Ellipse.RadiusX,
                    RadiusY = Ellipse.RadiusY,
                    Fill = Brushes.Transparent
                };

                var pos = e.GetPosition(CanvasPos);

                CanvasPos.Children.Add(rekt);
                Canvas.SetLeft(rekt, pos.X);
                Canvas.SetTop(rekt, pos.Y);

                CheckEllipse = false;
            }
            if (CheckFigure)
            {
                //Polygon pol = new Polygon();

            }*/
            if(Check)
            {
                Check = false;

                var a = (Rectangle)sender;
                
                var pos = e.GetPosition(CanvasPos);

                Canvas.SetLeft(a, pos.X);
                Canvas.SetTop(a, pos.Y);

            }
        }

        private void inTrash(object sender, RoutedEventArgs e)
        {
            CanvasPos.Children.Clear();
        }
        #endregion
    }
}
