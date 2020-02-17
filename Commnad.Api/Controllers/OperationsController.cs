using System;
using System.Threading.Tasks;
using CommandManagement.Domains.RBACs.Commands;
using CommandManagement.Domains.RBACs.Queries;
using CommandManagement.Domains.RBACs.ValueObjects;
using CommandManagement.Domains.RBACs.Views;
using Core.Commands;
using Core.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CommandManagement.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class OperationsController : Controller
    {
        private readonly ICommandBus _commandBus;
        private readonly IQueryBus _queryBus;

        public OperationsController(ICommandBus commandBus, IQueryBus queryBus)
        {
            _commandBus = commandBus;
            _queryBus = queryBus;
        }
        [HttpGet("{id}")]
        public Task<OperationView> Get(Guid id)
        {
            return _queryBus.Send<GetOperation, OperationView>(new GetOperation(id));
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]OperationInfo command)
        {
            var id = Guid.NewGuid();
            var operation = new CreateOperation(id, command);
            await _commandBus.Send(operation);
            return Created("api/operations", id);
        }
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody]OperationInfo operationInfo)
        {
            await _commandBus.Send(new UpdateOperation(id, operationInfo));
            return Ok();
        }
    }
}