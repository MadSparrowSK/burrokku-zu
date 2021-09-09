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
        Rect,
        Parabellum,
        Rhomb,
        Cycle
    }
    /// <summary>
    /// Класс-контейнер, необходимый данные, необходимые для сериализации (сохранение, загрузка, кодировани данных)
    /// </summary>
    [Serializable]
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
        /// Имя для нахождения сохранения для работы правого сайд-бара
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Список со всеми фигурами
        /// </summary>
        public List<Block> blocks { get; set; } = new List<Block>();
        public List<DataForSavingLine> Lines { get; set; } = new List<DataForSavingLine>();
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
            List<DataForSavingLine> LinesClone = new List<DataForSavingLine> { };
            foreach (DataForSavingLine line in Lines)
            {
                LinesClone.Add(line.CloneLine());
            }
            Diagramm diagrammClone = new Diagramm()
            {
                blocks = blocksClone,
                Lines = LinesClone,
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
            List<DataForSavingLine> LinesClone = new List<DataForSavingLine> { };
            foreach (DataForSavingLine line in Lines)
            {
                LinesClone.Add(line.CloneLine());
            }
            Diagramm diagrammClone = new Diagramm()
            {
                blocks = blocksClone,
                Lines = LinesClone,
                ShapesCounter = this.ShapesCounter,
                ID = id
            };
            return diagrammClone;
        }
        /// <summary>
        /// Статический метод для обновления ID
        /// </summary>
        /// <param name="diagramm"></param>
        public static void NewID(in Diagramm diagramm)
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
    [Serializable]
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
        public string TextIntoTextBox { get; set; }

        public Block()
        {

        }
        //Конструктор для работы с фигурой без текста
        public Block(Shapes shape, Point leftTop, Point nw, Point ne, Point sw, Point se, Point addpoint1, Point addpoint2, int indexNumber, string textIntoTextBox)
        {
            LeftTop = leftTop;
            NW = nw;
            NE = ne;
            SW = sw;
            SE = se;
            AddPoint1 = addpoint1;
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
    /// <summary>
    /// Класс-контейнер, содержащий в себе данные для воссоздания линий
    /// </summary>
    [Serializable]
    public class DataForSavingLine
    {
        //Счетчик для линий
        public int LinesCounter { get; set; }
        //Координаты начала и конца линии
        public Point StartPoint { get; set; }
        public Point EndPoint { get; set; }

        //Имя круга-источника линии и круга-цели
        public string Source { get; set; }
        public string Target { get; set; }

        public DataForSavingLine(Point startPoint, Point endPoint, string source, string target, int linesCounter)
        {
            StartPoint = startPoint;
            EndPoint = endPoint;
            Source = source;
            Target = target;
            LinesCounter = linesCounter;
        }

        public DataForSavingLine()
        {

        }

        public DataForSavingLine CloneLine()
        {
            DataForSavingLine LineClone = new DataForSavingLine()
            {
                StartPoint = this.StartPoint,
                EndPoint = this.EndPoint,
                Source = this.Source,
                Target = this.Target,
                LinesCounter = this.LinesCounter,
            };
            return LineClone;
        }


        public override string ToString()
        {
            return Source + " - " + Target + " № " + LinesCounter;
        }
    }

}
