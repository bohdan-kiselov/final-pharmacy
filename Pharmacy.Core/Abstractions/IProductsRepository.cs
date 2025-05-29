using Pharmacy.Core.Models;

namespace Pharmacy.DataAccess.Repositories
{
    public interface IProductsRepository
    {
        Task<List<Product>> GetProductsAndCompany(int amount);
        Task<List<Product>> GetProductsByName(String productName);
    }
}