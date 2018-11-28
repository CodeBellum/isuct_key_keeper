using System;

namespace KeyKeeper
{
    public class Operation
    {
        public Operation(int roomId, string user, OperationType type, bool isSuccess)
        {
            this.RoomId = roomId;
            this.User = user;
            this.Type = type;
            this.OperationDate = DateTime.Now;
            this.IsSuccess = isSuccess;
        }

        public DateTime OperationDate { get; }

        public string User { get; }

        public OperationType Type { get; }

        public int RoomId { get; }

        public bool IsSuccess { get; }
    }
}