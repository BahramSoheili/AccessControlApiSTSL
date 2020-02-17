using System;
using System.Threading.Tasks;
using CommandManagement.Domains.AccessControl.View;
using CommandManagement.Domains.Devices.Commands;
using CommandManagement.Domains.Devices.Queries;
using CommandManagement.Domains.Devices.ValueObjects;
using Core.Commands;
using Core.Queries;

using Microsoft.AspNetCore.Mvc;

namespace CommandManagement.Api.Controllers
{
    [Route("api/[controller]")]
    public class DevicesController: Controller
    {
        private readonly ICommandBus _commandBus;
        private readonly IQueryBus _queryBus;

        public DevicesController(ICommandBus commandBus, IQueryBus queryBus)
        {
            _commandBus = commandBus;
            _queryBus = queryBus;
        }

        [HttpGet("{id}")]
        public Task<DeviceView> Get(Guid id)
        {
            return _queryBus.Send<GetDevice, DeviceView>(new GetDevice(id));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]DeviceInfo command)
        {
            var id = Guid.NewGuid();
            var device = new CreateDevice(id, command);
            await _commandBus.Send(device);
            return Created("api/Devices", id);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody]DeviceInfo deviceInfo)
        {
            await _commandBus.Send(new UpdateDevice(id, deviceInfo));
            return Ok();
        }
    }
}