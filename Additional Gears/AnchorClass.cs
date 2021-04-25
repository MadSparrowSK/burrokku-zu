using System.Windows.Media;
using System.Windows.Shapes;

namespace Anchors
{
    public class Anchor 
    {
        public Polyline anchor_NWSE;
        public Polyline anchor_NS;
        public Polyline anchor_WE;

        public readonly int shiftTop;
        public readonly int shiftLeft;

        public bool is_anchor_create = false;

        public Anchor(Polyline NWSE, Polyline NS, Polyline WE, int left, int top) 
        {
            anchor_NS   = new Polyline();
            anchor_WE   = new Polyline();
            anchor_NWSE = new Polyline();

            anchor_NS.Points = NS.Points;
            anchor_WE.Points = WE.Points;
            anchor_NWSE.Points = NWSE.Points;

            shiftLeft = left;
            shiftTop  = top;
        }
        public void SetParametrs()
        { 
            anchor_NS.Stroke = Brushes.Red;
            anchor_NS.Fill   = Brushes.Transparent;

            anchor_WE.Stroke = Brushes.Red;
            anchor_WE.Fill   = Brushes.Transparent;

            anchor_NWSE.Stroke = Brushes.Red;
            anchor_NWSE.Fill   = Brushes.Transparent;
        }
        public void ResetParametrs()
        {
            anchor_NS.Stroke   = Brushes.Transparent;
            anchor_WE.Stroke   = Brushes.Transparent;
            anchor_NWSE.Stroke = Brushes.Transparent;
        }
    }
}
