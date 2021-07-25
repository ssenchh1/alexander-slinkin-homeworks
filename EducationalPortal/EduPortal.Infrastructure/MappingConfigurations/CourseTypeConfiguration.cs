using EduPortal.Domain.Models;
using EduPortal.Domain.Models.Joining;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EduPortal.Infrastructure.MappingConfigurations
{
    public class CourseTypeConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name).IsRequired().HasMaxLength(128);
            builder.Property(c => c.Description).IsRequired().HasMaxLength(1000);

            builder.HasMany(c => c.Materials)
                .WithMany(m => m.Courses)
                .UsingEntity<CourseMaterial>(
                    cm => cm.HasOne(c => c.Material)
                        .WithMany(m => m.CourseMaterials)
                        .HasForeignKey(m => m.MaterialId)
                        .OnDelete(DeleteBehavior.Restrict),
                    cm => cm.HasOne(c => c.Course)
                        .WithMany(c => c.CourseMaterials)
                        .HasForeignKey(c => c.CourseId)
                        .OnDelete(DeleteBehavior.Restrict),
                    cm => cm.HasKey(k => new {k.CourseId, k.MaterialId}));

            builder.HasMany(c => c.Students)
                .WithMany(s => s.Courses);

            builder.HasMany(c => c.FinishedStudents)
                .WithMany(s => s.FinishedCourses);
        }
    }
}
