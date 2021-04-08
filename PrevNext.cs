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
            Diagramms = new List<Diagramm> { new Diagramm()};
        }
        /// <summary>
        /// Метод, добавляющий принимаемый параметр в общий список диаграмм
        /// </summary>
        /// <param name="diagramm"></param>
        public static void AddDiaggram(Diagramm diagramm)
        {
            Diagramm.NewID(diagramm);
            Diagramm temp = new Diagramm();
            temp = diagramm.Clone(diagramm.ID);
            Diagramms.Add(temp);
            
        }
        /// <summary>
        /// Метод, возвращающий диаграмму, предшествующую этой
        /// </summary>
        /// <param name="diagramm"></param>
        public static void Prev(ref Diagramm diagramm, out bool flag)
        {
            flag = true;
            int indexOfDiagramm = 0;
            for (int i = 0; i < Diagramms.Count; i++)
            {
                if (Diagramms[i].ID == diagramm.ID)
                {
                    indexOfDiagramm = i;
                }

            }
            if (indexOfDiagramm - 1 > -1)
                diagramm = Diagramms[indexOfDiagramm - 1];
            else
                flag = false;
            
        }
        /// <summary>
        /// Метод, возвращающий диаграмму, идущую после этой
        /// </summary>
        /// <param name="diagramm"></param>
        public static void Next(ref Diagramm diagramm, out bool flag)
        {
            flag = true;
            int indexOfDiagramm = 0;
            for (int i = 0; i < Diagramms.Count; i++)
            {
                if (Diagramms[i].ID == diagramm.ID)
                {
                    indexOfDiagramm = i;
                }
            }
            if (indexOfDiagramm + 1 < Diagramms.Count)
                diagramm = Diagramms[indexOfDiagramm + 1];
            else
                flag = false;
        }

        /// <summary>
        /// Метод, очищающий список диаграмм
        /// </summary>
        public static void Clear()
        {
            Diagramms = new List<Diagramm>{ new Diagramm()};
        }

       
    }
}
