using Core.Queries;
using CommandManagement.Domains.RBACs.Views;
using System;
namespace CommandManagement.Domains.RBACs.Queries
{
    public class GetUser: IQuery<UserView>
    {
        public Guid Id { get; }
        public GetUser(Guid id)
        {
            Id = id;
        }
    }
}
