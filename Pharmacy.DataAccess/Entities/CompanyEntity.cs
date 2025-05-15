using System.ComponentModel.DataAnnotations;

namespace Pharmacy.DataAccess.Entities
{
    public class CompanyEntity
    {
        public int Id { get; set; }

        public required string CompanyName { get; set; }
        public required string Email { get; set; }
        public required string Country { get; set; }

        public ICollection<ProductEntity> Products { get; set; } = new List<ProductEntity>();

    }
}
