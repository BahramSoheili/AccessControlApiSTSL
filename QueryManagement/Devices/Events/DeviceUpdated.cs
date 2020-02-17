using Core.Events;
using DevicesSearch.Devices.SearchObjects;
using System;


namespace DevicesSearch.Devices.Events
{
    public class DeviceUpdated : IEvent
    {
        public Guid DeviceId { get; }
        public DeviceData Data { get; }
        public DeviceUpdated(Guid deviceId, DeviceData data)
        {
            DeviceId = deviceId;
            Data = data;
        }
        public static DeviceUpdated Create(Guid deviceId, DeviceData data)
        {
            if (deviceId == default(Guid))
                throw new ArgumentException($"{nameof(deviceId)} needs to be defined.");

            if (data == null)
                throw new ArgumentException($"Data can't be empty.");

            return new DeviceUpdated(deviceId, data);
        }
    }
}
