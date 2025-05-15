using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pharmacy.Core.Models;
using Pharmacy.DataAccess.Entities;

namespace Pharmacy.DataAccess.Configurations
{
    public class CompanyEntityConfiguration : IEntityTypeConfiguration<CompanyEntity>
    {
        public void Configure(EntityTypeBuilder<CompanyEntity> builder)
        {
            builder.ToTable("company");

            builder.HasKey(com => com.Id);

            builder.Property(com => com.Id)
                .HasColumnName("id")
                .IsRequired();

            builder.Property(com => com.CompanyName)
                .HasColumnName("company_name")
                .IsRequired()
                .HasMaxLength(Company.MAX_NAME_LENGTH);

            builder.Property(com => com.Email)
                .HasColumnName("email")
                .IsRequired()
                .HasMaxLength(Company.MAX_EMAIL_LENGTH);

            builder.Property(com => com.Country)
                .HasColumnName("country")
                .HasMaxLength(Company.MAX_COUNTRY_LENGTH);

            builder.HasMany(com => com.Products)
                .WithOne(p => p.Company)
                .HasForeignKey(p => p.CompanyId);
        }
    }
}
