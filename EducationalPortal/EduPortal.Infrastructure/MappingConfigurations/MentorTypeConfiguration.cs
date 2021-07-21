using EduPortal.Domain.Models.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EduPortal.Infrastructure.MappingConfigurations
{
    public class MentorTypeConfiguration : IEntityTypeConfiguration<Mentor>
    {
        public void Configure(EntityTypeBuilder<Mentor> builder)
        {
            builder.HasKey(m => m.Id);
            builder.HasOne(m => m.User).WithOne(u => u.Mentor).HasForeignKey("Mentor");

            builder.HasMany(m => m.CreatedMaterials)
                .WithOne(m => m.Author)
                .HasForeignKey(m => m.AuthorId);

            builder.HasMany(m => m.CreatedCourses)
                .WithOne(c => c.Author)
                .HasForeignKey(c => c.AuthorId);
        }
    }
}
