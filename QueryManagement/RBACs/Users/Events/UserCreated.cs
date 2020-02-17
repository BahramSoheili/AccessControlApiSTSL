using Core.Events;
using DevicesSearch.RBACs.Roles.SearchObjects;
using DevicesSearch.RBACs.Users.SearchObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace DevicesSearch.RBACs.Users.Events
{
    internal class UserCreated : IEvent
    {
        public Guid UserId { get; }
        public UserData Data { get; }
        public UserRoleData Role { get; }


        public UserCreated(Guid userId, UserData data, UserRoleData role)
        {
            UserId = userId;
            Data = data;
            Role = role;
        }
    }
}
