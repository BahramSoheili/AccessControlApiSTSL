using CommandManagement.Domains.RBACs.Views;
using Marten.Events.Projections;
using System;
using CommandManagement.Domains.RBACs.Events;
namespace CommandManagement.Domains.RBACs.Projections
{
    public class RoleViewProjection: ViewProjection<RoleView, Guid>
    {
        public RoleViewProjection()
        {
            ProjectEvent<RoleCreated>(e => e.RoleId, Apply);
            ProjectEvent<RoleUpdated>(e => e.RoleId, Apply);
        }
        private void Apply(RoleView view, RoleCreated @event)
        {
            view.Id = @event.RoleId;
            view.Data = @event.Data;
            view.Created = @event.Created;
            view.Operations = @event.Operations;
        }
        private void Apply(RoleView view, RoleUpdated @event)
        {
            view.Id = @event.RoleId;
            view.Data = @event.Data;
            view.Operations = @event.Operations;
        }
    }
}
