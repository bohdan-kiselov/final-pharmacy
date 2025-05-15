using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Pharmacy.DataAccess.Entities
{
    public class ProductEntity
    {
        public int Id { get; set; }

        public required string Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public decimal PurchasePrice { get; set; }
        public DateOnly PurchaseDate { get; set; }
        public int Quantity { get; set; }
        public required string ImageUrl { get; set; }

        public int CompanyId { get; set; }
        public CompanyEntity Company { get; set; } = default!;

        public int CatalogId { get; set; }
        public CatalogEntity Catalog { get; set; } = default!;

    }
}
