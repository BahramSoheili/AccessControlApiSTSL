using DevicesSearch.Devices;
using DevicesSearch.RBACs;
using DevicesSearch.Storage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DevicesSearch
{
    public static class Config
    {
        public static void AddDevicesSearch(this IServiceCollection services, IConfiguration config)
        {
            services.AddElasticsearch(config);
            services.AddConfigDevice();
            services.AddConfigRBAC();
        }
    }
}
