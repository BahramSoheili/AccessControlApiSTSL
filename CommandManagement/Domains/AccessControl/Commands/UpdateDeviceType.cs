using Core.Commands;
using CommandManagement.Domains.Devices.ValueObjects;
using System;
using CommandManagement.Domains.AccessControl.ValueObjects;

namespace CommandManagement.Domains.Devices.Commands
{
    public class UpdateDeviceType : ICommand
    {
        public Guid Id { get; }
        public DeviceTypeInfo Data { get; }

        public UpdateDeviceType(Guid id, DeviceTypeInfo data)
        {
            Id = id;
            Data = data;
        }
    }
}
