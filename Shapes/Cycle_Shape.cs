using System.Windows;
using System.Windows.Shapes;
namespace Shapes
{
    public class Cy_Shape : RP_Shapes
    {
        public Point W_point;
        public Point E_point;

        public Cy_Shape(Polygon copy_shape, Point left, Point bottom, Point right, Point top, Point W, Point E)
            : base(copy_shape, left, bottom, right, top)
        {
            W_point = new Point();
            E_point = new Point();

            W_point = W;
            E_point = E;
        }

        public override Point Point_W { get => W_point; set { W_point = value; } }
        public override Point Point_E { get => E_point; set { E_point = value; } }
    }
}