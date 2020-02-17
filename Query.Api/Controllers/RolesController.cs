using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Queries;
using DevicesSearch.RBACs.Roles;
using DevicesSearch.RBACs.Roles.Queries;
using Microsoft.AspNetCore.Mvc;
namespace DevicesSearch.Api.Controllers
{
    [Route("api/[controller]")]
    public class RolesController : Controller
    {
        private readonly IQueryBus _queryBus;
        public RolesController(IQueryBus queryBus)
        {
            _queryBus = queryBus;
        }
        [HttpGet]
        public Task<IReadOnlyCollection<Role>> Search([FromQuery]string filter)
        {
            return _queryBus.Send<SearchRoles, IReadOnlyCollection<Role>>(new SearchRoles(filter));
        }
        [HttpGet("{id}")]
        public Task<Role> SearchById(Guid Id)
        {
            return _queryBus.Send<SearchRoleById, Role>(new SearchRoleById(Id));
        }
    }
}
