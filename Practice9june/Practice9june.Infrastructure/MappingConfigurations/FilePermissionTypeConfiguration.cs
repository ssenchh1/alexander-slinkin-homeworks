using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Practice9june.Core.Models;

namespace Practice9june.Infrastructure.MappingConfigurations
{
    public class FilePermissionTypeConfiguration : IEntityTypeConfiguration<FilePermission>
    {
        public void Configure(EntityTypeBuilder<FilePermission> builder)
        {
            builder.HasKey(p => new {p.UserId, p.FileId});

            builder.Property(p => p.CanWrite).IsRequired();
            builder.Property(p => p.CanRead).IsRequired();

            builder.HasOne(p => p.File)
                .WithMany(f => f.FilePermissions)
                .HasForeignKey(p => p.FileId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(p => p.User)
                .WithMany(u => u.FilePermissions)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
