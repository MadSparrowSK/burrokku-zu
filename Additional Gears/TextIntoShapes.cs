using System.Windows;
using System.Windows.Media;
using System.Windows.Controls;

namespace TxTnShapes
{
    public class TXT
    {
        public TextBox txtbx;

        public readonly double text_left_indent;
        public readonly double text_top_indent;

        public TXT(double leftInd, double topInd)
        {
            txtbx = new TextBox();

            txtbx.Text = "Text";
            txtbx.MinWidth  = 40;
            txtbx.MinHeight = 20;
            txtbx.FontSize  = 10;
            txtbx.Foreground  = Brushes.White;
            txtbx.BorderBrush = Brushes.Transparent;
            txtbx.Background  = Brushes.Transparent;
            txtbx.BorderThickness = new Thickness(0);
            txtbx.IsEnabled = false;

            text_left_indent = leftInd;
            text_top_indent  = topInd;
        }
        public void PrepareToWriting()
        {
            txtbx.CaretBrush  = Brushes.Red;
            txtbx.BorderBrush = Brushes.Gray;
            txtbx.BorderThickness = new Thickness(1);
            txtbx.IsEnabled = true;
        }
        public void ResetParametrs()
        {
            txtbx.CaretBrush  = Brushes.Transparent;
            txtbx.BorderBrush = Brushes.Transparent;
            txtbx.BorderThickness = new Thickness(0);
            txtbx.IsEnabled = false;
        }
    }
}
