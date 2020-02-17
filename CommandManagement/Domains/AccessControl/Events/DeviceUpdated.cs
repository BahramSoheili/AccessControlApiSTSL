using Core.Events;
using CommandManagement.Domains.Devices.ValueObjects;
using System;
namespace CommandManagement.Domains.Devices.Events
{
    public class DeviceUpdated : IExternalEvent
    {
        public Guid DeviceId { get; }
        public DeviceInfo Data { get; }
        public DeviceUpdated(Guid deviceId, DeviceInfo data)
        {
            DeviceId = deviceId;
            Data = data;
        }
        public static DeviceUpdated Create(Guid deviceId, DeviceInfo data)
        {
            if (deviceId == default(Guid))
                throw new ArgumentException($"{nameof(deviceId)} needs to be defined.");

            if (data == null)
                throw new ArgumentException($"data can't be empty.");

            return new DeviceUpdated(deviceId, data);
        }
    }
}

