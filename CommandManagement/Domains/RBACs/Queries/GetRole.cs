using Core.Queries;
using CommandManagement.Domains.RBACs.Views;
using System;
namespace CommandManagement.Domains.RBACs.Queries
{
    public class GetRole: IQuery<RoleView>
    {
        public Guid Id { get; }
        public GetRole(Guid id)
        {
            Id = id;
        }
    }
}
