using Core.Events;
using CommandManagement.Domains.RBACs.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace CommandManagement.Domains.RBACs.Events
{
    public class OperationUpdated : IExternalEvent
    {
        public Guid OperationId { get; }
        public OperationInfo Data { get; }

        public OperationUpdated(Guid operationId, OperationInfo data)
        {
            OperationId = operationId;
            Data = data;
        }

        public static OperationUpdated Create(Guid operationId, OperationInfo data)
        {
            if (operationId == default(Guid))
                throw new ArgumentException($"{nameof(operationId)} needs to be defined.");     
            if (data == null)
                throw new ArgumentException($"data can't be empty.");
            return new OperationUpdated(operationId, data);
        }
    }
}
