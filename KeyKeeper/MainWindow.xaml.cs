using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Predefined;

namespace KeyKeeper
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Dictionary<string, StackPanel> states;

        private Dictionary<string, bool> logicalStates;

        public static string AdminLogin { get; private set; }
       
        public MainWindow(string adminLogin)
        {
            AdminLogin = adminLogin;
            this.states = new Dictionary<string, StackPanel>();
            this.logicalStates = new Dictionary<string, bool>();
            InitializeComponent();
        }

        public string SearchFilter { get; set; }

        private void FrameworkElement_OnInitialized(object sender, EventArgs e)
        {
            int floor = this.cmbFloor.SelectedIndex + 1;
            this.RenderFloor(floor);
            this.cmbFloor.SelectionChanged += this.CmbFloor_OnSelectionChanged;
            this.rbFree.Checked += this.RbFree_OnChecked;
            this.rbAll.Checked += this.RbAll_OnChecked;
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
            res.Content = "A" + num;
            return res;
        }

        private void Element_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            StackPanel element = (StackPanel)sender;
            bool isEmpty = (int)element.Tag == 0;
            string roomName = this.states.First(kvp => kvp.Value.Equals(element)).Key;
            
            Operation operation = new Operation(
                roomName,
                isEmpty ? "Нектототамов Н.Н." : OperationsKeeper.Instance.GetLastGetKeyOperationForRoom(roomName).User, 
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
                    this.logicalStates[roomName] = false;
                }
                else
                {
                    element.Background = Brushes.GreenYellow;
                    element.Tag = 0;
                    this.logicalStates[roomName] = true;
                }
            }

            this.ApplyFilter();
        }

        private void ApplyFilter()
        {
            if (!string.IsNullOrEmpty(this.SearchFilter))
            {
                foreach (KeyValuePair<string, StackPanel> state in this.states)
                {
                    if (!state.Key.Contains(this.SearchFilter))
                    {
                        state.Value.Visibility = Visibility.Collapsed;
                    }

                    if ((bool)this.rbFree.IsChecked)
                    {
                        if (!this.logicalStates[state.Key])
                        {
                            state.Value.Visibility = Visibility.Collapsed;
                        }
                    }
                    else
                    {
                        if (state.Key.Contains(this.SearchFilter))
                        {
                            state.Value.Visibility = Visibility.Visible;
                        }
                    }
                }

                return;
            }

            if ((bool)this.rbFree.IsChecked)
            {
                this.ShowFree();
            }
            else
            {
                this.ShowAll();
            }
        }

        private void UIElement_OnGotFocus(object sender, RoutedEventArgs e)
        {
            this.OperationsGrid.ItemsSource = OperationsKeeper.Instance.GetHistory();
        }

        private void CmbFloor_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.RenderFloor(this.cmbFloor.SelectedIndex + 1);
        }

        private void RenderFloor(int floor)
        {
            this.RoomsPanel.Children.Clear();
            this.states.Clear();

            int floorAdjustment = floor * 100;

            for (int i = 0; i < PredefinedRooms.RoomsByFloors[floor]; i++)
            {
                int roomId = i + 1;
                string key = "A" + (floorAdjustment + roomId);

                bool alreadyInitialized = this.logicalStates.ContainsKey(key);

                var element = new StackPanel();
                element.Width = 70;
                element.Margin = new Thickness(5);
                element.MouseLeftButtonUp += this.Element_MouseLeftButtonUp;
                element.Children.Add(this.CreateNumLabel(floorAdjustment + roomId));

                if (alreadyInitialized)
                {
                    if (!this.logicalStates[key])
                    {
                        element.Background = Brushes.OrangeRed;
                        element.Tag = 1;
                    }
                    else
                    {
                        element.Background = Brushes.GreenYellow;
                        element.Tag = 0;
                    }

                    element.Children.Add(this.CreateLabel(this.logicalStates[key]));
                }
                else
                {
                    element.Background = Brushes.GreenYellow;
                    element.Tag = 0;
                    this.logicalStates[key] = true;
                    element.Children.Add(this.CreateLabel(true));
                }
               
                this.states[key] = element;
                this.RoomsPanel.Children.Add(element);
            }

            this.ApplyFilter();
        }

        private void RbFree_OnChecked(object sender, RoutedEventArgs e)
        {
           this.ApplyFilter();
        }

        private void ShowAll()
        {
            foreach (KeyValuePair<string, StackPanel> state in this.states)
            {
                state.Value.Visibility = Visibility.Visible;
            }
        }

        private void ShowFree()
        {
            foreach (KeyValuePair<string, StackPanel> state in this.states)
            {

                state.Value.Visibility = !this.logicalStates[state.Key]
                                             ? Visibility.Collapsed
                                             : Visibility.Visible;
            }
        }

        private void RbAll_OnChecked(object sender, RoutedEventArgs e)
        {
            this.ApplyFilter();
        }

        private void TbSearch_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                this.SearchFilter = string.Empty;
                this.ApplyFilter();
            }
        }

        private void BtnSearch_OnClick(object sender, RoutedEventArgs e)
        {
            this.SearchFilter = this.tbSearch.Text;
            this.ApplyFilter();
        }
    }
}
