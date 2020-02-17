using CommandManagement.Domains.AccessControl;
using CommandManagement.Domains.RBACs;
using CommandManagement.Notifications;
using CommandManagement.Storage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CommandManagement
{
    public static class Config
    {
        public static void AddCommandManagement(this IServiceCollection services, IConfiguration config)
        {
            services.AddMarten(config, options =>
            {
                ConfigAccessControl.ConfigureMarten(options);
                ConfigRBAC.ConfigureMarten(options);
            });
            services.AddConfigAccessControl();
            services.AddRBAC();
            services.AddNotifications();
        }
    }
}
