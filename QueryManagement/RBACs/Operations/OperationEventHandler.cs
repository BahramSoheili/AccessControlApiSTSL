using Core.Events;
using Core.Storage;
using DevicesSearch.RBACs.Operations.Events;
using System.Threading;
using System.Threading.Tasks;
namespace DevicesSearch.RBACs.Operations
{
    internal class OperationEventHandler :
         IEventHandler<OperationCreated>,
         IEventHandler<OperationUpdated>
    {
        private readonly IRepository<Operation> repository;
        public OperationEventHandler(IRepository<Operation> repository)
        {
            this.repository = repository;
        }
        public Task Handle(OperationCreated @event, CancellationToken cancellationToken)
        {
            var operation = new Operation(@event.OperationId, @event.Data);
            return repository.Add(operation, cancellationToken);
        }
        public Task Handle(OperationUpdated @event, CancellationToken cancellationToken)
        {
            var operation = repository.Find(@event.OperationId, cancellationToken).Result;
            operation.Update(operation.Id, @event.Data);
            return repository.Update(operation, cancellationToken);
        }
    }
}
