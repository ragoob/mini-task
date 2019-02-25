using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Extentions
{
   public interface IEngine
    {
        T Resolve<T>() where T : class;
        object Resolve(Type type);

        IEnumerable<T> ResolveAll<T>();


        void Initialize(IServiceCollection services);

        IServiceProvider ConfigureServices(IServiceCollection services, IConfiguration configuration);
    }
}
