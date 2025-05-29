using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pharmacy.Core.Models;
using Pharmacy.DataAccess.Entities;

namespace Pharmacy.DataAccess.Configurations
{
    public class EmailTokenConfiguration : IEntityTypeConfiguration<EmailVerificationTokenEntity>
    {
        public void Configure(EntityTypeBuilder<EmailVerificationTokenEntity> builder)
        {
            builder.ToTable("email_verification_tokens");

            builder.HasKey(t => t.UserId);

            builder.Property(t => t.UserId)
                .HasColumnName("user_id")
                .IsRequired();

            builder.Property(t => t.Token)
                .HasColumnName("token")
                .IsRequired();

            builder.HasIndex(e => e.Token).IsUnique();

            builder.Property(t => t.ExpiresAt)
                .HasColumnName("expires_at")
                .IsRequired();

            builder.Property(t => t.Used)
                .HasColumnName("used")
                .IsRequired();

            builder.HasOne(t => t.User)
                .WithOne(u => u.EmailVerificationToken)
                .HasForeignKey<EmailVerificationTokenEntity>(t => t.UserId);

        }
    }
}
