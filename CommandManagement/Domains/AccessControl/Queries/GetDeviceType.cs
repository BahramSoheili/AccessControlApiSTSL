using CommandManagement.Domains.AccessControl.Views;
using Core.Queries;
using System;

namespace CommandManagement.Domains.AccessControl.Queries
{
    public class GetDeviceType: IQuery<DeviceTypeView>
    {
        public Guid Id { get; }

        public GetDeviceType(Guid id)
        {
            Id = id;
        }
    }
}
