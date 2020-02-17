using Core.Queries;
using CommandManagement.Domains.RBACs.Views;
using CommandManagement.Domains.RBACs.Queries;
using Marten;
using System;
using System.Threading;
using System.Threading.Tasks;
namespace CommandManagement.Domains.RBACs.Handlers
{
    class RoleQueryHandler : IQueryHandler<GetRole, RoleView>
    {
        private readonly IDocumentSession session;
        public RoleQueryHandler(
            IDocumentSession session
        )
        {
            this.session = session ?? throw new ArgumentNullException(nameof(session));
        }
        public Task<RoleView> Handle(GetRole request, CancellationToken cancellationToken)
        {
            return session.LoadAsync<RoleView>(request.Id, cancellationToken);
        }
    }
}
