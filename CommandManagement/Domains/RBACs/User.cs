using System;
using Core.Aggregates;
using CommandManagement.Domains.RBACs.ValueObjects;
using CommandManagement.Domains.RBACs.Events;
using System.Collections.Generic;

namespace CommandManagement.Domains.RBACs
{
    internal class User: Aggregate
    {
        public UserInfo Data { get; private set; }
        public DateTime Created { get; private set; }
        public UserRole Role { get; set; }
        public User()
        {
        }
        public static User Create(Guid id, UserInfo data, UserRole role)
        {
            return new User(id, data, DateTime.UtcNow, role );
        }
        public User(Guid id, UserInfo data, DateTime created, UserRole role)
        {
            if (id == Guid.Empty)
                throw new ArgumentException($"{nameof(id)} cannot be empty.");
            if (data == null)
                throw new ArgumentException($"{nameof(data)} cannot be empty.");
            var @event = UserCreated.Create(id, data, created, role);
            Enqueue(@event);
            Apply(@event);
        }
        public void Apply(UserCreated @event)
        {
            Id = @event.UserId;
            Data = @event.Data;
            Created = @event.Created;
            Role = @event.Role;
        }
        internal void Update(Guid id, UserInfo data, UserRole role)
        {
            var @event = UserUpdated.Create(id, data, role);
            Enqueue(@event);
            Apply(@event);
        }
        private void Apply(UserUpdated @event)
        {
            Data = @event.Data;
            Role = @event.Role;
        }
    }
}
