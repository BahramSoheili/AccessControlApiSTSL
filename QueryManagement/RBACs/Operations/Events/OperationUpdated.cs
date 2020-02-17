using Core.Events;
using DevicesSearch.RBACs.Operations.SearchObjects;
using System;


namespace DevicesSearch.RBACs.Operations.Events
{
    public class OperationUpdated : IEvent
    {
        public Guid OperationId { get; }
        public OperationData Data { get; }
        public OperationUpdated(Guid operationId, OperationData data)
        {
            OperationId = operationId;
            Data = data;
        }
        public static OperationUpdated Create(Guid operationId, OperationData data)
        {
            if (operationId == default(Guid))
                throw new ArgumentException($"{nameof(operationId)} needs to be defined.");

            if (data == null)
                throw new ArgumentException($"Data can't be empty.");

            return new OperationUpdated(operationId, data);
        }
    }
}
