using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface_1._0
{
    //Статический класс для отката диаграмм
    /// <summary>
    /// Служебный статический класс для работы с классом-контейнером Diagramm
    /// </summary>
    public static class PrevNext
    {
        /// <summary>
        /// Список диаграмм, хранящих в себе итерации событий данного приложения
        /// </summary>
        private static List<Diagramm> Diagramms { get; set; }
        static PrevNext()
        {
            Diagramms = new List<Diagramm> { new Diagramm() };
        }
        /// <summary>
        /// Метод, добавляющий принимаемый параметр в общий список диаграмм
        /// </summary>
        /// <param name="diagramm"></param>
        public static void AddDiagramm(ref Diagramm diagramm)
        {
            //Модификатор ref на тот случай, если в будущем предется при добавлении диаграммы в список, менять что-то в существующей
            //Изменяем список
            //Находим индекс старой диаграммы

            int indexOfDiagramm = -1;
            int DiagrammCount = Diagramms.Count;
            for (int i = 0; i < Diagramms.Count; i++)
            {
                if (Diagramms[i].ID == diagramm.ID)
                {
                    indexOfDiagramm = i;
                }
            }


            if ((indexOfDiagramm < DiagrammCount - 1) && (indexOfDiagramm > -1))
            {

                for (int i = Diagramms.Count - 1; i > indexOfDiagramm; i--)
                {
                    Diagramms.RemoveAt(i);
                }
            }
            //Добавляем новую диаграмму
            Diagramm.NewID(diagramm);
            Diagramm temp = diagramm.Clone(diagramm.ID);
            Diagramms.Add(temp);


        }
        /// <summary>
        /// Метод, возвращающий диаграмму, предшествующую этой
        /// </summary>
        /// <param name="diagramm"></param>
        public static void Prev(ref Diagramm diagramm, bool flag)
        {
            int indexOfDiagramm = -1;
            bool end = true;
            for (int i = 0; i < Diagramms.Count; i++)
            {
                if ((Diagramms[i].ID == diagramm.ID) && (end))
                {
                    indexOfDiagramm = i;
                    end = false;
                }

            }
            if (indexOfDiagramm - 1 > -1)
                diagramm = Diagramms[indexOfDiagramm - 1].Clone(Diagramms[indexOfDiagramm - 1].ID);
            else
                flag = false;


        }
        /// <summary>
        /// Метод, возвращающий диаграмму, идущую после этой
        /// </summary>
        /// <param name="diagramm"></param>
        public static void Next(ref Diagramm diagramm, bool flag)
        {
            int indexOfDiagramm = -1;
            for (int i = 0; i < Diagramms.Count; i++)
            {
                if (Diagramms[i].ID == diagramm.ID)
                {
                    indexOfDiagramm = i;
                }
            }
            if (indexOfDiagramm + 1 < Diagramms.Count)
                diagramm = Diagramms[indexOfDiagramm + 1].Clone(Diagramms[indexOfDiagramm + 1].ID);
            else
                flag = false;

        }

        /// <summary>
        /// Метод, очищающий список диаграмм
        /// </summary>
        public static void Clear()
        {
            Diagramms = new List<Diagramm> { new Diagramm() };

        }


    }
}
