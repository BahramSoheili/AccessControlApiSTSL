using Core.Events;
using CommandManagement.Domains.Devices.ValueObjects;
using System;
using CommandManagement.Domains.AccessControl.ValueObjects;

namespace CommandManagement.Domains.Devices.Events
{
    public class CardholderUpdated: IExternalEvent
    {
        public Guid CardholderId { get; }
        public CardholderInfo Data { get; }
        public CardholderUpdated(Guid cardholderId, CardholderInfo data)
        {
            CardholderId = cardholderId;
            Data = data;
        }
        public static CardholderUpdated Create(Guid cardholderId, CardholderInfo data)
        {
            if (cardholderId == default(Guid))
                throw new ArgumentException($"{nameof(cardholderId)} needs to be defined.");

            if (data == null)
                throw new ArgumentException($"data can't be empty.");

            return new CardholderUpdated(cardholderId, data);
        }
    }
}

