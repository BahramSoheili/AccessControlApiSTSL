using Core.Aggregates;
using Core.Events;
using Core.Storage;
using DevicesSearch.RBACs.Users;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DevicesSearch.Storage
{
    public class ElasticSearchRepository<T>: IRepository<T> where T : class, IAggregate, new()
    {
        private readonly Nest.IElasticClient elasticClient;
        //private readonly IEventBus eventBus;
        private const int MaxItemsCount = 1000;
        //IElasticsearchRepository repository;


        public ElasticSearchRepository(Nest.IElasticClient elasticClient, IEventBus eventBus)
        {
            this.elasticClient = elasticClient ?? throw new ArgumentNullException(nameof(elasticClient));
            //this.eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
            //this.repository = new ElasticsearchRepository(elasticClient);

        }

        public async Task<IReadOnlyCollection<T>> Filter(string filter, CancellationToken cancellationToken)
        {
            var response = await elasticClient.SearchAsync<T>(
                s => s.Query(q => q.QueryString(d => d.Query(filter))).Size(MaxItemsCount)
            );
            return response.Documents;
        }
        public async Task<T> Find(Guid id, CancellationToken cancellationToken)
        {
            var response = await elasticClient.GetAsync<T>(id);
            return response.Source;
        }
        public Task Add(T aggregate, CancellationToken cancellationToken)
        {
            return elasticClient.IndexAsync(aggregate, i => i.Id(aggregate.Id));
        }
        public Task Update(T aggregate, CancellationToken cancellationToken)
        {
            return elasticClient.UpdateAsync<T>(aggregate.Id, i => i.Doc(aggregate));
        }
        public Task Delete(T aggregate, CancellationToken cancellationToken)
        {
            return elasticClient.DeleteAsync<T>(aggregate.Id);
        }
        public async Task<T> Authenticate(string username, string password, CancellationToken cancellationToken)
        {
            try
            {
                var result = await elasticClient.SearchAsync<User>(s => s
                     .Query(q => q.Match(m => m
                      .Field(f => f.Data.username)
                      .Query(username))));
                var enumUser = result.Documents;
                User user = new User();
                foreach(var u in enumUser)
                {
                    user = u;
                }

                if(user.Data.password == password)
                {
                    var response = await elasticClient.GetAsync<T>(user.Id);
                    return response.Source;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }           
        }


    }
}
