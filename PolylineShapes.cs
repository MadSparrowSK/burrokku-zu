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
    public class CustomShape
    {
        public Polyline polyline;
        public Rectangle rectangle;
        public Ellipse ellipse;

        public CustomShape(Polyline pol)
        {
            polyline = new Polyline()
            {
                Fill = Brushes.Transparent,
                Stroke = Brushes.White,
                StrokeThickness = 1.5,
                Points = pol.Points,

            };
        }
        public CustomShape(Rectangle rect)
        {
            rectangle = new Rectangle()
            {
                Width = rect.Width,
                Height = rect.Height,
                RadiusX = rect.RadiusX,
                RadiusY = rect.RadiusY,
                Fill = Brushes.Transparent,
                Stroke = Brushes.White,
                StrokeThickness = 1.5,
            };
        }
        public CustomShape(Ellipse ell)
        {
            //...
        }
    }
}
