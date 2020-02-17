using CommandManagement.Domains.AccessControl.ValueObjects;
using Core.Commands;
using System;

namespace CommandManagement.Domains.AccessControl.Commands
{
    public class CreateCardholder: ICommand
    {
        public Guid Id { get; }
        public CardholderInfo Data { get; }

        public CreateCardholder(Guid id, CardholderInfo data)
        {
            Id = id;
            Data = data;
        }    
    }
}
