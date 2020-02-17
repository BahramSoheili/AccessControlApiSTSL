using Core.Aggregates;
using DevicesSearch.RBACs.Roles.Events;
using DevicesSearch.RBACs.Roles.SearchObjects;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace DevicesSearch.RBACs.Roles
{
    public class Role: Aggregate
    {
        public RoleData Data { get; protected set; }
        public List<RoleOperationData> Operations { get; protected set; }
        public Role()
        {
        }
        [JsonConstructor]
        public Role(Guid id, RoleData data, List<RoleOperationData> operations)
        {
            Id = id;
            Data = data;
        }
        public void Update(Guid id, RoleData data, List<RoleOperationData> operations)
        {
            var @event = RoleUpdated.Create(id, data, operations);
            Apply(@event);
        }
        private void Apply(RoleUpdated @event)
        {
            Data = @event.Data;
            Operations = @event.Operations;
        }
    }
}
