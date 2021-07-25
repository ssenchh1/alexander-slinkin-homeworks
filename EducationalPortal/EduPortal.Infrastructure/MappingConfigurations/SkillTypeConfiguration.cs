using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EduPortal.Domain.Models;
using EduPortal.Domain.Models.Joining;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EduPortal.Infrastructure.MappingConfigurations
{
    public class SkillTypeConfiguration : IEntityTypeConfiguration<Skill>
    {
        public void Configure(EntityTypeBuilder<Skill> builder)
        {
            builder.HasKey(s => s.Id);

            builder.Property(s => s.Name).IsRequired().HasMaxLength(128);

            builder.HasMany(s => s.Materials)
                .WithMany(m => m.ProvidedSkills)
                .UsingEntity<SkillMaterial>(sm => sm.HasOne(s => s.Material)
                    .WithMany(m => m.SkillMaterials).HasForeignKey(s => s.MaterialId),
                    sm => sm.HasOne(s => s.Skill)
                        .WithMany(s => s.SkillMaterials).HasForeignKey(s => s.SkillId),
                    sm =>
                    {
                        sm.Property(s => s.Level).HasDefaultValue(1);
                        sm.HasKey(s => new {s.MaterialId, s.SkillId});
                    });

            builder.HasMany(s => s.Users)
                .WithMany(u => u.Skills)
                .UsingEntity<SkillUser>(su => su.HasOne(s => s.User)
                    .WithMany(u => u.SkillUsers)
                    .HasForeignKey(s => s.UserId),
                    su => su.HasOne(s => s.Skill)
                        .WithMany(s => s.SkillUsers)
                        .HasForeignKey(s => s.SkillId),
                    su =>
                    {
                        su.Property(s => s.Level).HasDefaultValue(1);
                        su.HasKey(s => new {s.UserId, s.SkillId});
                    });
        }
    }
}
