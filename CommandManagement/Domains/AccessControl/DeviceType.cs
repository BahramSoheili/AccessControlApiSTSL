using CommandManagement.Domains.AccessControl.ValueObjects;
using CommandManagement.Domains.Devices.Events;
using Core.Aggregates;
using System;
namespace CommandManagement.Domains.AccessControl
{
    internal class DeviceType: Aggregate
    {
        public DeviceTypeInfo Data { get; private set; }
        public DateTime Created { get; private set; }
        public DeviceType()
        {

        }
        public static DeviceType Create(Guid id, DeviceTypeInfo data)
        {
            return new DeviceType(id, data, DateTime.UtcNow);
        }
        public DeviceType(Guid id, DeviceTypeInfo data, DateTime created)
        {
            if (id == Guid.Empty)
                throw new ArgumentException($"{nameof(id)} cannot be empty.");

            if (data == null)
                throw new ArgumentException($"{nameof(data)} cannot be empty.");

            var @event = DeviceTypeCreated.Create(id, data, created);

            Enqueue(@event);
            Apply(@event);
        }
        public void Apply(DeviceTypeCreated @event)
        {
            Id = @event.DeviceTypeId;
            Data = @event.Data;
            Created = @event.Created;
        }
        internal void Update(Guid id, DeviceTypeInfo data)
        {
            var @event = DeviceTypeUpdated.Create(id, data);
            Enqueue(@event);
            Apply(@event);
        }
        private void Apply(DeviceTypeUpdated @event)
        {
            Data = @event.Data;
        }
    }
}

