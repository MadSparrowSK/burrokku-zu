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
using System.IO;
using System.Text.Json;

namespace Interface_1._0
{
    /// <summary>
    /// Логика взаимодействия для SaveWindow.xaml
    /// </summary>
    /// 

    public partial class SaveWindow : Window
    {

        public SaveWindow()
        {
            InitializeComponent();
            title.Text = this.Title.ToString();

        }
        #region design
        private void Close_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
        private void Mouse_Enter_Close(object sender, RoutedEventArgs e)
        {
            ((Label)sender).Background = Brushes.Red;
        }
        private void Mouse_Leave_Close(object sender, RoutedEventArgs e)
        {
            lbl_Close.Background = new SolidColorBrush(Colors.Gray) { Opacity = 0 };
        }

        private void DnDWindow(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
        #endregion

        #region working with data
        private void Save(object sender, RoutedEventArgs e)
        {
            (Owner as MainWindow).DownSave(sender, null);
            if (DiagrammAnalyzer.tempPath != "")
                DialogResult = true;

        }
        private void notSave(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void cancel(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
        #endregion


    }
}
