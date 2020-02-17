using CommandManagement.Domains.AccessControl.Projections;
using CommandManagement.Domains.Devices.Events;
using CommandManagement.Domains.Devices.ValueObjects;
using Core.Aggregates;
using System;
namespace CommandManagement.Domains.AccessControl
{
    internal class Device: Aggregate
    {
        public DeviceInfo Data { get; private set; }
        public DateTime Created { get; private set; }
        public Device()
        {

        }
        public static Device Create(Guid id, DeviceInfo data)
        {
            return new Device(id, data, DateTime.UtcNow);
        }
        public Device(Guid id, DeviceInfo data, DateTime created)
        {
            if (id == Guid.Empty)
                throw new ArgumentException($"{nameof(id)} cannot be empty.");

            if (data == null)
                throw new ArgumentException($"{nameof(data)} cannot be empty.");

            var @event = DeviceCreated.Create(id, data, created);

            Enqueue(@event);
            Apply(@event);
        }
        public void Apply(DeviceCreated @event)
        {
            Id = @event.DeviceId;
            Data = @event.Data;
            Created = @event.Created;
        }
        internal void Update(Guid id, DeviceInfo data)
        {
            var @event = DeviceUpdated.Create(id, data);
            Enqueue(@event);
            Apply(@event);
        }
        private void Apply(DeviceUpdated @event)
        {
            Data = @event.Data;
        }
    }
}
