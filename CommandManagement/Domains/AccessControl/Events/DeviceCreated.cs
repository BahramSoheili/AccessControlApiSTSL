using Core.Events;
using CommandManagement.Domains.Devices.ValueObjects;
using System;
namespace CommandManagement.Domains.AccessControl.Projections
{
    public class DeviceCreated: IExternalEvent
    {
        public Guid DeviceId { get; }
        public DeviceInfo Data { get; }
        public DateTime Created { get; }

        public DeviceCreated(Guid deviceId, DeviceInfo data, DateTime created)
        {
            DeviceId = deviceId;
            Data = data;
            Created = created;
        }

        public static DeviceCreated Create(Guid deviceId, DeviceInfo data, DateTime created)
        {
            if (deviceId == default(Guid))
                throw new ArgumentException($"{nameof(deviceId)} needs to be defined.");

            if (created == default(DateTime))
                throw new ArgumentException($"{nameof(created)} needs to be defined.");

            if (data == null)
                throw new ArgumentException($"data can't be empty.");

            return new DeviceCreated(deviceId, data, created);
        }
    }
}

