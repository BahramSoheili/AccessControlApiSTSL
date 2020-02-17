using Core.Aggregates;
using CommandManagement.Domains.RBACs.ValueObjects;
using CommandManagement.Domains.RBACs.Events;
using System;
using System.Collections.Generic;

namespace CommandManagement.Domains.RBACs
{
    internal class Role: Aggregate
    {
        public RoleInfo Data { get; private set; }
        public DateTime Created { get; private set; }
        public List<RoleOperation> Operations { get; private set; }
        public Role()
        {
        }
        public static Role Create(Guid id, RoleInfo data, List<RoleOperation> operations )
        {
            return new Role(id, data, DateTime.UtcNow, operations);
        }
        public Role(Guid id, RoleInfo data, DateTime created, List<RoleOperation> operations)
        {
            if (id == Guid.Empty)
                throw new ArgumentException($"{nameof(id)} cannot be empty.");

            if (data == null)
                throw new ArgumentException($"{nameof(data)} cannot be empty.");

            var @event = RoleCreated.Create(id, data, created, operations);
            Enqueue(@event);
            Apply(@event);
        }
        public void Apply(RoleCreated @event)
        {
            Id = @event.RoleId;
            Data = @event.Data;
            Created = @event.Created;
            Operations = @event.Operations;
        }
        internal void Update(Guid id, RoleInfo data, List<RoleOperation> operations)
        {
            var @event = RoleUpdated.Create(id, data, operations);
            Enqueue(@event);
            Apply(@event);
        }
        private void Apply(RoleUpdated @event)
        {
            Data = @event.Data;
            Operations = @event.Operations;
        }
    }
}

