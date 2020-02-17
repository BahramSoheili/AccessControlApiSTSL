using Core.Storage;
using CommandManagement.Domains.Devices.Commands;
using CommandManagement.Storage;
using Marten;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using CommandManagement.Domains.AccessControl.Queries;
using CommandManagement.Domains.AccessControl.Handlers;
using CommandManagement.Domains.Devices.Projections;
using CommandManagement.Domains.AccessControl.Projections;
using CommandManagement.Domains.AccessControl;
using CommandManagement.Domains.Devices.Queries;
using CommandManagement.Domains.AccessControl.View;
using CommandManagement.Domains.RBACs.Handlers;
using CommandManagement.Domains.AccessControl.Views;
using CommandManagement.Domains.AccessControl.Commands;

namespace CommandManagement.Domains.AccessControl
{
    public static class ConfigAccessControl
    {
        public static void AddConfigAccessControl(this IServiceCollection services)
        {
            services.AddCardholderScope();
            services.AddDeviceScope();
            services.AddDeviceTypeScope();

        }
        private static void AddCardholderScope(this IServiceCollection services)
        {
            services.AddScoped<IRepository<Cardholder>, MartenRepository<Cardholder>>();
            services.AddScoped<IRequestHandler<CreateCardholder, Unit>, CardholderCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateCardholder, Unit>, CardholderCommandHandler>();
            services.AddScoped<IRequestHandler<GetCardholder, CardholderView>, CardholderQueryHandler>();
        }

        private static void AddDeviceScope(this IServiceCollection services)
        {
            services.AddScoped<IRepository<Device>, MartenRepository<Device>>();
            services.AddScoped<IRequestHandler<CreateDevice, Unit>, DeviceCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateDevice, Unit>, DeviceCommandHandler>();
            services.AddScoped<IRequestHandler<GetDevice, DeviceView>, DeviceQueryHandler>();
        }
        private static void AddDeviceTypeScope(this IServiceCollection services)
        {
            services.AddScoped<IRepository<DeviceType>, MartenRepository<DeviceType>>();
            services.AddScoped<IRequestHandler<CreateDeviceType, Unit>, DeviceTypeCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateDeviceType, Unit>, DeviceTypeCommandHandler>();
            services.AddScoped<IRequestHandler<GetDeviceType, DeviceTypeView>, DeviceTypeQueryHandler>();

        }

        public static void ConfigureMarten(StoreOptions options)
        {
            options.Events.InlineProjections.AggregateStreamsWith<Cardholder>();
            options.Events.InlineProjections.Add<CardholderViewProjection>();

            options.Events.InlineProjections.AggregateStreamsWith<Device>();
            options.Events.InlineProjections.Add<DeviceViewProjection>();

            options.Events.InlineProjections.AggregateStreamsWith<DeviceType>();
            options.Events.InlineProjections.Add<DeviceTypeViewProjection>();
        }
    }
}
