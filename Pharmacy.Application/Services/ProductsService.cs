using Pharmacy.Core.Models;
using Pharmacy.DataAccess.Repositories;

namespace Pharmacy.Application.Services
{
    public class ProductsService : IProductsService
    {
        private readonly IProductsRepository _productRepository;

        public ProductsService(IProductsRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<List<Product>> GetFourProducts()
        {
            return await _productRepository.GetProductsAndCompany(4);
        }

        public async Task<List<Product>> SearchProducts(List<string> rawNames)
        {
            var normalizedNames = rawNames
                .Select(n => n.Trim().ToLower())
                .Where(n => !string.IsNullOrWhiteSpace(n))
                .Distinct()
                .ToList();

            List<Product> foundProducts = new List<Product>();

            foreach (var normalizedName in normalizedNames) 
            {
                var results = await _productRepository.GetProductsByName(normalizedName);
                foundProducts.AddRange(results);
            }

            return foundProducts;
        }
    }
}
