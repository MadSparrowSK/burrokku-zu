using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Shapes
{
    public abstract class PrimaryShape
    {
        public Polygon shape;

        public PrimaryShape(Polygon copy_shape)
        {
            shape = new Polygon();

            shape.Points = copy_shape.Points;
            shape.Stroke = Brushes.White;
            shape.Fill   = Brushes.Transparent;
        }

        public abstract Point Point_N   { get; set; }
        public abstract Point Point_NW  { get; set; }
        public abstract Point Point_W   { get; set; }
        public abstract Point Point_SW  { get; set; }
        public abstract Point Point_S   { get; set; }
        public abstract Point Point_SE  { get; set; }
        public abstract Point Point_E   { get; set; }
        public abstract Point Point_NE  { get; set; }
    }
}