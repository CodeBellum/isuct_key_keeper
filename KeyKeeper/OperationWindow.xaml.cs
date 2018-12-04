using System.Windows;

namespace KeyKeeper
{
    /// <summary>
    /// Interaction logic for OperationWindow.xaml
    /// </summary>
    public partial class OperationWindow : Window
    {
        private Operation operationInstance;

        private bool hasCard = false;

        public OperationWindow(Operation operation)
        {
            this.operationInstance = operation;
            InitializeComponent();

            var window = new KeyEmulationWindow();
            window.Done += this.Window_Done;
            this.hasCard = (bool)window.ShowDialog();
            
            this.Title = operation.Type == OperationType.GetKey ? "Выдача ключа" : "Возврат ключа";
            this.OperationLabel.Content = operation.Type == OperationType.GetKey ? "Взять ключ" : "Вернуть ключ";
            this.RoomLabel.Content = operation.RoomId;
            this.OperationNameButton.Content = operation.Type == OperationType.GetKey ? "Выдать ключ" : "Принять ключ";
            this.StatusLabel.Content = operation.Type == OperationType.GetKey ? "Свободна" : "Занята";
            this.RejectButton.IsEnabled = operation.Type == OperationType.GetKey;

            if (!this.hasCard)
            {
                this.tbComment.Text = "Нет карточки";
            }
        }

        private void Window_Done(object sender, string e)
        {
            this.tbUser.Text = e;
            this.tbUser.IsReadOnly = true;
        }

        private void OperationNameButton_OnClick(object sender, RoutedEventArgs e)
        {
            Operation operation = new Operation(
                this.operationInstance.RoomId, 
                this.tbUser.Text,
                this.operationInstance.Type,
                true,
                this.tbComment.Text);

            OperationsKeeper.Instance.AddOperation(operation);
            this.DialogResult = true;
            this.Close();
        }

        private void RejectButton_OnClick(object sender, RoutedEventArgs e)
        {
            Operation operation = new Operation(
                this.operationInstance.RoomId, 
                this.tbUser.Text,
                this.operationInstance.Type, 
                false,
                this.tbComment.Text);

            OperationsKeeper.Instance.AddOperation(operation);
            this.DialogResult = false;
            this.Close();
        }
    }
}
