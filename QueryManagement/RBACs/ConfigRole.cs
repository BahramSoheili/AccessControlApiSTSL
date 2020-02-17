using Core.Storage;
using DevicesSearch.RBACs.Roles.Events;
using DevicesSearch.RBACs.Roles.Queries;
using DevicesSearch.Storage;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
namespace DevicesSearch.RBACs.Roles
{
    public static class ConfigRole
    {
        public static void AddConfigRole(this IServiceCollection services)
        {
            services.AddScoped<IRepository<Role>, ElasticSearchRepository<Role>>();
            services.AddScoped<IRequestHandler<SearchRoles, IReadOnlyCollection<Role>>, RoleQueryHandler>();
            services.AddScoped<INotificationHandler<RoleCreated>, RoleEventHandler>();
            services.AddScoped<INotificationHandler<RoleUpdated>, RoleEventHandler>();
            services.AddScoped<IRequestHandler<SearchRoleById, Role>, RoleQueryHandler>();
        }
    }
}
