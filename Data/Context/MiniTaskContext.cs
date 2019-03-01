using Data.Mapping;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Infrastructure.Extentions;
using Core.Infrastructure;

namespace Data.Context
{
    public class MiniTaskContext : DbContext
    {

        public MiniTaskContext(DbContextOptions<MiniTaskContext> options) : base(options)
        {
        }

        public MiniTaskContext() { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                
                var connectionString = EngineContext.Current.Resolve<IConfiguration>().GetConnectionString("DefaultConnection");
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
          
            // find all BaseEntityTypeConfiguration types  via Reflection 
            var typeConfigurations = Assembly.GetExecutingAssembly().GetTypes().Where(type =>
                          (type.BaseType?.IsGenericType ?? false)
                              && (type.BaseType.GetGenericTypeDefinition() == typeof(BaseEntityTypeConfiguration<>)
                                  ));
            typeConfigurations.ToList().ForEach(c => { ((IMapConfiguration)Activator.CreateInstance(c)).ApplyConfiguration(modelBuilder); });
            base.OnModelCreating(modelBuilder);
        }
    }
}
