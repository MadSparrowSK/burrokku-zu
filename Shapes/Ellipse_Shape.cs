using System.Windows.Media;
using System.Windows.Shapes;

namespace Shapes
{
    public class Ell_Shape
    {
        public Rectangle shape;

        public Ell_Shape(Rectangle copy_tangle)
        {
            shape = new Rectangle();

            shape.Width  = copy_tangle.Width;
            shape.Height = copy_tangle.Height;

            shape.RadiusX = copy_tangle.RadiusX;
            shape.RadiusY = copy_tangle.RadiusY;

            shape.Stroke = Brushes.White;
            shape.Fill   = Brushes.Transparent;
        }
    }
}