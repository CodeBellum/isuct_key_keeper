using System;

namespace KeyKeeper
{
    public class Operation
    {
        public Operation(string roomId, string user, OperationType type, bool isSuccess, string comment = "")
        {
            this.RoomId = roomId;
            this.User = user;
            this.Type = type;
            this.OperationDate = DateTime.Now;
            this.IsSuccess = isSuccess;
            this.Comment = comment;
        }

        public DateTime OperationDate { get; }

        public string User { get; }

        public OperationType Type { get; }

        public string RoomId { get; }

        public bool IsSuccess { get; }

        public string Comment { get; }
    }
}