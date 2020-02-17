using CommandManagement.Domains.AccessControl;
using CommandManagement.Domains.Devices.Commands;
using Core.Commands;
using Core.Storage;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
namespace CommandManagement.Domains.RBACs.Handlers
{
    class DeviceTypeCommandHandler :
        ICommandHandler<CreateDeviceType>,
        ICommandHandler<UpdateDeviceType>
    {
        private readonly IRepository<DeviceType> repository;
        public DeviceTypeCommandHandler(
            IRepository<DeviceType> repository
        )
        {
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }
        public async Task<Unit> Handle(CreateDeviceType request, CancellationToken cancellationToken)
        {
            var operation = DeviceType.Create(request.Id, request.Data);
            await repository.Add(operation, cancellationToken);
            return Unit.Value;
        }

        public async Task<Unit> Handle(UpdateDeviceType request, CancellationToken cancellationToken)
        {
            var operation = await repository.Find(request.Id, cancellationToken);
            operation.Update(request.Id, request.Data);
            await repository.Update(operation, cancellationToken);
            return Unit.Value;
        }
    }
}

