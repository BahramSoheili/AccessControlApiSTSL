using Core.Events;
using CommandManagement.Domains.RBACs.ValueObjects;
using System;
using System.Collections.Generic;

namespace CommandManagement.Domains.RBACs.Events
{
    public class UserCreated: IExternalEvent
    {
        public Guid UserId { get; }
        public UserInfo Data { get; }
        public DateTime Created { get; }
        public UserRole Role { get; set; }


        public UserCreated(Guid userId, UserInfo data, DateTime created, UserRole role)
        {
            UserId = userId;
            Data = data;
            Created = created;
            Role = role;
        }

        public static UserCreated Create(Guid userId, UserInfo data, DateTime created, UserRole role)
        {
            if (userId == default(Guid))
                throw new ArgumentException($"{nameof(userId)} needs to be defined.");

            if (created == default(DateTime))
                throw new ArgumentException($"{nameof(created)} needs to be defined.");

            if (data == null)
                throw new ArgumentException($"data can't be empty.");

            return new UserCreated(userId, data, created, role);
        }
    }
}

