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
    public class FileTypeConfiguration : IEntityTypeConfiguration<File>
    {
        public void Configure(EntityTypeBuilder<File> builder)
        {
            builder.HasKey(f => f.Id);

            builder.Property(f => f.Extention).IsRequired().HasMaxLength(5);
            builder.Property(f => f.Title).IsRequired().HasMaxLength(128);
            builder.Property(f => f.Size).IsRequired();

            builder.HasOne(f => f.Directory)
                .WithMany(d => d.Files)
                .HasForeignKey(f => f.DirectoryId);

            builder.HasMany(f => f.FilePermissions)
                .WithOne(p => p.File)
                .HasForeignKey(p => p.FileId);
        }
    }
}
