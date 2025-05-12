using Pharmacy.Core.Models;

namespace Pharmacy.DataAccess.Repositories
{
    public interface IUsersRepository
    {
        Task<User> Create(User user);
        Task<bool> FindEmail(string email);
        Task<bool> FindLogin(string login);
        Task<User?> GetUser(int userId);
        Task<User?> GetUserByLogin(string login);
        Task<bool> Update(User user);
    }
}