using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pharmacy.Core.Models;
using Pharmacy.DataAccess.Entities;

namespace Pharmacy.DataAccess.Configurations
{
    public class RoleEntityConfiguration : IEntityTypeConfiguration<RoleEntity>
    {
        public void Configure(EntityTypeBuilder<RoleEntity> builder)
        {
            builder.ToTable("role");

            builder.HasKey(r => r.Id);


            builder.Property(r => r.Id)
                .HasColumnName("id")
                .IsRequired();

            builder.Property(r => r.RoleName)
                .HasColumnName("role_name")
                .IsRequired()
                .HasMaxLength(Role.MAX_NAME_LENGTH);

            builder.HasIndex(r => r.RoleName).IsUnique();

            builder.HasMany(r => r.Users)
                .WithOne(u => u.Role)
                .HasForeignKey(u => u.RoleID);

            
        }
    }
}
