using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Practice9june.Core.Models;

namespace Practice9june.Infrastructure.MappingConfigurations
{
    public class DirectoryTypeConfiguration : IEntityTypeConfiguration<Directory>
    {
        public void Configure(EntityTypeBuilder<Directory> builder)
        {
            builder.HasKey(d => d.Id);

            builder.Property(d => d.Title).IsRequired().HasMaxLength(128);

            builder.HasOne(d => d.ParentDirectory)
                .WithMany(d => d.Directories)
                .HasForeignKey(d => d.ParentDirectoryId);

            builder.HasMany(d => d.DirectoryPermissions)
                .WithOne(p => p.Directory)
                .HasForeignKey(p => p.DirectoryId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
