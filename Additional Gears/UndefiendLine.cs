using System.Windows.Media;
using System.Windows.Shapes;

namespace Connect
{
    public class UndefiendLine
    {
        public Line undefLine = null;
        public Polyline undefArrow = null;

        public UndefiendLine(Line lineInCanvas)
        {
            undefLine = lineInCanvas;
        }
        public UndefiendLine(Line line, Polyline polyline)
        {
            undefLine = line;
            undefArrow = polyline;
        }
    }
}
