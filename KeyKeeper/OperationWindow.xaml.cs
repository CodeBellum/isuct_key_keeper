using System.Windows;

namespace KeyKeeper
{
    /// <summary>
    /// Interaction logic for OperationWindow.xaml
    /// </summary>
    public partial class OperationWindow : Window
    {
        private Operation operationInstance;
        public OperationWindow(Operation operation)
        {
            this.operationInstance = operation;
            InitializeComponent();
            this.Title = operation.Type == OperationType.GetKey ? "Выдача ключа" : "Возврат ключа";
            this.tbUser.Text = operation.User;
            this.OperationLabel.Content = operation.Type == OperationType.GetKey ? "Взять ключ" : "Вернуть ключ";
            this.RoomLabel.Content = operation.RoomId;
            this.OperationNameButton.Content = operation.Type == OperationType.GetKey ? "Выдать ключ" : "Принять ключ";
            this.StatusLabel.Content = operation.Type == OperationType.GetKey ? "Свободна" : "Занята";
            this.RejectButton.IsEnabled = operation.Type == OperationType.GetKey;
        }

        private void OperationNameButton_OnClick(object sender, RoutedEventArgs e)
        {
            Operation operation = new Operation(this.operationInstance.RoomId, this.tbUser.Text, this.operationInstance.Type);
            OperationsKeeper.Instance.AddOperation(operation);
            this.DialogResult = true;
            this.Close();
        }

        private void RejectButton_OnClick(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
    }
}
