using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EduPortal.Domain.Models.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EduPortal.Infrastructure.MappingConfigurations
{
    public class UserTypeConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);

            builder.Property(u => u.Email).IsRequired();
            builder.Property(u => u.Login).IsRequired().HasMaxLength(128);
            builder.Property(u => u.Password).IsRequired().HasMaxLength(128);
            builder.Property(u => u.PhoneNumber).HasMaxLength(128);
            builder.Property(u => u.Role).IsRequired().HasMaxLength(128);

            builder.HasMany(u => u.Skills)
                .WithMany(s => s.Users);
        }
    }
}
