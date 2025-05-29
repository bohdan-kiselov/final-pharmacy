namespace Pharmacy.Core.Models
{
    public class Catalog
    {
        public const int MAX_NAME_LENGTH = 100;

        public int Id { get; }

        public string Name { get;  } 

        public IReadOnlyCollection<Product> Products { get; } = new List<Product>().AsReadOnly();

        public Catalog(string name, IReadOnlyCollection<Product> products)
        {
            Name = name;
            Products = products;
        }

        public Catalog(int id, string name, IEnumerable<Product>? products = null)
        {
            Id = id;
            Name = name;

            if (products != null)
            {
                Products = products.ToList().AsReadOnly();
            }
        }
    }
}
