using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace KeyKeeper
{
    /// <summary>
    /// Interaction logic for AuthWindow.xaml
    /// </summary>
    public partial class AuthWindow : Window
    {
        public AuthWindow()
        {
            InitializeComponent();
        }

        private void BtnLogin_OnClick(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(this.tbLogin.Text))
            {
                this.Dispatcher.Invoke(
                    () =>
                    {
                        new MainWindow(this.tbLogin.Text).Show();
                    });

                this.Close();
            }
            else
            {
                this.tbLogin.BorderBrush = Brushes.Red;
            }
        }

        private void TbLogin_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.tbLogin.Text))
            {
                return;
            }

            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
               this.BtnLogin_OnClick(null, new RoutedEventArgs());
            }
        }

        private void PbPassword_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.tbLogin.Text))
            {
                return;
            }

            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                this.BtnLogin_OnClick(null, new RoutedEventArgs());
            }
        }
    }
}
