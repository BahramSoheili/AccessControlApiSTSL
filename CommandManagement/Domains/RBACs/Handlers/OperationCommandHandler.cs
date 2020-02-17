using Core.Commands;
using Core.Storage;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using CommandManagement.Domains.RBACs.Commands;
namespace CommandManagement.Domains.RBACs.Handlers
{
    class OperationCommandHandler:
        ICommandHandler<CreateOperation>,
        ICommandHandler<UpdateOperation>
    {
        private readonly IRepository<Operation> repository;
        public OperationCommandHandler(
            IRepository<Operation> repository
        )
        {
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }
        public async Task<Unit> Handle(CreateOperation request, CancellationToken cancellationToken)
        {
            var operation = Operation.Create(request.Id, request.Data);
            await repository.Add(operation, cancellationToken);
            return Unit.Value;
        }

        public async Task<Unit> Handle(UpdateOperation request, CancellationToken cancellationToken)
        {
            var operation = await repository.Find(request.Id, cancellationToken);
            operation.Update(request.Id, request.Data);
            await repository.Update(operation, cancellationToken);
            return Unit.Value;
        }
    }
}
