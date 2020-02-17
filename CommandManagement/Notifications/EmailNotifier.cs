using Core.Events;
using CommandManagement.Domains.Devices.Events;
using System;
using System.Threading;
using System.Threading.Tasks;
using CommandManagement.Domains.RBACs.Events;
using CommandManagement.Domains.AccessControl.Projections;

namespace CommandManagement.Notifications
{
    public class EmailNotifier : 
        IEventHandler<DeviceCreated>, 
        IEventHandler<UserCreated>
    {
        public Task Handle(UserCreated @event, CancellationToken cancellationToken)
        {
            //some dummy logic, but here could be email sender placed
            Console.Write($"{@event.Data.username} has been created");

            return Task.CompletedTask;
        }

        public Task Handle(DeviceCreated @event, CancellationToken cancellationToken)
        {
            Console.Write($"{@event.Data.deviceName} has been created");

            return Task.CompletedTask;
        }
    }
}
