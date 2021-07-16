using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Practice9june.Core.Models;

namespace Practice9june.Infrastructure.MappingConfigurations
{
    public class DirectoryPermissionTypeConfiguration : IEntityTypeConfiguration<DirectoryPermission>
    {
        public void Configure(EntityTypeBuilder<DirectoryPermission> builder)
        {
            builder.HasKey(p => new {p.UserId, p.DirectoryId});

            builder.Property(p => p.CanRead).IsRequired();
            builder.Property(p => p.CanWrite).IsRequired();

            builder.HasOne(p => p.Directory)
                .WithMany(d => d.DirectoryPermissions)
                .HasForeignKey(p => p.DirectoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(p => p.User)
                .WithMany(u => u.DirectoryPermissions)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
