using System;
using System.Threading.Tasks;
using CommandManagement.Domains.AccessControl.Queries;
using CommandManagement.Domains.AccessControl.ValueObjects;
using CommandManagement.Domains.AccessControl.Views;
using CommandManagement.Domains.Devices.Commands;
using Core.Commands;
using Core.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Command.Api.Controllers
{
    public class DeviceTypeController : Controller
    {
        [Route("api/[controller]")]
        public class DeviceTypesController : Controller
        {
            private readonly ICommandBus _commandBus;
            private readonly IQueryBus _queryBus;

            public DeviceTypesController(ICommandBus commandBus, IQueryBus queryBus)
            {
                _commandBus = commandBus;
                _queryBus = queryBus;
            }

            [HttpGet("{id}")]
            public Task<DeviceTypeView> Get(Guid id)
            {
                return _queryBus.Send<GetDeviceType, DeviceTypeView>(new GetDeviceType(id));
            }

            [HttpPost]
            public async Task<IActionResult> Post([FromBody]DeviceTypeInfo command)
            {
                var id = Guid.NewGuid();
                var device = new CreateDeviceType(id, command);
                await _commandBus.Send(device);
                return Created("api/DeviceTypes", id);
            }

            [HttpPut("{id}")]
            public async Task<IActionResult> Put(Guid id, [FromBody]DeviceTypeInfo deviceInfo)
            {
                await _commandBus.Send(new UpdateDeviceType(id, deviceInfo));
                return Ok();
            }
        }
    }
}