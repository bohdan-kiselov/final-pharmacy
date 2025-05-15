using Pharmacy.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pharmacy.Core.Models;

namespace Pharmacy.DataAccess.Configurations
{
    public class UserEntityConfiguration : IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            builder.ToTable("users");

            builder.HasKey(u => u.Id);

            builder.Property(u => u.Id)
                .HasColumnName("id")
                .IsRequired();

            builder.Property(u => u.Login)
                .HasColumnName("login")
                .IsRequired()
                .HasMaxLength(User.MAX_LOGIN_LENGTH);

            builder.Property(u => u.Email)
                .HasColumnName("email")
                .IsRequired()
                .HasMaxLength(User.MAX_EMAIL_LENGTH);

            builder.Property(u => u.HashPassword)
                .HasColumnName("password_hash")
                .IsRequired()
                .HasMaxLength(User.MAX_PASS_LENGTH);

            builder.Property(u => u.PhoneNumber)
                .HasColumnName("phone_number")
                .IsRequired()
                .HasMaxLength(User.MAX_PHONE_LENGTH);

            builder.Property(u => u.IsVerified)
                .HasColumnName("is_verified")
                .HasDefaultValue(false);

            builder.Property(u => u.CreatedAt)
                .HasColumnName("created_at")
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(u => u.RoleID)
                .HasColumnName("role_id")
                .HasDefaultValueSql("1");

            builder.HasOne(u => u.EmailVerificationToken)
                .WithOne(t => t.User)
                .HasForeignKey<EmailVerificationTokenEntity>(t => t.UserId);

            builder.HasOne(u => u.Role)
                .WithMany(r => r.Users)
                .HasForeignKey(u => u.RoleID);

        }
    }
}
