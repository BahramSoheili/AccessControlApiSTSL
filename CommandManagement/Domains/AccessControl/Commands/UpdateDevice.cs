using Core.Commands;
using System;
using CommandManagement.Domains.AccessControl.ValueObjects;
using CommandManagement.Domains.Devices.ValueObjects;

namespace CommandManagement.Domains.Devices.Commands
{
    public class UpdateDevice: ICommand
    {
        public Guid Id { get; }
        public DeviceInfo Data { get; }
        public UpdateDevice(Guid id, DeviceInfo data)
        {
            Id = id;
            Data = data;
        }
    }
}
