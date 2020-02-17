using Core.Storage;
using DevicesSearch.RBACs.Operations.Events;
using DevicesSearch.RBACs.Operations.Queries;
using DevicesSearch.Storage;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;

namespace DevicesSearch.RBACs.Operations
{
    public static class ConfigOperation
    {
        public static void AddConfigOperation(this IServiceCollection services)
        {
            services.AddScoped<IRepository<Operation>, ElasticSearchRepository<Operation>>();
            services.AddScoped<IRequestHandler<SearchOperations, IReadOnlyCollection<Operation>>, OperationQueryHandler>();
            services.AddScoped<INotificationHandler<OperationCreated>, OperationEventHandler>();
            services.AddScoped<INotificationHandler<OperationUpdated>, OperationEventHandler>();
            services.AddScoped<IRequestHandler<SearchOperationById, Operation>, OperationQueryHandler>();
        }
    }
}
