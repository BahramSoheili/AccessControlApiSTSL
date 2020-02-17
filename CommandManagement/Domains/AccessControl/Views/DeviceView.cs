using CommandManagement.Domains.Devices.ValueObjects;
using System;
namespace CommandManagement.Domains.AccessControl.View
{
    public class DeviceView
    {
        public Guid Id { get; set; }
        public DeviceInfo Data { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Start { get; set; }
        public DateTime? End { get; set; }
    }
}
