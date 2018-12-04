using System.Collections.Generic;
using System.Linq;

namespace KeyKeeper
{
    public class OperationsKeeper
    {
        private static OperationsKeeper instance;
        public static OperationsKeeper Instance
        {
            get
            {
                return instance ?? (instance = new OperationsKeeper());
            }
        }

        private Dictionary<int, Operation> storage;

        private int lastOperationId = 1;

        public void AddOperation(Operation operation)
        {
            this.storage.Add(this.lastOperationId, operation);
            this.lastOperationId++;
        }

        private OperationsKeeper()
        {
            this.storage = new Dictionary<int, Operation>();
        }

        public IEnumerable<OperationHistoryEntry> GetHistory()
        {
            return this.storage
                .Select(kvp => new OperationHistoryEntry(kvp.Key, kvp.Value, MainWindow.AdminLogin))
                .ToList();
        }

        public Operation GetLastGetKeyOperationForRoom(string roomId)
        {
            return this.storage.Last(kvp => kvp.Value.RoomId == roomId && kvp.Value.Type == OperationType.GetKey).Value;
        }
    }
}