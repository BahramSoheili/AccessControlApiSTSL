using Core.Queries;
using CommandManagement.Domains.RBACs.Views;
using CommandManagement.Domains.RBACs.Queries;
using Marten;
using System;
using System.Threading;
using System.Threading.Tasks;
namespace CommandManagement.Domains.RBACs.Handlers
{
    class OperationQueryHandler: IQueryHandler<GetOperation, OperationView>
    {
        private readonly IDocumentSession session;
        public OperationQueryHandler(
            IDocumentSession session
        )
        {
            this.session = session ?? throw new ArgumentNullException(nameof(session));
        }
        public Task<OperationView> Handle(GetOperation request, CancellationToken cancellationToken)
        {
            return session.LoadAsync<OperationView>(request.Id, cancellationToken);
        }
    }
}
