using System.Windows;
using System.Windows.Shapes;
using System.Windows.Controls;

namespace RUIEl
{
    class RememberShNTxT
    {
        public Shape remShape = null;
        public TextBox remTxT = null;
        public TextBox remKurwaTxT = null;

        public Point posRemShape;
        public Point posRemTxT;
        public Point posRemKwTxT;

        public RememberShNTxT(Shape shape, TextBox txt, TextBox kTxT, 
            Point posSh, Point posTxT, Point posKwTxT)
        {
            remShape = shape;
            remTxT = txt;
            remKurwaTxT = kTxT;

            posRemShape = posSh;
            posRemTxT = posTxT;
            posRemKwTxT = posKwTxT;
        }
    }

    class RememberLines
    {
        public Line remLine = null;

        public RememberLines(Line line)
        {
            remLine = line;
        }
    }
}
