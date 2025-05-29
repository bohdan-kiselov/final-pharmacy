using System.ComponentModel.DataAnnotations;

namespace Pharmacy.DataAccess.Entities
{
    public class CatalogEntity
    {
        public int Id { get; set; }

        public required string CatalogName { get; set; }

        public ICollection<ProductEntity> Products { get; set; } = new List<ProductEntity>();
    }
}
