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
            txtbx.Foreground  = Brushes.Black;
            txtbx.BorderBrush = Brushes.Transparent;
            txtbx.Background  = Brushes.Transparent;
            txtbx.BorderThickness = new Thickness(0);

            

            text_left_indent = leftInd;
            text_top_indent  = topInd;

        }
    }

    static class TxTResetClass
    {
        public static void SetTxT(this TextBox txt)
        {
            txt.CaretBrush = Brushes.Red;
            txt.BorderBrush = Brushes.Gray;
            txt.Foreground = Brushes.White;
            txt.BorderThickness = new Thickness(1);
            txt.IsEnabled = true;
        }

        public static void ResetTxT(this TextBox txt)
        {
            txt.CaretBrush = Brushes.Transparent;
            txt.BorderBrush = Brushes.Transparent;
            txt.BorderThickness = new Thickness(0);
            txt.IsEnabled = false;
        } 
    }
}
