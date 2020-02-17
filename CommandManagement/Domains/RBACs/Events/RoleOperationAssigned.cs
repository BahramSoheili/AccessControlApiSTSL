using Core.Events;
using CommandManagement.Domains.RBACs.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace CommandManagement.Domains.RBACs.Events
{
    class RoleOperationAssigned : IExternalEvent
    {
        public Guid RoleId { get; }
        public List<RoleOperation> Operations { get; }

        public RoleOperationAssigned(Guid roleId, List<RoleOperation> operations)
        {
            RoleId = roleId;
            Operations = operations;
        }

        public static RoleOperationAssigned Create(Guid roleId, List<RoleOperation> operations)
        {
            if (roleId == default(Guid))
                throw new ArgumentException($"{nameof(roleId)} needs to be defined.");

            if (operations == default(List<RoleOperation>))
                throw new ArgumentException($"{nameof(operations)} needs to be defined.");

            return new RoleOperationAssigned(roleId, operations);
        }
    }
}
