using CommandManagement.Domains.AccessControl.Commands;
using CommandManagement.Domains.Devices.Commands;
using Core.Commands;
using Core.Storage;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CommandManagement.Domains.AccessControl.Handlers
{
   internal class CardholderCommandHandler:
       ICommandHandler<CreateCardholder>,
       ICommandHandler<UpdateCardholder>
    {
        private readonly IRepository<Cardholder> repository;

        public CardholderCommandHandler(
            IRepository<Cardholder> repository
        )
        {
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<Unit> Handle(CreateCardholder request, CancellationToken cancellationToken)
        {
            var cardholder = Cardholder.Create(request.Id, request.Data);

            await repository.Add(cardholder, cancellationToken);

            return Unit.Value;
        }

        public async Task<Unit> Handle(UpdateCardholder request, CancellationToken cancellationToken)
        {
            var meeting = await repository.Find(request.Id, cancellationToken);

            meeting.Update(request.Id, request.Data);

            await repository.Update(meeting, cancellationToken);

            return Unit.Value;
        }

       
    }
}


