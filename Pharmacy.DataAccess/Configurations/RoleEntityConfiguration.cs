using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pharmacy.DataAccess.Entities;

namespace Pharmacy.DataAccess.Configurations
{
    public class RoleEntityConfiguration : IEntityTypeConfiguration<RoleEntity>
    {
        public void Configure(EntityTypeBuilder<RoleEntity> builder)
        {
            builder.ToTable("role");

            builder.HasKey(s => s.Id);


            builder.Property(s => s.Id)
                .HasColumnName("id")
                .IsRequired();

            builder.Property(s => s.RoleName)
                .HasColumnName("role_name")
                .IsRequired();


            builder.HasMany(s => s.Users)
                .WithOne(u => u.Roles)
                .HasForeignKey(u => u.RoleID);
        }
    }
}
