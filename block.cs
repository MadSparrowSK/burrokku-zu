using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Interface_1._0
{
    //Перечисление типов фигур
    enum Shapes
    {
        Ellipse,
        Rekt,
        Parrabellum,
        Rhomb,
        Cycle
    }

    class Diagramm
    {
        public int ShapesCounter { get; set; }
        public List<Block> blocks = new List<Block>();
    }
    class Block
    {
        //Координаты фигуры
        public Point LeftTop { get; set; }
        public Point NW { get; set; }
        public Point NE { get; set; }
        public Point SW { get; set; }
        public Point SE { get; set; }
        // две доп. координаты для пятиугольника
        public Point AddPoint1 { get; set; } 
        public Point AddPoint2 { get; set; }

        //Индекс фигуры
        public int indexNumber = 0;
        

        //Длина и ширина фигуры для элипса
        public double Width { get; set; }

        public double Height { get; set; }

        //Тип фигуры
        public Shapes Shape { get; set; }

        //Ширина, длина и размер шрифта текста внутри фигуры
        /*
        public int TextWidth { get; set; }

        public int TextHeigth { get; set; }

        public int FontSize { get; set; }
        */

        public Block()
        {

        }
        //Конструктор для работы с фигурой без текста
        public Block(Shapes shape, Point leftTop, Point nw, Point ne, Point sw , Point se, Point addpoint1, Point addpoint2, int width = 0, int height = 0, int indexOfShape = 0)
        {
            LeftTop = leftTop;
            NW = nw;
            NE = ne;
            SW = sw;
            SE = se;
            AddPoint1= addpoint1;
            AddPoint2 = addpoint2;
            Width = width;
            Height = height;
            Shape = shape;
            indexNumber = indexOfShape;
        }
        
        public Block(Shapes shape, Point leftTop, Point nw, Point ne, Point sw, Point se, int width = 0, int height = 0)
        {
            LeftTop = leftTop;
            NW = nw;
            NE = ne;
            SW = sw;
            SE = se;
            Width = width;
            Height = height;
            Shape = shape;
        }
        /*
        //Конструктор для работы с фигурой с текстом
        
        public Diagramm(Shapes shape, int nw, int ne, int sw, int se, int addpoint1 = 0, int addpoint2 = 0, int width = 0, int height = 0, int textWidth = 10, int textHeight = 10, int fontSize = 8)
        {
            NW = nw;
            NE = ne;
            SW = sw;
            SE = se;
            AddPoint1 = addpoint1;
            AddPoint2 = addpoint2;
            Width = width;
            Height = height;
            Shape = shape;
            TextWidth = textWidth;
            TextHeight = textHeight;
            FontSize = fontSize;
        }
        */

        public override string ToString()
        {
            return Shape.ToString();
        }
    }

    class Arrows
    {
        //Координаты точек стрелочки
        public List<ArrowPoint> Points { get; set; }
        //Переменные, которые, возможно пригодятся при работе со стрелами
        /*
        public int StartIndex { get; set; }
        public int EndIndex { get; set; }

        public int StartConnectorIndex { get; set; }

        public int EndConnectorIndex { get; set; }
        */
    }

    class ArrowPoint
    {
        //Координаты одной стрелы
        public int X1 { get; set; }
        public int Y1 { get; set; }
        public int X2 { get; set; }
        public int Y2 { get; set; }
    }

}
