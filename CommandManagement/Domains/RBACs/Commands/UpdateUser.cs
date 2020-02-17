using Core.Commands;
using CommandManagement.Domains.RBACs.ValueObjects;
using System;

namespace CommandManagement.Domains.RBACs.Commands
{
    public class UpdateUser: ICommand
    {
        public Guid Id { get; }
        public UserInfo Data { get; }
        public UserRole Role { get; }
        public UpdateUser(Guid id, UserInfo data, UserRole role)
        {
            Id = id;
            Data = data;
            Role = role;
        }
    }
}
