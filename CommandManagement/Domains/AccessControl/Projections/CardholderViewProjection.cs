using CommandManagement.Domains.AccessControl.Events;
using CommandManagement.Domains.AccessControl.View;
using CommandManagement.Domains.AccessControl.Views;
using CommandManagement.Domains.Devices.Events;
using Marten.Events.Projections;
using System;
namespace CommandManagement.Domains.AccessControl.Projections
{
    public class CardholderViewProjection: ViewProjection<CardholderView, Guid>
    {
        public CardholderViewProjection()
        {
            ProjectEvent<CardholderCreated>(e => e.CardholderId, Apply);
            ProjectEvent<CardholderUpdated>(e => e.CardholderId, Apply);
        }
        private void Apply(CardholderView view, CardholderCreated @event)
        {
            view.Id = @event.CardholderId;
            view.Data = @event.Data;
            view.Created = @event.Created;
        }
        private void Apply(CardholderView view, CardholderUpdated @event)
        {
            view.Id = @event.CardholderId;
            view.Data = @event.Data;
        }
    }
}
