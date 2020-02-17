using CommandManagement.Domains.RBACs.Views;
using Marten.Events.Projections;
using System;
using CommandManagement.Domains.RBACs.Events;
namespace CommandManagement.Domains.RBACs.Projections
{
    public class UserViewProjection: ViewProjection<UserView, Guid>
    {
        public UserViewProjection()
        {
            ProjectEvent<UserCreated>(e => e.UserId, Apply);
            ProjectEvent<UserUpdated>(e => e.UserId, Apply);
        }
        private void Apply(UserView view, UserCreated @event)
        {
            view.Id = @event.UserId;
            view.Data = @event.Data;
            view.Created = @event.Created;
            view.Role = @event.Role;
        }
        private void Apply(UserView view, UserUpdated @event)
        {
            view.Id = @event.UserId;
            view.Data = @event.Data;
            view.Role = @event.Role;
        }
    }
}
