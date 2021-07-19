using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface_1._0
{
    /// <summary>
    /// Статический класс, хранящий данные, необходимые для функционирования программы
    /// </summary>
    static class DiagrammAnalyzer
    {
        //Булевые переменные, необходимые для всплывающего окна
        public static bool isLoaded { get; set; }
        public static bool isChanged { get; set; }
        //Булевые переменные, необходимые для работы PrevNext
        public static bool isPrevNext { get; set; }
        public static bool PrevNextTextChanged { get; set; }
        public static bool ShapeMoved { get; set; }
        public static bool isEmpty { get; set; }
        public static bool isCanBeLoaded { get; set; }

        //Счетчик фигур
        public static int shapesCounter { get; set; }
        //Строки для работы с проводником
        public static string tempPath { get; set; }
        public static string pathForSaving { get; set; }
        static DiagrammAnalyzer()
        {
            isLoaded = false;
            isChanged = false;
            isPrevNext = false;
            PrevNextTextChanged = false;
            isEmpty = false;
            isCanBeLoaded = true;
            shapesCounter = 0;
            tempPath = "";
            pathForSaving = "";

        }
    }
}
