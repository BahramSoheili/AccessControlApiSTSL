using System.Collections.Generic;
using Core.Storage;
using MediatR;
using DevicesSearch.Devices.Events;
using DevicesSearch.Devices.Queries;
using DevicesSearch.Storage;
using Microsoft.Extensions.DependencyInjection;

namespace DevicesSearch.Devices
{
    public static class ConfigDevice
    {
        public static void AddConfigDevice(this IServiceCollection services)
        {
            services.AddScoped<IRepository<Device>, ElasticSearchRepository<Device>>();
            services.AddScoped<IRequestHandler<SearchDevices, IReadOnlyCollection<Device>>, DeviceQueryHandler>();
            services.AddScoped<INotificationHandler<DeviceCreated>, DeviceEventHandler>();
            services.AddScoped<INotificationHandler<DeviceUpdated>, DeviceEventHandler>();
            services.AddScoped<IRequestHandler<SearchDeviceById, Device>, DeviceQueryHandler>();
        }
    }
}
