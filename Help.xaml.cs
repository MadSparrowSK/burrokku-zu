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
using System.Windows.Shapes;

namespace Interface_1._0
{
    /// <summary>
    /// Логика взаимодействия для Help.xaml
    /// </summary>
    public partial class Help : Window
    {
        public Help()
        {
            InitializeComponent();
        }
        #region design

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
            AllUsers.RemoveMark();
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
        #region ResizeAndDragMainWindow

        private void Roll_Up(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
        private void Mouse_Drag_Window(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }
        #endregion
    }
}
