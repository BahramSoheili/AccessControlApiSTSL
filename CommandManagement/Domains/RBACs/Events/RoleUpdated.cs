using Core.Events;
using CommandManagement.Domains.RBACs.ValueObjects;
using CommandManagement.Domains.RBACs.Events;
using System;
using System.Collections.Generic;

namespace CommandManagement.Domains.RBACs.Events
{
    public class RoleUpdated : IExternalEvent
    {
        public Guid RoleId { get; }
        public RoleInfo Data { get; }
        public List<RoleOperation> Operations { get; }
        public RoleUpdated(Guid roleId, RoleInfo data, List<RoleOperation> operations)
        {
            RoleId = roleId;
            Data = data;
            Operations = operations;
        }
        public static RoleUpdated Create(Guid roleId, RoleInfo data, List<RoleOperation> operations)
        {
            if (roleId == default(Guid))
                throw new ArgumentException($"{nameof(roleId)} needs to be defined.");

            if (data == null)
                throw new ArgumentException($"data can't be empty.");

            return new RoleUpdated(roleId, data, operations);
        }
    }
}

