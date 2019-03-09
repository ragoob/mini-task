using Core.Infrastructure;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;


namespace Data.Context
{
   public class EventSourceContext : DbContext
    {
        public virtual DbSet<EventSource> EventSources { get; set; }
        public EventSourceContext(DbContextOptions<EventSourceContext> options) : base(options)
        {
        }

        public EventSourceContext() { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

                var connectionString = EngineContext.Current.Resolve<IConfiguration>().GetConnectionString("EventConnection");
                optionsBuilder.UseSqlServer(connectionString);
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EventSource>().ToTable("EventSources");
            modelBuilder.Entity<EventSource>().HasKey(e=> e.Id);


            base.OnModelCreating(modelBuilder);
        }

    }
}
