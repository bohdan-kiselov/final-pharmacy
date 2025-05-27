using Microsoft.EntityFrameworkCore;
using Pharmacy.Core.Models;

namespace Pharmacy.DataAccess.Repositories
{
    public class ProductsRepository : IProductsRepository
    {
        private readonly PharmacyDBContext _context;

        public ProductsRepository(PharmacyDBContext dbContext)
        {
            _context = dbContext;
        }

        public async Task<List<Product>> GetProductsAndCompany(int amount)
        {
            var allIds = await _context.Products.Select(p => p.Id).ToListAsync();

            var rnd = new Random();
            var randomIds = new HashSet<int>();

            while (randomIds.Count < amount)
            {
                var randomId = allIds[rnd.Next(allIds.Count)];
                randomIds.Add(randomId);
            }

            var products = await _context.Products
                .Include(p => p.Company)
                .Where(p => randomIds.Contains(p.Id))
                .AsNoTracking()
                .ToListAsync();

            var randomProducts = products
                .Select(p => new Product(p.Id, p.Name, p.Description, p.Price, p.PurchasePrice,
                p.PurchaseDate, p.Quantity, p.ImageUrl, new Company(p.Company.CompanyName, p.Company.Email, p.Company.Country))).ToList();

            return randomProducts;


        }

        public async Task<List<Product>> GetProductsByName(String productName)
        {
            var products = await _context.Products
                .Include(p => p.Company)
                .AsNoTracking()
                .Where(p => EF.Functions.Like(p.Name.ToLower(), $"%{productName}%"))
                .ToListAsync();

            return products.Select(p => new Product(
                p.Id, p.Name, p.Description, p.Price, p.PurchasePrice,
                p.PurchaseDate, p.Quantity, p.ImageUrl,
                new Company(p.Company.CompanyName, p.Company.Email, p.Company.Country)
            )).ToList();
        }

    }
}
