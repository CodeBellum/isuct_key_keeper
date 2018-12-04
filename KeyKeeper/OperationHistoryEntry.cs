using System;

namespace KeyKeeper
{
    public class OperationHistoryEntry
    {
        public OperationHistoryEntry(int id, Operation operation, string admin)
        {
            this.RoomId = operation.RoomId;
            this.User = operation.User;
            if (operation.IsSuccess)
            {
                this.Type = operation.Type == OperationType.GetKey ? "Взял ключ" : "Вернул ключ";
            }
            else
            {
                this.Type = operation.Type == OperationType.GetKey ? "Отказано в выдаче ключа" : "Ошибка";
            }
            
            this.OperationDate = operation.OperationDate;
            this.Id = id;
            this.Admin = admin;

            this.Comment = operation.Comment;
        }

        public int Id { get; }

        public string User { get; }

        public string Type { get; }

        public string RoomId { get; }

        public DateTime OperationDate { get; }

        public string Admin { get; }

        public string Comment { get; }
    }
}