using Core.Events;
using Core.Storage;
using DevicesSearch.Devices.Events;

using System.Threading;
using System.Threading.Tasks;

namespace DevicesSearch.Devices
{
    internal class DeviceEventHandler :
         IEventHandler<DeviceCreated>,
         IEventHandler<DeviceUpdated>
    {
        private readonly IRepository<Device> repository;

        public DeviceEventHandler(IRepository<Device> repository)
        {
            this.repository = repository;
        }

        public Task Handle(DeviceCreated @event, CancellationToken cancellationToken)
        {
            var device = new Device(@event.DeviceId, @event.Data);

            return repository.Add(device, cancellationToken);
        }
        public Task Handle(DeviceUpdated @event, CancellationToken cancellationToken)
        {
            var device = repository.Find(@event.DeviceId, cancellationToken).Result;

            //var Updatedmeeting = new Meeting(@event.MeetingId, @event.Name);

            device.Update(device.Id, @event.Data);

            return repository.Update(device, cancellationToken);
        }
    }
}
