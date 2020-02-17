using Core.Events;
using DevicesSearch.Devices.SearchObjects;
using System;

namespace DevicesSearch.Devices.Events
{
    internal class DeviceCreated : IEvent
    {
        public Guid DeviceId { get; }
        public DeviceData Data { get; }

        public DeviceCreated(Guid deviceId, DeviceData data)
        {
            DeviceId = deviceId;
            Data = data;
        }
    }
}
