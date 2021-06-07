using System.Windows;
using System.Windows.Media;
using System.Windows.Controls;

namespace TxTnShapes
{
    public class TXT
    {
        public TextBox txtbx;
        public TextBox kurwa_txtbox;

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

            kurwa_txtbox = new TextBox();

            kurwa_txtbox.Text = "...";
            kurwa_txtbox.Foreground  = Brushes.Transparent;
            kurwa_txtbox.BorderBrush = Brushes.Transparent;
            kurwa_txtbox.Background  = Brushes.Transparent;
            kurwa_txtbox.IsEnabled = false;
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
