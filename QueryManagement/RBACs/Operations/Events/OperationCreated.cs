using Core.Events;
using DevicesSearch.RBACs.Operations.SearchObjects;
using System;
namespace DevicesSearch.RBACs.Operations.Events
{
    internal class OperationCreated : IEvent
    {
        public Guid OperationId { get; }
        public OperationData Data { get; }
        public OperationCreated(Guid operationId, OperationData data)
        {
            OperationId = operationId;
            Data = data;
        }
    }
}
