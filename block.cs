using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
namespace Interface_1._0
{
    /// <summary>
    /// Перечисление доступных типов фигур
    /// </summary>
    [DataContract]
    public enum Shapes
    {
        Ellipse,
        Rekt,
        Parrabellum,
        Rhomb,
        Cycle
    }
    /// <summary>
    /// Класс-контейнер, необходимый для операц сериализации (сохранение, загрузка, кодировани данных)
    /// </summary>
    public class Diagramm
    {
        public Diagramm()
        {
            _id = randomID.Next(-100000, 100000) * randomID.NextDouble();
        }

        //Рандомайзер для создания id диаграмм
        public static Random randomID = new Random();
        private double _id = 0;
        //Счетчик фигур. По большеому счету, бесполезен, но в теории может пригодиться при добавлении стрел
        public int ShapesCounter { get; set; }
        //ID диаграммы
        public double ID { get { return _id; } set { _id = value; } }
        

        /// <summary>
        /// Список со всеми фигурами
        /// </summary>
        public List<Block> blocks { get; set; } = new List<Block>();
        //Вспоминаем, что класс - это ссылочный тип и пишем метод для передачи значения экземпляра
        /// <summary>
        /// Возвращает Diagramm-объект, хранящий значение данного объекта
        /// </summary>
        /// <returns></returns>
        public Diagramm Clone()
        {
            List<Block> blocksClone = new List<Block> { };
            foreach (Block block in blocks)
            {
                blocksClone.Add(block.Clone());
            }
            Diagramm diagrammClone = new Diagramm()
            {
                blocks = blocksClone,
                ShapesCounter = this.ShapesCounter
            };
            return diagrammClone;
        }
        public Diagramm Clone(double id)
        {
            List<Block> blocksClone = new List<Block> { };
            foreach (Block block in blocks)
            {
                blocksClone.Add(block.Clone());
            }
            Diagramm diagrammClone = new Diagramm()
            {
                blocks = blocksClone,
                ShapesCounter = this.ShapesCounter,
                ID = id
            };
            return diagrammClone;
        }
        /// <summary>
        /// Статический метод для обновления ID
        /// </summary>
        /// <param name="diagramm"></param>
        public static void NewID( in Diagramm diagramm)
        {
            diagramm.ID = randomID.Next(-100000, 100000) * randomID.NextDouble();
        }

        /// <summary>
        /// Возвращает id диаграммы
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "Diagramm id " + ID;
        }
    }
    /// <summary>
    /// Класс-контейнер, содержащий в себе данные для воссоздания фигуры
    /// </summary>
    public class Block
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
        public int IndexNumber { get; set; }
        

        //Длина и ширина фигуры для элипса
        public double Width { get; set; }

        public double Height { get; set; }

        //Тип фигуры
        public Shapes Shape { get; set; }

        //Текст внутри фигуры
        public string  TextIntoTextBox { get; set; }

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
        public Block(Shapes shape, Point leftTop, Point nw, Point ne, Point sw , Point se, Point addpoint1, Point addpoint2, int indexNumber, string textIntoTextBox)
        {
            LeftTop = leftTop;
            NW = nw;
            NE = ne;
            SW = sw;
            SE = se;
            AddPoint1= addpoint1;
            AddPoint2 = addpoint2;
            
            Shape = shape;
            IndexNumber = indexNumber;

            TextIntoTextBox = textIntoTextBox;
        }
        
        public Block(Shapes shape, Point leftTop, Point nw, Point ne, Point sw, Point se, int indexNumber, string textIntoTextBox)
        {
            LeftTop = leftTop;
            NW = nw;
            NE = ne;
            SW = sw;
            SE = se;
            Shape = shape;
            IndexNumber = indexNumber;
            TextIntoTextBox = textIntoTextBox;
        }

        public Block(Shapes shape, Point leftTop, double width, double height, int indexNumber, string textIntoTextBox)
        {
            Shape = shape;
            LeftTop = leftTop;
            Width = width;
            Height = height;
            IndexNumber = indexNumber;
            TextIntoTextBox = textIntoTextBox;
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

        /// <summary>
        /// Возвращает Block-объект, хранящий значение данного объекта
        /// </summary>
        /// <returns></returns>
        public Block Clone()
        {
            Block blockClone = new Block()
            {
                LeftTop = this.LeftTop,
                NW = this.NW,
                NE = this.NE,
                SW = this.SW,
                SE = this.SE,
                AddPoint1 = this.AddPoint1,
                AddPoint2 = this.AddPoint2,
                Shape = this.Shape,
                IndexNumber = this.IndexNumber,
                Width = this.Width,
                Height = this.Height,
                TextIntoTextBox = this.TextIntoTextBox,
            };
        
            return blockClone;
        }

        /// <summary>
        /// Вернет имя данной фигуры (в имени хранится индекс и тип фигуры)
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Shape.ToString() + "_" + IndexNumber;
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
