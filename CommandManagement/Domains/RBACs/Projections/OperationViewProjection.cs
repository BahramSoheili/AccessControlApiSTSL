using CommandManagement.Domains.RBACs.Events;
using CommandManagement.Domains.RBACs.Views;
using Marten.Events.Projections;
using System;
namespace CommandManagement.Domains.RBACs.Projections
{
    public class OperationViewProjection: ViewProjection<OperationView, Guid>
    {
        public OperationViewProjection()
        {
            ProjectEvent<OperationCreated>(e => e.OperationId, Apply);
            ProjectEvent<OperationUpdated>(e => e.OperationId, Apply);
        }
        private void Apply(OperationView view, OperationCreated @event)
        {
            view.Id = @event.OperationId;
            view.Data = @event.Data;
            view.Created = @event.Created;
        }
        private void Apply(OperationView view, OperationUpdated @event)
        {
            view.Id = @event.OperationId;
            view.Data = @event.Data;
        }
    }
}
