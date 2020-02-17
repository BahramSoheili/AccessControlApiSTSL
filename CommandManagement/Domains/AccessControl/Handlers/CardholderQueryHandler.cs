using Core.Queries;
using Marten;
using System;
using System.Threading;
using System.Threading.Tasks;
using CommandManagement.Domains.AccessControl.Queries;
using CommandManagement.Domains.AccessControl.Views;

namespace CommandManagement.Domains.AccessControl.Handlers
{
    public class CardholderQueryHandler: IQueryHandler<GetCardholder, CardholderView>
    {
        private readonly IDocumentSession session;

        public CardholderQueryHandler(
            IDocumentSession session
        )
        {
            this.session = session ?? throw new ArgumentNullException(nameof(session));
        }

        public Task<CardholderView> Handle(GetCardholder request, CancellationToken cancellationToken)
        {
            return session.LoadAsync<CardholderView>(request.Id, cancellationToken);
        }
    }
}
