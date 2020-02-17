using Core.Commands;
using CommandManagement.Domains.Devices.ValueObjects;
using System;
namespace CommandManagement.Domains.Devices.Commands
{
    public class CreateDevice: ICommand
    {
        public Guid Id { get; }
        public DeviceInfo Data { get; }

        public CreateDevice(Guid id, DeviceInfo data)
        {
            Id = id;
            Data = data;
        }
    }
}
