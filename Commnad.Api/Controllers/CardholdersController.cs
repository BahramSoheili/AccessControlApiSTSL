using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommandManagement.Domains.AccessControl.Commands;
using CommandManagement.Domains.AccessControl.Queries;
using CommandManagement.Domains.AccessControl.ValueObjects;
using CommandManagement.Domains.AccessControl.Views;
using CommandManagement.Domains.Devices.Commands;
using Core.Commands;
using Core.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Command.Api.Controllers
{
    [Route("api/[controller]")]
    public class CardholdersController: Controller
    {
        private readonly ICommandBus _commandBus;
        private readonly IQueryBus _queryBus;

        public CardholdersController(ICommandBus commandBus, IQueryBus queryBus)
        {
            _commandBus = commandBus;
            _queryBus = queryBus;
        }

        [HttpGet("{id}")]
        public Task<CardholderView> Get(Guid id)
        {
            return _queryBus.Send<GetCardholder, CardholderView>(new GetCardholder(id));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]CardholderInfo command)
        {
            var id = Guid.NewGuid();
            var device = new CreateCardholder(id, command);
            await _commandBus.Send(device);
            return Created("api/Cardholders", id);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody]CardholderInfo deviceInfo)
        {
            await _commandBus.Send(new UpdateCardholder(id, deviceInfo));
            return Ok();
        }
    }
}