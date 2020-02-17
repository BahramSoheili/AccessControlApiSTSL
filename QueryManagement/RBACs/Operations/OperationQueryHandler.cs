using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Core.Queries;
using Core.Storage;
using DevicesSearch.Devices;
using DevicesSearch.Devices.Queries;
using DevicesSearch.RBACs.Operations.Queries;

namespace DevicesSearch.RBACs.Operations
{
    internal class OperationQueryHandler : 
        IQueryHandler<SearchOperations, IReadOnlyCollection<Operation>>,
        IQueryHandler<SearchOperationById, Operation>
    {
        private const int MaxItemsCount = 1000;
        private readonly Nest.IElasticClient elasticClient;
        private readonly IRepository<Operation> repository;
        public OperationQueryHandler(
            Nest.IElasticClient elasticClient,
           IRepository<Operation> repository
        )
        {
            this.elasticClient = elasticClient ?? throw new ArgumentNullException(nameof(elasticClient));
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<IReadOnlyCollection<Operation>> Handle(SearchOperations query, CancellationToken cancellationToken)
        {
            var response = await elasticClient.SearchAsync<Operation>(
                s => s.Query(q => q.QueryString(d => d.Query(query.Filter))).Size(MaxItemsCount)
            );
            return response.Documents;
        }
        public async Task<Operation> Handle(SearchOperationById request, CancellationToken cancellationToken)
        {
            var response = await repository.Find(request.Id, cancellationToken);
            return response;
        }
    }
}
