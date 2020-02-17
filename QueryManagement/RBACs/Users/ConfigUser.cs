using Core.Storage;
using DevicesSearch.RBACs.Users.Events;
using DevicesSearch.RBACs.Users.Queries;
using DevicesSearch.Storage;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;

namespace DevicesSearch.RBACs.Users
{
    public static class ConfigUser
    {
        public static void AddConfigUser(this IServiceCollection services)
        {
            services.AddScoped<IRepository<User>, ElasticSearchRepository<User>>();
            services.AddScoped<IRequestHandler<SearchUsers, IReadOnlyCollection<User>>, UserQueryHandler>();
            services.AddScoped<INotificationHandler<UserCreated>, UserEventHandler>();
            services.AddScoped<INotificationHandler<UserUpdated>, UserEventHandler>();
            services.AddScoped<IRequestHandler<SearchUserById, User>, UserQueryHandler>();
            services.AddScoped<IRequestHandler<AuthenticateUser, User>, UserQueryHandler>();
        }
    }
}
