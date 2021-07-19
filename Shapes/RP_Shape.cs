using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Shapes
{
    public class RP_Shapes : PrimaryShape
    {
        public Point NW_point;
        public Point SW_point;
        public Point SE_point;
        public Point NE_point;

        public RP_Shapes(Polygon copy_shape, Point left, Point bottom, Point right, Point top, PointCollection points = null) 
            : base(copy_shape, points)
        {

            NW_point = new Point();
            SW_point = new Point();
            SE_point = new Point();
            NE_point = new Point();

            NW_point = left;
            SW_point = bottom;
            SE_point  = right;
            NE_point  = top;
        }
        
        public Point Point_NW { get => NW_point; set { NW_point = value; } }
        public Point Point_SW { get => SW_point; set { SW_point = value; } }
        public Point Point_SE { get => SE_point; set { SE_point = value; } }
        public Point Point_NE { get => NE_point; set { NE_point = value; } }
    }
}
