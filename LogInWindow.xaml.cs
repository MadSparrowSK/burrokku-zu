using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Interface_1._0
{
    /// <summary>
    /// Логика взаимодействия для LogInWindow.xaml
    /// </summary>
    public partial class LogInWindow : Window
    {
        public LogInWindow()
        {
            InitializeComponent();
        }

        #region design

        private void Mouse_Enter(object sender, RoutedEventArgs e)
        {
            ((Label)sender).Background = new SolidColorBrush(Colors.Gray) { Opacity = 0.2 };
        }
        private void Mouse_Leave(object sender, RoutedEventArgs e)
        {
            ((Label)sender).Background = new SolidColorBrush(Colors.Gray) { Opacity = 0 };
        }
        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void Mouse_Enter_Close(object sender, RoutedEventArgs e)
        {
            ((Label)sender).Background = Brushes.Red;
        }
        private void Mouse_Leave_Close(object sender, RoutedEventArgs e)
        {
            lbl_Close.Background = new SolidColorBrush(Colors.Gray) { Opacity = 0 };
        }


        #endregion
        #region ResizeAndDragMainWindow

        private void Roll_Up(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
        private void Mouse_Drag_Window(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }
        #endregion

        private void SwitchWindow(object sender, MouseButtonEventArgs e)
        {
            if(LoginZone.Visibility == Visibility.Hidden)
            {
                //Скрываем все данные окна регистрации
                RegistrationZone.Visibility = Visibility.Hidden;
                RegistrationTxB.Text = "";
                OptionTxB.Text = "";
                PassWord1TxB.Text = "";
                PassWord2TxB.Text = "";
                TB_Registration.Style = (Style)FindResource("HiddenTB");
                TB_option.Style = (Style)FindResource("HiddenTB");
                TB_checkingPS1.Style = (Style)FindResource("HiddenTB");
                TB_checkingPS2.Style = (Style)FindResource("HiddenTB");
                //Показываем данные окна входа
                SepBelowSwitcher.Points = new PointCollection() { new Point(0, 0), new Point(93, 0) };
                switcher.Text = "Зарегистрироваться";
                LoginZone.Visibility = Visibility.Visible;
                TB_login.Style = (Style)FindResource("TextBlockForTxB_login");
                TB_password.Style = (Style)FindResource("TextBlockForTxB_password");
            }
            else
            {

                //Скрываем данные окна входа
                SepBelowSwitcher.Points = new PointCollection() { new Point(85, 0), new Point(112, 0) };
                switcher.Text = "Уже есть аккаунт? Войти";
                LoginZone.Visibility = Visibility.Hidden;
                TB_login.Style = (Style)FindResource("HiddenTB");
                TB_password.Style = (Style)FindResource("HiddenTB");
                loginTxB.Text = "";
                passwordTxB.Text = "";
                //Показываем все данные окна регистрации
                RegistrationZone.Visibility = Visibility.Visible;
                TB_Registration.Style = (Style)FindResource("TextBlockForTxB_registration");
                TB_option.Style = (Style)FindResource("TextBlockForTxB_option");
                TB_checkingPS1.Style = (Style)FindResource("TextBlockForTxB_checkPassword1");
                TB_checkingPS2.Style = (Style)FindResource("TextBlockForTxB_checkPassword2");
            }
        }
    }
}
