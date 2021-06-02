using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Emitter
{
    class ExcretorySquare
    {
        public Polygon mainSquare = new Polygon();
        public Rectangle additSquare = new Rectangle();

        public Point mainS_NW;
        public Point mainS_SW;
        public Point mainS_SE;
        public Point mainS_NE;

        PointCollection resetCollection = new PointCollection();

        public ExcretorySquare(Point nw, Point sw, Point se, Point ne)
        {
            additSquare.Stroke = Brushes.Transparent;
            additSquare.Width = 10;
            additSquare.Height = 10;

            mainS_NW = nw;
            mainS_SW = sw;
            mainS_SE = se;
            mainS_NE = ne;

            PointCollection points = new PointCollection();
            points.Add(nw);
            points.Add(sw);
            points.Add(se);
            points.Add(ne);

            resetCollection.Add(nw);
            resetCollection.Add(sw);
            resetCollection.Add(se);
            resetCollection.Add(ne);

            mainSquare.Points = points;

            mainSquare.Fill = new SolidColorBrush(Colors.Blue) { Opacity = .1 };
            mainSquare.Stroke = Brushes.Blue;
        }

        public void Reset()
        {
            mainSquare.Points = resetCollection;
        }
        public void SetColors()
        {
            mainSquare.Fill = new SolidColorBrush(Colors.Blue) { Opacity = .1 };
            mainSquare.Stroke = Brushes.Blue;

            additSquare.Stroke = Brushes.Transparent;

            mainSquare.IsEnabled = true;
        }
        public void ResetColors()
        {
            mainSquare.Fill = Brushes.Transparent;
            mainSquare.Stroke = Brushes.Transparent;

            additSquare.Stroke = Brushes.Transparent;

            mainSquare.IsEnabled = false;
        }
    }
}
