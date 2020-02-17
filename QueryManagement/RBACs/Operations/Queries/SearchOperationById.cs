using Core.Queries;
using System;
using System.Collections.Generic;

namespace DevicesSearch.RBACs.Operations.Queries
{
    public class SearchOperationById : IQuery<Operation>
    {
        public Guid Id { get; }

        public SearchOperationById(Guid id)
        {
            Id = id;
        }
    }
}

