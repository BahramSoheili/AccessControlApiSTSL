using Core.Queries;
using System.Collections.Generic;

namespace DevicesSearch.RBACs.Operations.Queries
{
    public class SearchOperations : IQuery<IReadOnlyCollection<Operation>>
    {
        public string Filter { get; }

        public SearchOperations(string filter)
        {
            Filter = filter;
        }
    }
}

