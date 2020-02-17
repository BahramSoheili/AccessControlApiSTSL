using Core.Queries;
using System;
namespace DevicesSearch.RBACs.Roles.Queries
{
    public class SearchRoleById : IQuery<Role>
    {
        public Guid Id { get; }
        public SearchRoleById(Guid id)
        {
            Id = id;
        }
    }
}

