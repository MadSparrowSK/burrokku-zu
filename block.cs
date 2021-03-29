using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public List<block> blocks { get; set; }
    }
    class block
    {
        //Координаты фигуры
        public int X { get; set; }
        public int Y { get; set; }

        //Длина и ширина фигуры
        public int Width { get; set; }

        public int Height { get; set; }

        //Тип фигуры
        public Shapes Shape { get; set; }

        //Ширина, длина и размер шрифта текста внутри фигуры
        /*
        public int TextWidth { get; set; }

        public int TextHeigth { get; set; }

        public int FontSize { get; set; }
        */

        public block()
        {

        }
        //Конструктор для работы с фигурой без текста
        public block(int x, int y, int width, int height, Shapes shape)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
            Shape = shape;
        }

        //Конструктор для работы с фигурой с текстом
        /*
        public Diagramm(int x, int y, int width, int height, Shapes shape, int textWidth = 10, int textHeight = 10, int fontSize = 8)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
            Shape = shape;
            TextWidth = textWidth;
            TextHeight = textHeight;
            FontSize = fontSize;
        }
        */

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
        //Координаты одной из точек стрелки
        public int X { get; set; }
        public int Y { get; set; }
    }

}
