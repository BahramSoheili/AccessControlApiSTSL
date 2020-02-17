using MediatR;
using Microsoft.Extensions.DependencyInjection;
using CommandManagement.Domains.RBACs.Events;

namespace CommandManagement.Notifications
{
    public static class Config
    {
        public static void AddNotifications(this IServiceCollection services)
        {
            services.AddScoped<INotificationHandler<UserCreated>, EmailNotifier>();
            //services.AddScoped<INotificationHandler<DeviceUpdated>, EmailNotifier>();
        }
    }
}
