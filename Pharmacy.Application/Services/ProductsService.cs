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

    }
}
