using Core.Queries;
using System;

namespace DevicesSearch.Devices.Queries
{
    public class SearchDeviceById : IQuery<Device>
    {
        public Guid Id { get; }

        public SearchDeviceById(Guid id)
        {
            Id = id;
        }
    }
}

