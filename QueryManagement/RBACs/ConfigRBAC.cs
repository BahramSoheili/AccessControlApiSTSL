using DevicesSearch.RBACs.Operations;
using DevicesSearch.RBACs.Roles;
using DevicesSearch.RBACs.Users;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DevicesSearch.RBACs
{
    public static class ConfigRBAC
    {
        public static void AddConfigRBAC(this IServiceCollection services)
        {
            services.AddConfigOperation();
            services.AddConfigRole();
            services.AddConfigUser();
        }
    }
}
