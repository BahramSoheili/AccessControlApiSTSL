using DevicesSearch.Devices;
using DevicesSearch.RBACs.Operations;
using DevicesSearch.RBACs.Roles;
using DevicesSearch.RBACs.Users;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nest;
using System;

namespace DevicesSearch.Storage
{
    public static class ElasticSearchConfig
    {
        public static ElasticClient client;
        public static Nest.ConnectionSettings settings;
        public static void AddElasticsearch(
            this IServiceCollection services, IConfiguration configuration)
        {
            settings = new Nest.ConnectionSettings();
            services.AddDeviceIndex(configuration);
            services.AddOperationIndex(configuration);
            services.AddRoleIndex(configuration);
            services.AddUserIndex(configuration);
            client = new ElasticClient(settings);
            services.AddSingleton<IElasticClient>(client);
        }
        private static void AddDeviceIndex(this IServiceCollection services, 
            IConfiguration configuration)
        {
            string Index = configuration["elasticDevices:index"];

            settings.DefaultMappingFor<Device>(i => i
                  .IndexName(Index)
                  .IdProperty(p => p.Id)
                  .PropertyName(p => p.Data, "data"))
                  .EnableDebugMode()
                  .PrettyJson()
                  .RequestTimeout(TimeSpan.FromMinutes(2));
        }
        private static void AddOperationIndex(this IServiceCollection services,
           IConfiguration configuration)
        {
            string Index = configuration["elasticOperations:index"];

            settings.DefaultMappingFor<Operation>(i => i
                 .IndexName(Index)
                 .IdProperty(p => p.Id)
                 .PropertyName(p => p.Data, "data"))
                 .EnableDebugMode()
                 .PrettyJson()
                 .RequestTimeout(TimeSpan.FromMinutes(2));
        }
        private static void AddRoleIndex(this IServiceCollection services,
           IConfiguration configuration)
        {
            string Index = configuration["elasticRoles:index"];

            settings.DefaultMappingFor<Role>(i => i
                  .IndexName(Index)
                  .IdProperty(p => p.Id)
                  .PropertyName(p => p.Data, "data")
                  .PropertyName(p => p.Operations, "operations"))
                  .EnableDebugMode()
                  .PrettyJson()
                  .RequestTimeout(TimeSpan.FromMinutes(2));
        }
        private static void AddUserIndex(this IServiceCollection services,
           IConfiguration configuration)
        {
            string Index = configuration["elasticUsers:index"];           

            settings.DefaultMappingFor<User>(i => i
                .IndexName(Index)
                .IdProperty(p => p.Id)
                .PropertyName(p => p.Data, "data")
                .PropertyName(p => p.Role, "role"))
                .EnableDebugMode()
                .PrettyJson()
                .RequestTimeout(TimeSpan.FromMinutes(2));         
        }
    }
}
