using System;
using System.Windows;

namespace KeyKeeper
{
    /// <summary>
    /// Interaction logic for KeyEmulationWindow.xaml
    /// </summary>
    public partial class KeyEmulationWindow : Window
    {
        public KeyEmulationWindow()
        {
            InitializeComponent();
        }

        public event EventHandler<string> Done; 

        private void BtnLogin_OnClick(object sender, RoutedEventArgs e)
        {
            if (this.Done != null)
            {
                this.Done(this, this.tbName.Text);
            }

            this.DialogResult = this.HasCard.IsChecked;
        }
    }
}
