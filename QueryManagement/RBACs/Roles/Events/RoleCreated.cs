using Core.Events;
using DevicesSearch.RBACs.Roles.SearchObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace DevicesSearch.RBACs.Roles.Events
{
    internal class RoleCreated : IEvent
    {
        public Guid RoleId { get; }
        public RoleData Data { get; }
        public List<RoleOperationData> Operations { get; }
        public RoleCreated(Guid roleId, RoleData data, List<RoleOperationData> operations)
        {
            RoleId = roleId;
            Data = data;
            Operations = operations;
        }
    }
}
