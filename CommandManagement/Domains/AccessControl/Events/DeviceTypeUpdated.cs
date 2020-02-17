using Core.Events;
using CommandManagement.Domains.Devices.ValueObjects;
using System;
using CommandManagement.Domains.AccessControl.ValueObjects;

namespace CommandManagement.Domains.Devices.Events
{
    public class DeviceTypeUpdated: IExternalEvent
    {
        public Guid DeviceTypeId { get; }
        public DeviceTypeInfo Data { get; }
        public DeviceTypeUpdated(Guid deviceTypeId, DeviceTypeInfo data)
        {
            DeviceTypeId = deviceTypeId;
            Data = data;
        }
        public static DeviceTypeUpdated Create(Guid deviceTypeId, DeviceTypeInfo data)
        {
            if (deviceTypeId == default(Guid))
                throw new ArgumentException($"{nameof(deviceTypeId)} needs to be defined.");

            if (data == null)
                throw new ArgumentException($"data can't be empty.");

            return new DeviceTypeUpdated(deviceTypeId, data);
        }
    }
}

