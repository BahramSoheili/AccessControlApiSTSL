using Core.Events;
using CommandManagement.Domains.RBACs.ValueObjects;
using System;
using System.Collections.Generic;

namespace CommandManagement.Domains.RBACs.Events
{
    public class RoleCreated: IExternalEvent
    {
        public Guid RoleId { get; }
        public RoleInfo Data { get; }
        public DateTime Created { get; }
        public List<RoleOperation> Operations { get; }
        public RoleCreated(Guid roleId, RoleInfo data, DateTime created, List<RoleOperation> operations)
        {
            RoleId = roleId;
            Data = data;
            Created = created;
            Operations = operations;
        }
        public static RoleCreated Create(Guid roleId, RoleInfo data, DateTime created, List<RoleOperation> operations)
        {
            if (roleId == default(Guid))
                throw new ArgumentException($"{nameof(roleId)} needs to be defined.");

            if (created == default(DateTime))
                throw new ArgumentException($"{nameof(created)} needs to be defined.");

            if (data == null)
                throw new ArgumentException($"data can't be empty.");

            return new RoleCreated(roleId, data, created, operations);
        }
    }
}

