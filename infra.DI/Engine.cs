using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Core.Bus;
using ApplicationServices.Intefaces;
using ApplicationServices.Services;
using MediatR;
using Core.Notifications;
using Domain.Events;
using Domain.Commands.Project;
using Domain.EventHandlers;
using Domain.CommandHandlers;
using Domain.Interfaces;
using infrastructure.Bus;
using Data.Context;
using Data.UnityOfWork;
using Data.Repositories;

namespace infra.DI
{
    public class Engine : IEngine
    {
        
        private IServiceProvider _serviceProvider { get; set; }

        protected IServiceProvider GetServiceProvider()
        {
            var accessor = ServiceProvider.GetService<IHttpContextAccessor>();
            var context = accessor.HttpContext;
            return context?.RequestServices ?? ServiceProvider;
        }

        public T Resolve<T>() where T : class
        {
            return (T)GetServiceProvider().GetRequiredService(typeof(T));
        }

        public object Resolve(Type type)
        {
            return GetServiceProvider().GetRequiredService(type);
        }

        public IEnumerable<T> ResolveAll<T>()
        {
            return (IEnumerable<T>)GetServiceProvider().GetServices(typeof(T));
        }

        public void Initialize(IServiceCollection services)
        {
            _serviceProvider = services.BuildServiceProvider();

        }

        public IServiceProvider ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
           
           return RegisterDependencies( services);
            
          
        }

        public virtual IServiceProvider ServiceProvider => _serviceProvider;

        protected virtual IServiceProvider RegisterDependencies( IServiceCollection services)
        {
          
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
            services.AddScoped<IUnitOfWork, UntityOfWork>();
            services.AddScoped<MiniTaskContext>();
            return _serviceProvider;
        }
    }
}
