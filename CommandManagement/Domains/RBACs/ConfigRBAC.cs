using Core.Storage;
using CommandManagement.Domains.RBACs.Views;
using CommandManagement.Domains.RBACs.Projections;
using CommandManagement.Domains.RBACs.Queries;
using CommandManagement.Storage;
using Marten;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using CommandManagement.Domains.RBACs.Commands;
using System;
using CommandManagement.Domains.RBACs.Handlers;

namespace CommandManagement.Domains.RBACs
{
    public static class ConfigRBAC
    {
        public static void AddRBAC(this IServiceCollection services)
        {
            services.AddUserScope();
            services.AddRoleScope();
            services.AddOperationScope();

        }
        private static void AddOperationScope(this IServiceCollection services)
        {
            services.AddScoped<IRepository<Operation>, MartenRepository<Operation>>();
            services.AddScoped<IRequestHandler<CreateOperation, Unit>, OperationCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateOperation, Unit>, OperationCommandHandler>();
            services.AddScoped<IRequestHandler<GetOperation, OperationView>, OperationQueryHandler>();
        }

        private static void AddRoleScope(this IServiceCollection services)
        {
            services.AddScoped<IRepository<Role>, MartenRepository<Role>>();
            services.AddScoped<IRequestHandler<CreateRole, Unit>, RoleCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateRole, Unit>, RoleCommandHandler>();
            services.AddScoped<IRequestHandler<GetRole, RoleView>, RoleQueryHandler>();
        }
        private static void AddUserScope(this IServiceCollection services)
        {
            services.AddScoped<IRepository<User>, MartenRepository<User>>();
            services.AddScoped<IRequestHandler<CreateUser, Unit>, UserCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateUser, Unit>, UserCommandHandler>();
            services.AddScoped<IRequestHandler<GetUser, UserView>, UserQueryHandler>();
            services.AddScoped<IRequestHandler<GetAuthenticater, UserView>, UserQueryHandler>();

        }

        public static void ConfigureMarten(StoreOptions options)
        {
            options.Events.InlineProjections.AggregateStreamsWith<Operation>();
            options.Events.InlineProjections.Add<OperationViewProjection>();

            options.Events.InlineProjections.AggregateStreamsWith<Role>();
            options.Events.InlineProjections.Add<RoleViewProjection>();

            options.Events.InlineProjections.AggregateStreamsWith<User>();
            options.Events.InlineProjections.Add<UserViewProjection>();
        }
    }
}
