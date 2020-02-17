using CommandManagement.Domains.AccessControl.Events;
using CommandManagement.Domains.AccessControl.ValueObjects;
using CommandManagement.Domains.Devices.Events;
using Core.Aggregates;
using System;
namespace CommandManagement.Domains.AccessControl
{
    internal class Cardholder: Aggregate
    {
        public CardholderInfo Data { get; private set; }
        public DateTime Created { get; private set; }
        public Cardholder()
        {

        }
        public static Cardholder Create(Guid id, CardholderInfo data)
        {
            return new Cardholder(id, data, DateTime.UtcNow);
        }
        public Cardholder(Guid id, CardholderInfo data, DateTime created)
        {
            if (id == Guid.Empty)
                throw new ArgumentException($"{nameof(id)} cannot be empty.");

            if (data == null)
                throw new ArgumentException($"{nameof(data)} cannot be empty.");

            var @event = CardholderCreated.Create(id, data, created);

            Enqueue(@event);
            Apply(@event);
        }
        public void Apply(CardholderCreated @event)
        {
            Id = @event.CardholderId;
            Data = @event.Data;
            Created = @event.Created;
        }
        internal void Update(Guid id, CardholderInfo data)
        {
            var @event = CardholderUpdated.Create(id, data);
            Enqueue(@event);
            Apply(@event);
        }
        private void Apply(CardholderUpdated @event)
        {
            Data = @event.Data;
        }
    }
}


