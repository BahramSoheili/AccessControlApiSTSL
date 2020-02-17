using CommandManagement.Domains.Devices.Commands;
using Core.Commands;
using Core.Storage;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CommandManagement.Domains.AccessControl.Handlers
{
    internal class DeviceCommandHandler :
      ICommandHandler<CreateDevice>,
      ICommandHandler<UpdateDevice>
    {
        private readonly IRepository<Device> repository;

        public DeviceCommandHandler(
            IRepository<Device> repository
        )
        {
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<Unit> Handle(CreateDevice request, CancellationToken cancellationToken)
        {
            var cardholder = Device.Create(request.Id, request.Data);

            await repository.Add(cardholder, cancellationToken);

            return Unit.Value;
        }

        public async Task<Unit> Handle(UpdateDevice request, CancellationToken cancellationToken)
        {
            var device = await repository.Find(request.Id, cancellationToken);

            device.Update(request.Id, request.Data);

            await repository.Update(device, cancellationToken);

            return Unit.Value;
        }


    }
}

