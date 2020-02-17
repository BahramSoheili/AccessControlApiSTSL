using Core.Events;
using System;
using CommandManagement.Domains.AccessControl.ValueObjects;

namespace CommandManagement.Domains.Devices.Events
{
    public class DeviceTypeCreated : IExternalEvent
    {
        public Guid DeviceTypeId { get; }
        public DeviceTypeInfo Data { get; }
        public DateTime Created { get; }

        public DeviceTypeCreated(Guid deviceTypeId, DeviceTypeInfo data, DateTime created)
        {
            DeviceTypeId = deviceTypeId;
            Data = data;
            Created = created;
        }

        public static DeviceTypeCreated Create(Guid deviceTypeId, DeviceTypeInfo data, DateTime created)
        {
            if (deviceTypeId == default(Guid))
                throw new ArgumentException($"{nameof(deviceTypeId)} needs to be defined.");

            if (created == default(DateTime))
                throw new ArgumentException($"{nameof(created)} needs to be defined.");

            if (data == null)
                throw new ArgumentException($"data can't be empty.");

            return new DeviceTypeCreated(deviceTypeId, data, created);
        }
    }
}

