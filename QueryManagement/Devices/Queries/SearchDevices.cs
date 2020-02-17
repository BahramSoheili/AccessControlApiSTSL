using Core.Queries;
using System.Collections.Generic;

namespace DevicesSearch.Devices.Queries
{
    public class SearchDevices : IQuery<IReadOnlyCollection<Device>>
    {
        public string Filter { get; }

        public SearchDevices(string filter)
        {
            Filter = filter;
        }
    }
}

