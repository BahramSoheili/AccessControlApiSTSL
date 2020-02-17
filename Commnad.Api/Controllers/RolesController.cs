using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Commands;
using Core.Queries;
using CommandManagement.Domains.RBACs;
using CommandManagement.Domains.RBACs.Commands;
using CommandManagement.Domains.RBACs.Queries;
using CommandManagement.Domains.RBACs.ValueObjects;
using CommandManagement.Domains.RBACs.Views;
using Microsoft.AspNetCore.Mvc;

namespace CommandManagement.Api.Controllers
{
    [Route("api/[controller]")]
    public class RolesController : Controller
    {
        private readonly ICommandBus _commandBus;
        private readonly IQueryBus _queryBus;

        public RolesController(ICommandBus commandBus, IQueryBus queryBus)
        {
            _commandBus = commandBus;
            _queryBus = queryBus;
        }
        [HttpGet("{id}")]
        public Task<RoleView> Get(Guid id)
        {
            return _queryBus.Send<GetRole, RoleView>(new GetRole(id));
        }
        [HttpPost]
        /*
        {
	        "Item1":
	        {
                "roleTitle":"plplp",
                "description":"kooooooookokokok"
            },
            "Item2":
            [{
                "OperationId":"6b510c74-f2e3-469e-a8a4-04e8346bda50",
                "OperationName":"vffffffff",
                "description":"llllllllllllllll"
            }]
        }
        */
        public async Task<IActionResult> Post([FromBody](RoleInfo roleInfo, List<RoleOperation> roleOperation) command)
        {
            var id = Guid.NewGuid();
            var role = new CreateRole(id, command.roleInfo, command.roleOperation);
            await _commandBus.Send(role);
            return Created("api/roles", id);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody](RoleInfo roleInfo, List<RoleOperation> roleOperation) command)
        {
            await _commandBus.Send(new UpdateRole(id, command.roleInfo, command.roleOperation));
            return Ok();
        }
    }
}