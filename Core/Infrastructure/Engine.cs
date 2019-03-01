using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Internal;
using System.Net;
using Microsoft.AspNetCore.Hosting;
using Core.Plugins;

namespace Core.Infrastructure
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

            var provider = services.BuildServiceProvider();
            var hostingEnvironment = provider.GetRequiredService<IHostingEnvironment>();
            EngineContext.DefaultFileProvider = new TaskFileProvider(hostingEnvironment);
            //initialize plugins
            var mvcCoreBuilder = services.AddMvcCore();
            PluginManager.Initialize(mvcCoreBuilder.PartManager);

            _serviceProvider = services.BuildServiceProvider();
        }

        public IServiceProvider ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {

            var typeFinder = new DefaultTypeFinder();



            //register Plugins dependancies 

            var startupConfigurations = typeFinder.FindClassesOfType<IMiniTaskStartUp>();
            var instances = startupConfigurations
            .Select(startup => (IMiniTaskStartUp)Activator.CreateInstance(startup));
            foreach (var instance in instances)
                instance.ConfigureServices(services, configuration);

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
                            return service;
                        }
                        catch (Exception ex)
                        {
                            try
                            {
                                //try manual
                                var service = ManualResolve(parameter.ParameterType);

                                return service;
                            }
                            catch (Exception exc)
                            {

                                throw new Exception($"can not resolve paramter {exc.Message}");
                            }
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
