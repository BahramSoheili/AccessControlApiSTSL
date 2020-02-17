using Core.Events;
using DevicesSearch.RBACs.Roles.SearchObjects;
using DevicesSearch.RBACs.Users.SearchObjects;
using System;
using System.Collections.Generic;

namespace DevicesSearch.RBACs.Users.Events
{
    public class UserUpdated : IEvent
    {
        public Guid UserId { get; }
        public UserData Data { get; }
        public UserRoleData Role { get; }
        public UserUpdated(Guid userId, UserData data, UserRoleData role)
        {
            UserId = userId;
            Data = data;
            Role = role;
        }
        public static UserUpdated Create(Guid userId, UserData data, UserRoleData role)
        {
            if (userId == default(Guid))
                throw new ArgumentException($"{nameof(userId)} needs to be defined.");

            if (data == null)
                throw new ArgumentException($"Data can't be empty.");

            return new UserUpdated(userId, data, role);
        }
    }
}
