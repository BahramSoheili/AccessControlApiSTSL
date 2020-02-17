﻿using Core.Commands;
using CommandManagement.Domains.RBACs.ValueObjects;
using System;

namespace CommandManagement.Domains.RBACs.Commands
{
    public class CreateOperation: ICommand
    {
        public Guid Id { get; }
        public OperationInfo Data { get; }

        public CreateOperation(Guid id, OperationInfo data)
        {
            Id = id;
            Data = data;
        }
    }
}
