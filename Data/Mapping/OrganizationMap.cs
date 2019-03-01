using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Mapping
{
   public class OrganizationMap : BaseEntityTypeConfiguration<Organization>
    {
        public override void Configure(EntityTypeBuilder<Organization> builder)
        {
            builder.ToTable("Organizations", "dbo");
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Name)
                .HasColumnType("nvarchar(100)")
                .HasMaxLength(100)
                .IsRequired();
            builder.Property(c => c.EmployeesCount);
           
        }
    }
}
