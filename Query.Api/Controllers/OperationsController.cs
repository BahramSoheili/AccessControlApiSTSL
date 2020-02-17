using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Queries;
using DevicesSearch.RBACs.Operations;
using DevicesSearch.RBACs.Operations.Queries;
using Microsoft.AspNetCore.Mvc;

namespace DevicesSearch.Api.Controllers
{
    [Route("api/[controller]")]
    public class OperationsController : Controller
    {
        private readonly IQueryBus _queryBus;
        public OperationsController(IQueryBus queryBus)
        {
            _queryBus = queryBus;
        }

        [HttpGet]
        public Task<IReadOnlyCollection<Operation>> Search([FromQuery]string filter)
        {
            return _queryBus.Send<SearchOperations, IReadOnlyCollection<Operation>>(new SearchOperations(filter));
        }
        [HttpGet("{id}")]
        public Task<Operation> SearchById(Guid Id)
        {
            return _queryBus.Send<SearchOperationById, Operation>(new SearchOperationById(Id));
        }

    }
}