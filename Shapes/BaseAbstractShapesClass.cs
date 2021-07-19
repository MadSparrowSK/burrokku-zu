using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Shapes
{
    public class PrimaryShape
    {
        public Shape shape;

        public PrimaryShape(Polygon copy_shape, PointCollection points)
        {
            shape = new Polygon();
            if (points != null) (shape as Polygon).Points = points;
            else (shape as Polygon).Points = copy_shape.Points;
            (shape as Polygon).Stroke = Brushes.White;
            shape.Fill = Brushes.Transparent;
        }
        public PrimaryShape(Rectangle copy_tangle)
        {
            shape = new Rectangle();

            shape.Width = copy_tangle.Width;
            shape.Height = copy_tangle.Height;

            (shape as Rectangle).RadiusX = copy_tangle.RadiusX;
            (shape as Rectangle).RadiusY = copy_tangle.RadiusY;

            shape.Stroke = Brushes.White;
            shape.Fill = Brushes.Transparent;
        }
    }
}