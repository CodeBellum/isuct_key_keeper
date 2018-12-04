using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Predefined
{
    /// <summary>
    /// Interaction logic for RoomsGrid.xaml
    /// </summary>
    public partial class RoomsGrid : UserControl
    {
        public RoomsGrid()
        {
            InitializeComponent();
        }

        public event EventHandler<MouseButtonEventArgs> Clicked;

        public Func<int, Label> CreateNumLabel = i => new Label();

        public Func<bool, Label> CreateLabel = i => new Label();

        private Dictionary<int, UIElement> states = new Dictionary<int, UIElement>();

        private void FrameworkElement_OnInitialized(object sender, EventArgs e)
        {
            this.InitializeFloor(1);

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

        private void Element_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (this.Clicked != null)
            {
                this.Clicked(sender, e);
            }
        }

        public void InitializeFloor(int floorId)
        {
            this.Rooms.RowDefinitions.Clear();
            while (this.Rooms.RowDefinitions.Count < PredefinedRooms.RoomsByFloors[floorId])
            {
                this.Rooms.RowDefinitions.Add(new RowDefinition());
            }
        }
    }
}
