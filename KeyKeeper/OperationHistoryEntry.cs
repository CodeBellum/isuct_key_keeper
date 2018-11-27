using System;

namespace KeyKeeper
{
    public class OperationHistoryEntry
    {
        public OperationHistoryEntry(int id, Operation operation, string admin)
        {
            this.RoomId = operation.RoomId;
            this.User = operation.User;
            this.Type = operation.Type == OperationType.GetKey ? "Взял ключ" : "Вернул ключ";
            this.OperationDate = operation.OperationDate;
            this.Id = id;
            this.Admin = admin;
        }

        public int Id { get; }

        public string User { get; }

        public string Type { get; }

        public int RoomId { get; }

        public DateTime OperationDate { get; }

        public string Admin { get; }
    }
}