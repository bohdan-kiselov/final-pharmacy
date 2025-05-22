namespace Pharmacy.Core.Models
{
    public class Company
    {
        public const int MAX_NAME_LENGTH = 100;
        public const int MAX_EMAIL_LENGTH = 254;
        public const int MAX_COUNTRY_LENGTH = 50;

        public int Id { get; }

        public string CompanyName { get; }
        public string Email { get; }
        public string Country { get; }

        public IReadOnlyCollection<Product> Products { get; } = new List<Product>().AsReadOnly();

        public Company(string companyName, string email, string country)
        {
            CompanyName = companyName;
            Email = email;
            Country = country;
        }

        public Company(int id, string companyName, string email, string country, IEnumerable<Product>? products = null)
            : this(companyName, email, country)
        {
            Id = id;
            if (products != null)
            {
                Products = products.ToList().AsReadOnly();
            }
        }

    }
}
