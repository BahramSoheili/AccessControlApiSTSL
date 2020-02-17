using CommandManagement.Domains.AccessControl.ValueObjects;
using Core.Events;
using System;
namespace CommandManagement.Domains.AccessControl.Events
{
    public class CardholderCreated: IExternalEvent
    {
        public Guid CardholderId { get; }
        public CardholderInfo Data { get; }
        public DateTime Created { get; }

        public CardholderCreated(Guid deviceId, CardholderInfo data, DateTime created)
        {
            CardholderId = deviceId;
            Data = data;
            Created = created;
        }

        public static CardholderCreated Create(Guid deviceId, CardholderInfo data, DateTime created)
        {
            if (deviceId == default(Guid))
                throw new ArgumentException($"{nameof(deviceId)} needs to be defined.");

            if (created == default(DateTime))
                throw new ArgumentException($"{nameof(created)} needs to be defined.");

            if (data == null)
                throw new ArgumentException($"data can't be empty.");

            return new CardholderCreated(deviceId, data, created);
        }
    }
}

