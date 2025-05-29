using Pharmacy.Core.Models;

namespace Pharmacy.Application.Services
{
    public interface IProductsService
    {
        Task<List<Product>> GetFourProducts();
        Task<List<Product>> SearchProducts(List<string> rawNames);
    }
}