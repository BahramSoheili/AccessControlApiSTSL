using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Core.Aggregates;

namespace Core.Storage
{
    public interface IRepository<T> where T : IAggregate
    {
        Task<IReadOnlyCollection<T>> Filter(string filter, CancellationToken cancellationToken);
        Task<T> Find(Guid id, CancellationToken cancellationToken);
        Task<T> Authenticate(string username, string password, CancellationToken cancellationToken);

        Task Add(T aggregate, CancellationToken cancellationToken);

        Task Update(T aggregate, CancellationToken cancellationToken);

        Task Delete(T aggregate, CancellationToken cancellationToken);
    }
}
