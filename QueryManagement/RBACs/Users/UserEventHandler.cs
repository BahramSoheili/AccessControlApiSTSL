using Core.Events;
using Core.Storage;
using DevicesSearch.Devices.Events;
using DevicesSearch.RBACs.Users.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DevicesSearch.RBACs.Users
{
    internal class UserEventHandler: IEventHandler<UserCreated>, IEventHandler<UserUpdated>
    {
        private readonly IRepository<User> repository;

        public UserEventHandler(IRepository<User> repository)
        {
            this.repository = repository;
        }

        public Task Handle(UserCreated @event, CancellationToken cancellationToken)
        {
            var user = new User(@event.UserId, @event.Data, @event.Role);

            return repository.Add(user, cancellationToken);
        }
        public Task Handle(UserUpdated @event, CancellationToken cancellationToken)
        {
            var user = repository.Find(@event.UserId, cancellationToken).Result;

            user.Update(user.Id, @event.Data, @event.Role);

            return repository.Update(user, cancellationToken);
        }
    }
}
