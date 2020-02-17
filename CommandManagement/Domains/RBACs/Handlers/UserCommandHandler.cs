using Core.Commands;
using Core.Storage;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using CommandManagement.Domains.RBACs.Commands;
using CommandManagement.Domains.RBACs.ValueObjects;

namespace CommandManagement.Domains.RBACs.Handlers
{
    class UserCommandHandler:
        ICommandHandler<CreateUser>,
        ICommandHandler<UpdateUser>,
        ICommandHandler<AuthenticateUser>

    {
        private readonly IRepository<User> repository;
        public UserCommandHandler(
            IRepository<User> repository
        )
        {
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }
        public async Task<Unit> Handle(CreateUser request, CancellationToken cancellationToken)
        {
            var user = User.Create(request.Id, request.Data, request.Role);
            await repository.Add(user, cancellationToken);
            return Unit.Value;
        }
        public async Task<Unit> Handle(UpdateUser request, CancellationToken cancellationToken)
        {
            var user = await repository.Find(request.Id, cancellationToken);
            user.Update(request.Id, request.Data, request.Role);
            await repository.Update(user, cancellationToken);
            return Unit.Value;
        }

        public Task<Unit> Handle(AuthenticateUser request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        //public Task<ClaimElement> Handle(AuthenticateUser request, CancellationToken cancellationToken)
        //{
        //    throw new NotImplementedException();
        //}

    }
}
