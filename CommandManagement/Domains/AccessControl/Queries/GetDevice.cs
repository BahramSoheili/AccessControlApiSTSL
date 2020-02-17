using CommandManagement.Domains.AccessControl.View;
using Core.Queries;
using System;
namespace CommandManagement.Domains.Devices.Queries
{
    public class GetDevice: IQuery<DeviceView>
    {
        public Guid Id { get; }

        public GetDevice(Guid id)
        {
            Id = id;
        }
    }
}
