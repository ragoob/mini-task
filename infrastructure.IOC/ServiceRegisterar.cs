using ApplicationServices.Intefaces;
using ApplicationServices.Services;
using Core.Bus;
using Core.Notifications;
using Data.Context;
using Data.Repositories;
using Data.UnityOfWork;
using Domain.CommandHandlers;
using Domain.Commands.Project;
using Domain.EventHandlers;
using Domain.Events;
using Domain.Interfaces;
using infrastructure.Buses;
using Infrastructure.Extentions;
using MediatR;
using Microsoft.AspNetCore.Http;
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
            var engine = EngineContext.Create();
            engine.Initialize(services);
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IMediatorHandler, Bus>();
            services.AddScoped<IProjectService, ProjectService>();
            services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();
            services.AddScoped<INotificationHandler<AddNewProjectEvent>, ProjectEventHandler>();
            services.AddScoped<INotificationHandler<UpdateProjectEvent>, ProjectEventHandler>();
            services.AddScoped<INotificationHandler<DeleteProjectEvent>, ProjectEventHandler>();
            services.AddScoped<IRequestHandler<AddNewProjectCommand, bool>, ProjectCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateProjectCommand, bool>, ProjectCommandHandler>();
            services.AddScoped<IRequestHandler<DeleteProjectCommand, bool>, ProjectCommandHandler>();
            services.AddScoped<IProjectRepository, ProjectRepository>();
            services.AddScoped<IUnitOfWork, UntitOfWork>();
            services.AddScoped<MiniTaskContext>();
            services.AddDbContext<MiniTaskContext>(options =>
          options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
           
            return services.BuildServiceProvider();
        }
    }
}
