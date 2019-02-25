using Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Mapping
{
    public abstract class BaseEntityTypeConfiguration<TEntity> : IMapConfiguration, IEntityTypeConfiguration<TEntity> where TEntity : Entity
    {
        /// <summary>
        /// devloper can add adtional code 
        /// </summary>
        /// <param name="builder"></param>
        protected virtual void PostConfigure(EntityTypeBuilder<TEntity> builder)
        {
        }
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            PostConfigure(builder);
        }
        /// <summary>
        /// apply configration to passed modelbuilder
        /// </summary>
        /// <param name="modelBuilder"></param>
        public virtual void ApplyConfiguration(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(this);
        }
    }


}
