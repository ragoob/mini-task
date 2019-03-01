using ApplicationServices.Intefaces;
using ApplicationServices.Services;
using Core.Bus;
using Core.Notifications;
using Core.Plugins;
using Data.Context;
using Data.Repositories;
using Data.UnityOfWork;
using Domain.CommandHandlers;
using Domain.Commands.Organizations;
using Domain.Commands.Projects;
using Domain.EventHandlers;
using Domain.Events;
using Domain.Interfaces;
using infrastructure.Buses;
using Infrastructure.Extentions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Internal;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace infrastructure.IOC
{
    public class ServiceRegistrar
    {
        public static IServiceProvider RegisterDependencies(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IMediatorHandler, Bus>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IProjectService, ProjectService>();
            services.AddScoped<IOrganizationService, OrganizationService>();
            services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();
            services.AddScoped<INotificationHandler<ProjectAddedEvent>, ProjectEventHandler>();
            services.AddScoped<INotificationHandler<ProjectUpdatedEvent>, ProjectEventHandler>();
            services.AddScoped<INotificationHandler<ProjectDeletedEvent>, ProjectEventHandler>();
            services.AddScoped<IRequestHandler<AddNewProjectCommand, bool>, ProjectCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateProjectCommand, bool>, ProjectCommandHandler>();
            services.AddScoped<IRequestHandler<DeleteProjectCommand, bool>, ProjectCommandHandler>();
            services.AddScoped<INotificationHandler<OrganizationAddedEvent>, OrganizationEventHandler>();
            services.AddScoped<INotificationHandler<OrganizationUpdatedEvent>, OrganizationEventHandler>();
            services.AddScoped<INotificationHandler<OrganizationDeletedEvent>, OrganizationEventHandler>();
            services.AddScoped<IRequestHandler<AddNewOrganizationCommand, bool>, OrganizationCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateOrganizationCommand, bool>, OrganizationCommandHandler>();
            services.AddScoped<IRequestHandler<DeleteOrganizationCommand, bool>, OrganizationCommandHandler>();
            services.AddScoped<IUnitOfWork, UntitOfWork>();
            services.AddScoped<MiniTaskContext>();
            services.AddScoped<IPluginInfoParser, PluginInfoParser>();
            services.AddScoped<ITaskFileProvider, TaskFileProvider>();
            services.AddDbContext<MiniTaskContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            var engine = EngineContext.Create();
            engine.Initialize(services);
            
            var mvcCoreBuilder = services.AddMvcCore();
            PluginManager.Initialize(mvcCoreBuilder.PartManager);
            return services.BuildServiceProvider();
        }
    }
}
