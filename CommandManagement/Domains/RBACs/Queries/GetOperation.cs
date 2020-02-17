using Core.Queries;
using CommandManagement.Domains.RBACs.Views;
using System;
namespace CommandManagement.Domains.RBACs.Queries
{
    public class GetOperation : IQuery<OperationView>
    {
        public Guid Id { get; }
        public GetOperation(Guid id)
        {
            Id = id;
        }
    }
}