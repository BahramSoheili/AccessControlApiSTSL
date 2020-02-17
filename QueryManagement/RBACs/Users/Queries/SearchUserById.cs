using Core.Queries;
using System;
namespace DevicesSearch.RBACs.Users.Queries
{
    public class SearchUserById: IQuery<User>
    {
        public Guid Id { get; }

        public SearchUserById(Guid id)
        {
            Id = id;
        }
    }
}

