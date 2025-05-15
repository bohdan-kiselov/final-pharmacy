using Pharmacy.Core.Models;

namespace Pharmacy.Application.Services
{
    public interface IProductsService
    {
        Task<List<Product>> GetFourProducts();
    }
}