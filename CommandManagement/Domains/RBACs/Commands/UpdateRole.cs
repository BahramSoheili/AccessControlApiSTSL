using Core.Commands;
using CommandManagement.Domains.RBACs.ValueObjects;
using System;
using System.Collections.Generic;

namespace CommandManagement.Domains.RBACs
{
    public class UpdateRole: ICommand
    {
        public Guid Id { get; }
        public RoleInfo Data { get; }
        public List<RoleOperation> Operations { get; }
        public UpdateRole(Guid id, RoleInfo data, List<RoleOperation> operations )
        {
            Id = id;
            Data = data;
            Operations = operations;
        }
    }
}
