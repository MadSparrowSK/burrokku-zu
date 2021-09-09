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
    /// Логика взаимодействия для picture.xaml
    /// </summary>
    public partial class picture : Window
    {
        public picture()
        {
            
            InitializeComponent();
            try
            {
                User activeUser = AllUsers.FindMarkedUser();
                string path = "../../tasks/" + activeUser.SelectedTerm + "_" + activeUser.SelectedLab + "_" + activeUser.SelectedTask + "_" + activeUser.Option + ".PNG";
                BitmapImage bitmapImage = new BitmapImage(new Uri(path, UriKind.RelativeOrAbsolute));
                Task.ImageSource = bitmapImage;
                this.Height = bitmapImage.Height + 50;
                this.Width = bitmapImage.Width;
                this.Title = "Задание №" + activeUser.SelectedTask;
            }
            catch (Exception)
            {
                this.Title = "Ошибка!";
                Task.ImageSource = null;
                this.Background = new SolidColorBrush(Colors.White);
                this.Height = 150;
                this.Width = 400;
            }

            
        }
       
    }
}
