using Core.Aggregates;
using CommandManagement.Domains.RBACs.Events;
using CommandManagement.Domains.RBACs.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace CommandManagement.Domains.RBACs
{
    internal class Operation: Aggregate
    {
        public OperationInfo Data { get; private set; }
        public DateTime Created { get; private set; }

        public Operation()
        {
        }
        public static Operation Create(Guid id, OperationInfo data)
        {
            return new Operation(id, data, DateTime.UtcNow);
        }
        public Operation(Guid id, OperationInfo data, DateTime created)
        {
            if (id == Guid.Empty)
                throw new ArgumentException($"{nameof(id)} cannot be empty.");

            if (data == null)
                throw new ArgumentException($"{nameof(data)} cannot be empty.");

            var @event = OperationCreated.Create(id, data, created);

            Enqueue(@event);
            Apply(@event);
        }
        public void Apply(OperationCreated @event)
        {
            Id = @event.OperationId;
            Data = @event.Data;
            Created = @event.Created;
        }
        internal void Update(Guid id, OperationInfo data)
        {
            var @event = OperationUpdated.Create(id, data);

            Enqueue(@event);
            Apply(@event);
        }
        private void Apply(OperationUpdated @event)
        {
            Data = @event.Data;
        }
    }
}