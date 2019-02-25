using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Mapping
{
    public class ProjectMap : BaseEntityTypeConfiguration<Project>
    {
        public override void Configure(EntityTypeBuilder<Project> builder)
        {
            builder.ToTable("Projects");
            builder.Property(p => p.Id)
                .HasColumnName("Id");

            builder.HasIndex(p => p.Name)
                .IsUnique();

            builder.Property(p => p.Name)
                .HasColumnType("nvarchar(100)")
                .HasMaxLength(100)
                 .IsRequired();


            builder.Property(p => p.Description)
                .HasColumnType("nvarchar(500)")
                .HasMaxLength(500);
               

                
        }
    }
}
