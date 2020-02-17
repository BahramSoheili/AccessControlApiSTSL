using Core.Queries;
using CommandManagement.Domains.RBACs.Views;
using CommandManagement.Domains.RBACs.Queries;
using Marten;
using System;
using System.Threading;
using System.Threading.Tasks;
using CommandManagement.Domains.RBACs.ValueObjects;
using System.Linq;

namespace CommandManagement.Domains.RBACs.Handlers
{
    class UserQueryHandler: 
        IQueryHandler<GetUser, UserView>,
        IQueryHandler<GetAuthenticater, UserView>
    {
        private readonly IDocumentSession session;
        public UserQueryHandler(IDocumentSession session)
        {
            this.session = session ?? throw new ArgumentNullException(nameof(session));
        }
        public Task<UserView> Handle(GetUser request, CancellationToken cancellationToken)
        {
            return session.LoadAsync<UserView>(request.Id, cancellationToken);
        }

        public async Task<UserView> Handle(GetAuthenticater request, CancellationToken cancellationToken)
        {
            try
            {
                var response =  session.Query<UserView>()
                    .Where(x=> x.Data.username == request.Username && x.Data.password == request.Password)
                    .SingleOrDefault();
                return response;
            }
            catch (Exception ex)
            {
                return null;

            }
        }
    }
}
