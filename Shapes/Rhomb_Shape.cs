using System.Windows;
using System.Windows.Shapes;

namespace Shapes
{
    public class Rh_Shape : PrimaryShape
    {
        public Point W_Point;
        public Point S_Point;
        public Point E_Point;
        public Point N_Point;
        
        public Rh_Shape(Polygon copy_shape, Point left, Point bottom, Point right, Point top)
            : base(copy_shape)
        {
            W_Point = new Point(); 
            S_Point = new Point(); 
            E_Point = new Point(); 
            N_Point = new Point();

            W_Point.X = left.X;
            W_Point.Y = left.Y;

            S_Point.X = bottom.X;
            S_Point.Y = bottom.Y;

            E_Point.X = right.X;
            E_Point.Y = right.Y;

            N_Point.X = top.X;
            N_Point.Y = top.Y;
        }

        public Point Point_N { get => N_Point; set { N_Point = value; } }
        public Point Point_W { get => W_Point; set { W_Point = value; } }
        public Point Point_S { get => S_Point; set { S_Point = value; } }
        public Point Point_E { get => E_Point; set { E_Point = value; } }
    }
}