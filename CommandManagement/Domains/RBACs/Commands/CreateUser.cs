using Core.Commands;
using CommandManagement.Domains.RBACs.ValueObjects;
using System;
using System.Collections.Generic;

namespace CommandManagement.Domains.RBACs.Commands
{
    public class CreateUser: ICommand
    {
        public Guid Id { get; }
        public UserInfo Data { get; }
        public UserRole Role { get; }
        public CreateUser(Guid id, UserInfo data, UserRole role)
        {
             Id = id;
             Data = data;
             Role = role;
        }
    }
}
