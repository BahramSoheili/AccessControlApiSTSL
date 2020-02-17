using Core.Aggregates;
using DevicesSearch.Devices.Events;
using DevicesSearch.Devices.SearchObjects;
using Newtonsoft.Json;
using System;

namespace DevicesSearch.Devices
{
    public class Device: Aggregate
    {
        public DeviceData Data { get; protected set; }
        public Device()
        {
        }
        [JsonConstructor]
        public Device(Guid id, DeviceData data)
        {
            Id = id;
            Data = data;
        }
        public void Update(Guid id, DeviceData data)
        {
            var @event = DeviceUpdated.Create(id, data);
            Apply(@event);
        }
        private void Apply(DeviceUpdated @event)
        {
            Data = @event.Data;
        }
    }
}
