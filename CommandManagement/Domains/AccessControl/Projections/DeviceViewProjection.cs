using CommandManagement.Domains.AccessControl.Projections;
using CommandManagement.Domains.AccessControl.View;
using CommandManagement.Domains.Devices.Events;
using Marten.Events.Projections;
using System;
namespace CommandManagement.Domains.Devices.Projections
{
    public class DeviceViewProjection: ViewProjection<DeviceView, Guid>
    {
        public DeviceViewProjection()
        {
            ProjectEvent<DeviceCreated>(e => e.DeviceId, Apply);
            ProjectEvent<DeviceUpdated>(e => e.DeviceId, Apply);
        }
        private void Apply(DeviceView view, DeviceCreated @event)
        {
            view.Id = @event.DeviceId;
            view.Data = @event.Data;
            view.Created = @event.Created;
        }
        private void Apply(DeviceView view, DeviceUpdated @event)
        {
            view.Id = @event.DeviceId;
            view.Data = @event.Data;
        }
    }
}
