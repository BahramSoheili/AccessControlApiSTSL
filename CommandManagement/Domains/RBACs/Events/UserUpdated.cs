using Core.Events;
using CommandManagement.Domains.RBACs.ValueObjects;
using System;
using System.Collections.Generic;

namespace CommandManagement.Domains.RBACs.Events
{
    public class UserUpdated : IExternalEvent
    {
        public Guid UserId { get; }
        public UserInfo Data { get; }
        public UserRole Role { get; set; }

        public UserUpdated(Guid userId, UserInfo data, UserRole role)
        {
            UserId = userId;
            Data = data;
            Role = role;
        }

        public static UserUpdated Create(Guid userId, UserInfo data, UserRole role)
        {
            if (userId == default(Guid))
                throw new ArgumentException($"{nameof(UserId)} needs to be defined.");

            if (data == null)
                throw new ArgumentException($"data can't be empty.");

            return new UserUpdated(userId, data, role);
        }
    }
}

