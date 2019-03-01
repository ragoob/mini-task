using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Internal;

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

            _serviceProvider = services.BuildServiceProvider();
            return _serviceProvider;
            
          
        }

        public object ManualResolve(Type type)
        {
            Exception innerException = null;
            foreach (var constructor in type.GetConstructors())
            {
                try
                {
                    //try to resolve constructor parameters
                    var parameters = constructor.GetParameters().Select(parameter =>
                    {
                        try
                        {
                            //try to reslove via DI
                            var service = Resolve(parameter.ParameterType);
                            if (service == null)
                                //try to reslove Manual recursion 
                                service = ManualResolve(parameter.ParameterType);
                            return service;
                        }
                        catch (Exception ex)
                        {

                            throw new Exception($"Unknown dependency {ex.Message}");
                        }
                    });

                    //all is ok, so create instance
                    return Activator.CreateInstance(type, parameters.ToArray());
                }
                catch (Exception ex)
                {
                    innerException = ex;
                }
            }

            throw new Exception($"No Constractor found in this type {innerException.Message}");
        }

        public virtual IServiceProvider ServiceProvider => _serviceProvider;

       
    }
}
