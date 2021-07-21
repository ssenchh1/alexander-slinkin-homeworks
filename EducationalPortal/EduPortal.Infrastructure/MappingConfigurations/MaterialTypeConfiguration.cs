using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EduPortal.Domain.Models.Materials;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EduPortal.Infrastructure.MappingConfigurations
{
    public class MaterialTypeConfiguration : IEntityTypeConfiguration<Material>
    {
        public void Configure(EntityTypeBuilder<Material> builder)
        {
            builder.HasKey(m => m.Id);

            builder.Property(m => m.Name).IsRequired().HasMaxLength(128);
            builder.Property(m => m.Category).IsRequired().HasMaxLength(128);

            builder.HasMany(m => m.ProvidedSkills)
                .WithMany(s => s.Materials);

            builder.HasOne(m => m.Author)
                .WithMany(m => m.CreatedMaterials)
                .HasForeignKey(m => m.AuthorId);
        }
    }
}
