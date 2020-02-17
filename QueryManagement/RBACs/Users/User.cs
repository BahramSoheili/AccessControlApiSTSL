using Core.Aggregates;
using DevicesSearch.RBACs.Roles.SearchObjects;
using DevicesSearch.RBACs.Users.Events;
using DevicesSearch.RBACs.Users.SearchObjects;
using Nest;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace DevicesSearch.RBACs.Users
{
    public class User: Aggregate
    {
        public UserData Data { get; protected set; }
        public UserRoleData Role { get; protected set; }
        public User()
        {
        }
        [JsonConstructor]
        public User(Guid id, UserData data, UserRoleData role)
        {
            Id = id;
            Data = data;
            Role = role;
        }
        public void Update(Guid id, UserData data, UserRoleData role)
        {
            var @event = UserUpdated.Create(id, data, role);
            Apply(@event);
        }
        private void Apply(UserUpdated @event)
        {
            Data = @event.Data;
            Role = @event.Role;
        }
        public User WithoutPassword()
        {
            this.Data.password = null;
            return this;
        }

        public static implicit operator GetResponse<object>(User v)
        {
            throw new NotImplementedException();
        }
    }
}
