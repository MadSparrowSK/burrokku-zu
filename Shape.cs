using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Interface_1._0
{
    abstract class Shape
    {
        public abstract PointCollection PointsOfShape { get; set; }
        public abstract Point coordinates { get; set; }
        public abstract TextBox txt { get; set; }
        public int t;
        abstract public void DropShape(ref int counter, ref bool isLoaded, ref bool isPrevNext, ref bool ischanged);
        abstract public void AddShape(ref int counter, ref bool isLoaded, ref bool isPrevNext, ref bool ischanged);
        abstract public void MoveShape(ref int counter, ref bool isLoaded, ref bool isPrevNext, ref bool ischanged);
        

    }

    class rect : Shape
    {
        public override PointCollection PointsOfShape { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public override Point coordinates { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public override TextBox txt { get; set; }

        public override void AddShape(ref int counter, ref bool isLoaded, ref bool isPrevNext, ref bool ischanged)
        {
            throw new NotImplementedException();
        }

        public override void DropShape(ref int counter, ref bool isLoaded, ref bool isPrevNext, ref bool ischanged)
        {
            throw new NotImplementedException();
        }

        public override void MoveShape(ref int counter, ref bool isLoaded, ref bool isPrevNext, ref bool ischanged)
        {
            throw new NotImplementedException();
        }
    }

}
