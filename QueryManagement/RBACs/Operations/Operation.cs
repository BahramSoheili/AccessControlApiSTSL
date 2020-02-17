using Core.Aggregates;
using DevicesSearch.RBACs.Operations.Events;
using DevicesSearch.RBACs.Operations.SearchObjects;
using Newtonsoft.Json;
using System;

namespace DevicesSearch.RBACs.Operations
{
    public class Operation: Aggregate
    {
        public OperationData Data { get; protected set; }

        public Operation()
        {
        }

        [JsonConstructor]
        public Operation(Guid id, OperationData data)
        {
            Id = id;
            Data = data;
        }
        public void Update(Guid id, OperationData data)
        {
            var @event = OperationUpdated.Create(id, data);
            Apply(@event);
        }
        private void Apply(OperationUpdated @event)
        {
            Data = @event.Data;
        }
    }
}
