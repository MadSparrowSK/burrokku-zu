using System.Windows.Media;
using System.Windows.Shapes;


namespace Connect
{
    public class ConnectionLine
    {
        public Ellipse circle_left;
        public Ellipse circle_right;
        public Ellipse circle_top;
        public Ellipse circle_bottom;

        public Line line_left;
        public Line line_right;
        public Line line_top;
        public Line line_bottom;

        public ConnectionLine()
        {
            circle_left = new Ellipse();
            circle_left.Height = 5;
            circle_left.Width  = 5;
            circle_left.Fill = Brushes.Green;

            circle_right = new Ellipse();
            circle_right.Height = 5;
            circle_right.Width = 5;
            circle_right.Fill = Brushes.Green;

            circle_top = new Ellipse();
            circle_top.Height = 5;
            circle_top.Width = 5;
            circle_top.Fill = Brushes.Green;

            circle_bottom = new Ellipse();
            circle_bottom.Height = 5;
            circle_bottom.Width = 5;
            circle_bottom.Fill = Brushes.Green;

            line_left = new Line();
            line_right = new Line();
            line_bottom = new Line();
            line_top = new Line();
        }
    }
}