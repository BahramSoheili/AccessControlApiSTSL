using Core.Queries;
using System.Collections.Generic;

namespace DevicesSearch.RBACs.Roles.Queries
{
    public class SearchRoles: IQuery<IReadOnlyCollection<Role>>
    {
        public string Filter { get; }

        public SearchRoles(string filter)
        {
            Filter = filter;
        }
    }
}

