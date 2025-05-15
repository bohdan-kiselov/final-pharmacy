using Microsoft.EntityFrameworkCore;
using Pharmacy.DataAccess.Configurations;
using Pharmacy.DataAccess.Entities;

namespace Pharmacy.DataAccess
{
    public class PharmacyDBContext : DbContext
    {
        public PharmacyDBContext(DbContextOptions<PharmacyDBContext> options)
            : base(options){}

        public required DbSet<UserEntity> Users { get; set; }
        public required DbSet<EmailVerificationTokenEntity> EmailVerificationTokens { get; set; }
        public required DbSet<ProductEntity> Products { get; set; }
        public required DbSet<CompanyEntity> Companies { get; set; }
        public required DbSet<CatalogEntity> Catalog { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserEntityConfiguration());
            modelBuilder.ApplyConfiguration(new EmailTokenConfiguration());
            modelBuilder.ApplyConfiguration(new RoleEntityConfiguration());
            modelBuilder.ApplyConfiguration(new CatalogEntityConfiguration());
            modelBuilder.ApplyConfiguration(new CompanyEntityConfiguration());
            modelBuilder.ApplyConfiguration(new ProductEntityConfiguration());

        }
    }
}
