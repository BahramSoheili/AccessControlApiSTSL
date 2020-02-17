using Core.Queries;
using System.Collections.Generic;
namespace DevicesSearch.RBACs.Users.Queries
{
    public class SearchUsers : IQuery<IReadOnlyCollection<User>>
    {
        public string Filter { get; }

        public SearchUsers(string filter)
        {
            Filter = filter;
        }
    }
}

