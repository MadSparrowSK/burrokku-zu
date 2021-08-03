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

        public RememberShNTxT(Shape shape, Point pos)
        {
            this.remShape = shape;
            this.posRemShape = pos;
        }

        public RememberShNTxT(Shape shape, TextBox txt, Point shPos, Point txtPos)
        {
            remShape = shape;
            remTxT = txt;
            posRemShape = shPos;
            posRemTxT = txtPos;
        }

        public RememberShNTxT(Shape shape,Ellipse le, Ellipse re, Ellipse te, Ellipse be, TextBox txt, 
            Point posSh, Point l, Point r, Point t, Point b,Point posTxT)
        {
            remShape = shape;

            left = le;
            right = re;
            top = te;
            bottom = be;

            remTxT = txt;

            posEllLeft = l;
            posEllRight = r;
            posEllTop = t;
            posEllBottom = b;

            posRemShape = posSh;
            posRemTxT = posTxT;
        }
    }

    class RememberLines
    {
        public Line remLine = null;

        public Point posXY1;
        public Point posXY2;

        public bool top = false;
        public bool bottom = false;

        public RememberLines(Line line, Point pos)
        {
            remLine = line;
            posXY1 = pos;
        }
        public RememberLines(Line line, Point pos1, Point pos2)
        {
            remLine = line;
            posXY1 = pos1;
            posXY2 = pos2;
        }
    }
}
