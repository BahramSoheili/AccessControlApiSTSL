using CommandManagement.Domains.AccessControl.Views;
using CommandManagement.Domains.Devices.Events;
using Marten.Events.Projections;
using System;
namespace CommandManagement.Domains.AccessControl.Projections
{
    public class DeviceTypeViewProjection: ViewProjection<DeviceTypeView, Guid>
    {
        public DeviceTypeViewProjection()
        {
            ProjectEvent<DeviceTypeCreated>(e => e.DeviceTypeId, Apply);
            ProjectEvent<DeviceTypeUpdated>(e => e.DeviceTypeId, Apply);
        }
        private void Apply(DeviceTypeView view, DeviceTypeCreated @event)
        {
            view.Id = @event.DeviceTypeId;
            view.Data = @event.Data;
            view.Created = @event.Created;
        }
        private void Apply(DeviceTypeView view, DeviceTypeUpdated @event)
        {
            view.Id = @event.DeviceTypeId;
            view.Data = @event.Data;
        }
    }
}

