using Core.Events;
using Core.Storage;
using DevicesSearch.Devices.Events;
using DevicesSearch.RBACs.Roles.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DevicesSearch.RBACs.Roles
{
    internal class RoleEventHandler:
         IEventHandler<RoleCreated>,
         IEventHandler<RoleUpdated>
    {
        private readonly IRepository<Role> repository;
        public RoleEventHandler(IRepository<Role> repository)
        {
            this.repository = repository;
        }
        public Task Handle(RoleCreated @event, CancellationToken cancellationToken)
        {
            var role = new Role(@event.RoleId, @event.Data, @event.Operations);
            return repository.Add(role, cancellationToken);
        }
        public Task Handle(RoleUpdated @event, CancellationToken cancellationToken)
        {
            var role = repository.Find(@event.RoleId, cancellationToken).Result;
            role.Update(role.Id, @event.Data, @event.Operations);
            return repository.Update(role, cancellationToken);
        }
    }
}
