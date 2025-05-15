using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pharmacy.Core.Models;
using Pharmacy.DataAccess.Entities;

namespace Pharmacy.DataAccess.Configurations
{
    public class ProductEntityConfiguration : IEntityTypeConfiguration<ProductEntity>
    {
        public void Configure(EntityTypeBuilder<ProductEntity> builder)
        {
            builder.ToTable("product");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                .HasColumnName("id")
                .IsRequired();

            builder.Property(p => p.Name)
                .HasColumnName("name")
                .IsRequired()
                .HasMaxLength(Product.MAX_NAME_LENGTH);

            builder.Property(p => p.Description)
                .HasColumnName("description");

            builder.Property(p => p.Price)
                .HasColumnName("price")
                .IsRequired()
                .HasColumnType("Decimal(10,2)");

            builder.Property(p => p.PurchasePrice)
                .HasColumnName("purchase_price")
                .IsRequired()
                .HasColumnType("Decimal(10,2)");

            builder.Property(p => p.PurchaseDate)
               .HasColumnName("purchase_date")
               .IsRequired();

            builder.Property(p => p.Quantity)
               .HasColumnName("quantity")
               .HasDefaultValue("0");

            builder.Property(p => p.ImageUrl)
                .HasColumnName("image_url")
                .IsRequired()
                .HasMaxLength(Product.MAX_IMAGE_URL);

            builder.Property(p => p.CompanyId)
              .HasColumnName("company_id")
              .IsRequired();

            builder.Property(p => p.CatalogId)
              .HasColumnName("catalog_id")
              .IsRequired();

            builder.HasOne(p => p.Company)
              .WithMany(c => c.Products)
              .HasForeignKey(p => p.CompanyId);

            builder.HasOne(p => p.Catalog)
               .WithMany(c => c.Products)
               .HasForeignKey(p => p.CatalogId);
        }
    }
}
