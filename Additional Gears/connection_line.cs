using System.Windows.Media;
using System.Windows.Shapes;


namespace Connect
{
    public class ConnectionLine
    {
        public Ellipse circle_left;

        public Line line_1;

        public ConnectionLine()
        {
            circle_left = new Ellipse();

            circle_left.Height = 5;
            circle_left.Width  = 5;
            circle_left.Fill = Brushes.LightGreen;

            line_1 = new Line();
        }

        public void Reset(Line line) 
        {
            line.Stroke = Brushes.Transparent;
        }
    }
}