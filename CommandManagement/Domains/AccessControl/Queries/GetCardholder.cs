using CommandManagement.Domains.AccessControl.Views;
using Core.Queries;
using System;
using System.Collections.Generic;
using System.Text;

namespace CommandManagement.Domains.AccessControl.Queries
{
    public class GetCardholder: IQuery<CardholderView>
    {
        public Guid Id { get; }

        public GetCardholder(Guid id)
        {
            Id = id;
        }
    }
}
