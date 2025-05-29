using Pharmacy.Core.Models;

namespace Pharmacy.DataAccess.Repositories
{
    public interface IUsersRepository
    {
        Task<User> Create(User user);
        Task<bool> FindEmail(string email);
        Task<bool> FindLogin(string login);
        Task<bool> FindPhone(string phone);
        Task<User?> FindUserByValidToken(string token);
        Task<User?> GetUser(int userId);
        Task<User?> GetUserByEmail(string email);
        Task<User?> GetUserByLogin(string login);
        Task<bool> SwitchIsVerified(int userId);
        Task<bool> Update(User user);
        Task<bool> UpdatePassword(int userId, string hashedPass);
    }
}