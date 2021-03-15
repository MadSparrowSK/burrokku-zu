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
            lastPoint_img = e.GetPosition(smt);

            DragDrop.DoDragDrop(this, this, DragDropEffects.Copy);
        }

        #region Interaction_With_Shapes_Into_Canvas

        ImageCustomShape img_custom;
        Polyline anchor_size;

        Point lastPoint_img;
        Point lastPoint_anchor;
        Point current_anchor_postion;

        bool anchor_create = false;

        private void IntoCanvasDown(object sender, MouseButtonEventArgs e)
        {
            var smt = (Image)sender;
            lastPoint_img = e.GetPosition(smt);

            if(!anchor_create)
            {
                anchor_size = new Polyline();
                anchor_size.Points = Anchor.Points;
                anchor_size.Stroke = Brushes.Gray;
                anchor_size.Fill = Brushes.Transparent;

                anchor_size.MouseDown += AnchorMouseDown;
                anchor_size.MouseMove += AnchorMouseMove;
                anchor_size.MouseUp   += AnchorMouseUp;

                CanvasPos.Children.Add(anchor_size);

                Canvas.SetLeft(anchor_size, Canvas.GetLeft(smt) + 25);
                Canvas.SetTop(anchor_size, Canvas.GetTop(smt) + 10);

                anchor_create = true;
            }
        }

        private void AnchorMouseDown(object sender, MouseButtonEventArgs e)
        {
            var anchor = (UIElement)sender;
            lastPoint_anchor = e.GetPosition(anchor);
            current_anchor_postion = e.GetPosition(CanvasPos);
        }
        private void AnchorMouseMove(object sender, MouseEventArgs e)
        {
            if(e.LeftButton == MouseButtonState.Pressed)
            {
                Point pos = new Point();

                var anchor = (UIElement)sender;
                anchor.CaptureMouse();

                pos.X = e.GetPosition(CanvasPos).X - lastPoint_anchor.X;
                pos.Y = e.GetPosition(CanvasPos).Y - lastPoint_anchor.Y;

                if(pos.X < current_anchor_postion.X && pos.Y < current_anchor_postion.Y) 
                {
                    img_custom.img.Width  += 1;
                    img_custom.img.Height += 1;

                    //current_anchor_postion = e.GetPosition(CanvasPos);
                }
                else
                {
                    img_custom.img.Width  -= 1;
                    img_custom.img.Height -= 1;

                    //current_anchor_postion = e.GetPosition(CanvasPos);
                }

                Canvas.SetLeft(anchor, pos.X);
                Canvas.SetTop(anchor, pos.Y);
            }
        }
        private void AnchorMouseUp(object sender, MouseButtonEventArgs e)
        {
            var anchor = (UIElement)sender;
            anchor.ReleaseMouseCapture();
        }

        private void IntoCanvasMove(object sender, MouseEventArgs e)
        {
            if(e.LeftButton == MouseButtonState.Pressed) 
            {
                if(anchor_create)
                {
                    anchor_create = false;
                    CanvasPos.Children.Remove(anchor_size);
                }

                var smt = (UIElement)sender;
                smt.CaptureMouse();

                var pos = e.GetPosition(CanvasPos);

                Canvas.SetLeft(smt, pos.X - lastPoint_img.X);
                Canvas.SetTop(smt, pos.Y - lastPoint_img.Y);
            }
        }
        public void IntoCanvasUp(object sender, MouseButtonEventArgs e)
        {
            var smt = (Image)sender;
            smt.ReleaseMouseCapture();
        }
        #endregion
        private void DnD_Drop(object sender, DragEventArgs e)
        {

            if(Rectangle_Check) 
            {
                Rectangle_Check = false;

                img_custom = new ImageCustomShape();
                img_custom.addRectangle();

                img_custom.img.MouseDown += IntoCanvasDown;
                img_custom.img.MouseMove += IntoCanvasMove;
                img_custom.img.MouseUp   += IntoCanvasUp;

                CanvasPos.Children.Add(img_custom.img);

                var pos = e.GetPosition(CanvasPos);

                Canvas.SetLeft(img_custom.img, pos.X - lastPoint_img.X);
                Canvas.SetTop(img_custom.img, pos.Y - lastPoint_img.Y);
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
        }
        #endregion
    }
}
