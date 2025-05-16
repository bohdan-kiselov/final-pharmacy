using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pharmacy.Core.Models;
using Pharmacy.DataAccess.Entities;

namespace Pharmacy.DataAccess.Configurations
{
    public class CatalogEntityConfiguration : IEntityTypeConfiguration<CatalogEntity>
    {
        public void Configure(EntityTypeBuilder<CatalogEntity> builder)
        {
            builder.ToTable("catalog");

            builder.HasKey(cat => cat.Id);

            builder.Property(cat => cat.Id)
                .HasColumnName("id")
                .IsRequired();

            builder.Property(cat => cat.CatalogName)
                .HasColumnName("catalog_name")
                .IsRequired()
                .HasMaxLength(Catalog.MAX_NAME_LENGTH);

            builder.HasIndex(cat => cat.CatalogName).IsUnique();

            builder.HasMany(cat => cat.Products)
                .WithOne(p => p.Catalog)
                .HasForeignKey(p => p.CatalogId);
        }
    }
}
