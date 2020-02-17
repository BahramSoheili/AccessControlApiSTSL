using Core.Commands;
using CommandManagement.Domains.RBACs.ValueObjects;
using System;

namespace CommandManagement.Domains.RBACs.Commands
{
    public class AssignRoleOperation : ICommand
    {
        public Guid RoleId { get; }
        public RoleOperation Data { get; }

        public AssignRoleOperation(Guid id, RoleOperation data)
        {
            RoleId = id;
            Data = data;
        }
    }
}
