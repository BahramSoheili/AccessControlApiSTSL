using Core.Events;
using CommandManagement.Domains.RBACs.ValueObjects;
using System;

namespace CommandManagement.Domains.RBACs.Events
{
    public class OperationCreated: IExternalEvent
    {
        public Guid OperationId { get; }
        public OperationInfo Data { get; }
        public DateTime Created { get; }

        public OperationCreated(Guid operationId, OperationInfo data, DateTime created)
        {
            OperationId = operationId;
            Data = data;
            Created = created;
        }

        public static OperationCreated Create(Guid operationId, OperationInfo data, DateTime created)
        {
            if (operationId == default(Guid))
                throw new ArgumentException($"{nameof(operationId)} needs to be defined.");

            if (created == default(DateTime))
                throw new ArgumentException($"{nameof(created)} needs to be defined.");

            if (data == null)
                throw new ArgumentException($"data can't be empty.");

            return new OperationCreated(operationId, data, created);
        }
    }
}
