using System.ComponentModel.Design;

namespace Pharmacy.Core.Models
{
    public class Product
    {
        public const int MAX_NAME_LENGTH = 100;
        public const int MAX_IMAGE_URL = 254;

        public int Id { get; }

        public string Name { get; }
        public string? Description { get; }
        public decimal Price { get; }
        public decimal PurchasePrice { get; }
        public DateOnly PurchaseDate { get; } 
        public int Quantity { get; }
        public string ImageUrl { get; }

        public int CompanyId { get; }
        public int CatalogId { get; }

       
        public Company? Company { get; }
        public Catalog? Catalog { get; }

        public Product(int id, string name, string? description, decimal price, decimal purchasePrice, DateOnly purchaseDate,
            int quantity, string imageUrl, Company? company = null, Catalog? catalog = null)
        {
            Id = id;
            Name = name;
            Description = description;
            Price = price;
            PurchasePrice = purchasePrice;
            PurchaseDate = purchaseDate;
            Quantity = quantity;
            ImageUrl = imageUrl;

            Company = company;
            Catalog = catalog;

            CompanyId = company?.Id ?? 0;
            CatalogId = catalog?.Id ?? 0;
        }


    }
}
