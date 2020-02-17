using Core.Commands;
using System;
using CommandManagement.Domains.AccessControl.ValueObjects;

namespace CommandManagement.Domains.Devices.Commands
{
    public class UpdateCardholder: ICommand
    {
        public Guid Id { get; }
        public CardholderInfo Data { get; }
        public UpdateCardholder(Guid id, CardholderInfo data)
        {
            Id = id;
            Data = data;
        }
    }
}
