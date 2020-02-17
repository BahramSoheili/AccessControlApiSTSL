using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Queries;
using DevicesSearch.Devices;
using DevicesSearch.Devices.Queries;
using Microsoft.AspNetCore.Mvc;

namespace DevicesSearch.Api.Controllers
{
    [Route("api/[controller]")]
    public class DevicesController : Controller
    {
        private readonly IQueryBus _queryBus;
        public DevicesController(IQueryBus queryBus)
        {
            _queryBus = queryBus;
        }
        [HttpGet]
        public Task<IReadOnlyCollection<Device>> Search([FromQuery]string filter)
        {
            return _queryBus.Send<SearchDevices, IReadOnlyCollection<Device>>(new SearchDevices(filter));
        }
        [HttpGet("{id}")]
        public Task<Device> SearchById(Guid Id)
        {
            return _queryBus.Send<SearchDeviceById, Device>(new SearchDeviceById(Id));
        }
    }
}
