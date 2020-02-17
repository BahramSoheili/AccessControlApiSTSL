using Core.Commands;
using System;
using CommandManagement.Domains.AccessControl.ValueObjects;

namespace CommandManagement.Domains.Devices.Commands
{
    public class CreateDeviceType: ICommand
    {
        public Guid Id { get; }
        public DeviceTypeInfo Data { get; }

        public CreateDeviceType(Guid id, DeviceTypeInfo data)
        {
            Id = id;
            Data = data;
        }
    }
}
