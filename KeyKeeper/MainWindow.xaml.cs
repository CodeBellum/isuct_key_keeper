using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace KeyKeeper
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Dictionary<int, StackPanel> states;

        public static string AdminLogin { get; private set; }
       
        public MainWindow(string adminLogin)
        {
            AdminLogin = adminLogin;
            this.states = new Dictionary<int, StackPanel>();
            InitializeComponent();
        }

        private void FrameworkElement_OnInitialized(object sender, EventArgs e)
        {
            for (int i = 0; i < this.Rooms.RowDefinitions.Count; i++)
            {
                for (int j = 0; j < this.Rooms.ColumnDefinitions.Count; j++)
                {
                    int roomId = i * this.Rooms.ColumnDefinitions.Count + j + 1;
                    var element = new StackPanel();
                    element.Background = Brushes.GreenYellow;
                    element.Tag = 0;
                    element.MouseLeftButtonUp += this.Element_MouseLeftButtonUp;
                    element.Children.Add(this.CreateNumLabel(roomId));
                    element.Children.Add(this.CreateLabel(true));
                    Grid.SetRow(element, i);
                    Grid.SetColumn(element, j);
                    this.states[roomId] = element;
                    this.Rooms.Children.Add(element);
                }
            }
        }

        private Label CreateLabel(bool isEmpty)
        {
            Label res = new Label();
            res.VerticalAlignment = VerticalAlignment.Center;
            res.HorizontalAlignment = HorizontalAlignment.Center;
            res.Content = isEmpty ? "Свободна" : "Занята";
            return res;
        }

        private Label CreateNumLabel(int num)
        {
            Label res = new Label();
            res.VerticalAlignment = VerticalAlignment.Center;
            res.HorizontalAlignment = HorizontalAlignment.Left;
            res.Content = num;
            return res;
        }

        private void Element_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            StackPanel element = (StackPanel)sender;
            bool isEmpty = (int)element.Tag == 0;
            int roomId = this.states.First(kvp => kvp.Value.Equals(element)).Key;
            
            Operation operation = new Operation(
                roomId,
                isEmpty ? "Нектототамов Н.Н." : OperationsKeeper.Instance.GetLastGetKeyOperationForRoom(roomId).User, 
                isEmpty ? OperationType.GetKey : OperationType.ReturnKey, true);

            bool result = new OperationWindow(operation).ShowDialog() ?? false;

            if (result)
            {
                Label statusLabel = (Label)element.Children[1];

                statusLabel.Content = !isEmpty ? "Свободна" : "Занята";

                if (isEmpty)
                {
                    element.Background = Brushes.OrangeRed;
                    element.Tag = 1;
                }
                else
                {
                    element.Background = Brushes.GreenYellow;
                    element.Tag = 0;
                }
            }
        }

        public bool BookingAllowed { get; set; }

        private void UIElement_OnGotFocus(object sender, RoutedEventArgs e)
        {
            this.OperationsGrid.ItemsSource = OperationsKeeper.Instance.GetHistory();
        }
    }
}
