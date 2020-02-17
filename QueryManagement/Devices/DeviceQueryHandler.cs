using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Core.Queries;
using Core.Storage;
using DevicesSearch.Devices.Queries;
using MediatR;

namespace DevicesSearch.Devices
{
    internal class DeviceQueryHandler : 
        IQueryHandler<SearchDevices, IReadOnlyCollection<Device>>,
        IQueryHandler<SearchDeviceById, Device>
    {
        private const int MaxItemsCount = 1000;
        private readonly Nest.IElasticClient elasticClient;
        private readonly IRepository<Device> repository;
        public DeviceQueryHandler(
            Nest.IElasticClient elasticClient,
            IRepository<Device> repository
        )
        {
            this.elasticClient = elasticClient ?? throw new ArgumentNullException(nameof(elasticClient));
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<IReadOnlyCollection<Device>> Handle(SearchDevices query, CancellationToken cancellationToken)
        {
            var response = await elasticClient.SearchAsync<Device>(
                s => s.Query(q => q.QueryString(d => d.Query(query.Filter))).Size(MaxItemsCount)
            );
            return response.Documents;
        }
        public async Task<Device> Handle(SearchDeviceById request, CancellationToken cancellationToken)
        {
            var response = await repository.Find(request.Id, cancellationToken);
            return response;
        }
    }
}
