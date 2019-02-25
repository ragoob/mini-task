using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;


namespace Infrastructure.Extentions
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

            return _serviceProvider;
            
          
        }

        public virtual IServiceProvider ServiceProvider => _serviceProvider;

       
    }
}
