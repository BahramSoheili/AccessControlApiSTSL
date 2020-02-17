using Core.Events;
using DevicesSearch.RBACs.Roles.SearchObjects;
using System;
using System.Collections.Generic;

namespace DevicesSearch.RBACs.Roles.Events
{
    public class RoleUpdated : IEvent
    {
        public Guid RoleId { get; }
        public RoleData Data { get; }
        public List<RoleOperationData> Operations { get; }
        public RoleUpdated(Guid roleId, RoleData data, List<RoleOperationData> operations)
        {
            RoleId = roleId;
            Data = data;
            Operations = operations;
        }
        public static RoleUpdated Create(Guid roleId, RoleData data, List<RoleOperationData> operations)
        {
            if (roleId == default(Guid))
                throw new ArgumentException($"{nameof(roleId)} needs to be defined.");

            if (data == null)
                throw new ArgumentException($"Data can't be empty.");

            return new RoleUpdated(roleId, data, operations);
        }
    }
}
