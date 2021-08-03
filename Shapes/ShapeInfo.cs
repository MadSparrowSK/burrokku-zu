using System.Windows.Shapes;
using System.Windows.Controls;

namespace Shapes
{
    class ShapeInfo
    {
        public Shape shape = null;

        public Ellipse left = null;
        public Ellipse right = null;
        public Ellipse top = null;
        public Ellipse bottom = null;

        public TextBox txt = null;

        public ShapeInfo(Shape shape, TextBox txt)
        {
            this.shape = shape;
            this.txt = txt;
        }

        public ShapeInfo(Shape shape, Ellipse l, Ellipse r, Ellipse t, Ellipse b, TextBox txt)
        {
            this.shape = shape;

            left = l;
            right = r;
            top = t;
            bottom = b;

            this.txt = txt;
        }
    }
}
