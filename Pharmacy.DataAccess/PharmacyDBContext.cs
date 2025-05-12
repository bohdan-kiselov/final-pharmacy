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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserEntityConfiguration());
            modelBuilder.ApplyConfiguration(new EmailTokenConfiguration());
            modelBuilder.ApplyConfiguration(new RoleEntityConfiguration());
        }
    }
}
