using System;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Interface_1._0
{
    class ImageCustomShape
    {
        public Image img;

        public void addRectangle() 
        {
            img = new Image();
            img.Width = 100;
            img.Height = 50;

            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri("Shapes/rekt.PNG", UriKind.Relative);
            bitmap.EndInit();

            img.Source = bitmap;
        }
    }
}
