using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Core.Queries;
using Core.Storage;
using DevicesSearch.RBACs.Roles.Queries;

namespace DevicesSearch.RBACs.Roles
{
    internal class RoleQueryHandler: 
        IQueryHandler<SearchRoles, IReadOnlyCollection<Role>>,
        IQueryHandler<SearchRoleById, Role>
    {
        private const int MaxItemsCount = 1000;
        private readonly Nest.IElasticClient elasticClient;
        private readonly IRepository<Role> repository;
        public RoleQueryHandler(
            Nest.IElasticClient elasticClient,
            IRepository<Role> repository
        )
        {
            this.elasticClient = elasticClient ?? throw new ArgumentNullException(nameof(elasticClient));
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<IReadOnlyCollection<Role>> Handle(SearchRoles query, CancellationToken cancellationToken)
        {
            var response = await elasticClient.SearchAsync<Role>(
                s => s.Query(q => q.QueryString(d => d.Query(query.Filter))).Size(MaxItemsCount)
            );
            return response.Documents;
        }
        public async Task<Role> Handle(SearchRoleById request, CancellationToken cancellationToken)
        {
            var response = await repository.Find(request.Id, cancellationToken);
            return response;
        }
    }
}
