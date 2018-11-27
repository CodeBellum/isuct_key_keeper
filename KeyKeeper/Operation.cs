using System;

namespace KeyKeeper
{
    public class Operation
    {
        public Operation(int roomId, string user, OperationType type)
        {
            this.RoomId = roomId;
            this.User = user;
            this.Type = type;
            this.OperationDate = DateTime.Now;
        }

        public DateTime OperationDate { get; }

        public string User { get; }

        public OperationType Type { get; }

        public int RoomId { get; }
    }
}