using System.Windows;
using System.Windows.Shapes;
using System.Windows.Controls;

namespace RUIEl
{
    class RememberShNTxT
    {
        public Shape remShape = null;

        public Ellipse left = null;
        public Ellipse right = null;
        public Ellipse top = null;
        public Ellipse bottom = null;

        public TextBox remTxT = null;
        public TextBox remKurwaTxT = null;

        public Point posRemShape;

        public Point posEllLeft;
        public Point posEllRight;
        public Point posEllTop;
        public Point posEllBottom;

        public Point posRemTxT;
        public Point posRemKwTxT;

        public RememberShNTxT(Shape shape, TextBox txt, Point shPos, Point txtPos)
        {
            remShape = shape;
            remTxT = txt;
            posRemShape = shPos;
            posRemTxT = txtPos;
        }

        public RememberShNTxT(Shape shape,Ellipse le, Ellipse re, Ellipse te, Ellipse be, TextBox txt, TextBox kTxT, 
            Point posSh, Point l, Point r, Point t, Point b,Point posTxT, Point posKwTxT)
        {
            remShape = shape;

            left = le;
            right = re;
            top = te;
            bottom = be;

            remTxT = txt;
            remKurwaTxT = kTxT;

            posEllLeft = l;
            posEllRight = r;
            posEllTop = t;
            posEllBottom = b;

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
