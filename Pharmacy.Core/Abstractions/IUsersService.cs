using Pharmacy.Core.Models;

namespace Pharmacy.Core.Abstractions
{
    public interface IUsersService
    {
        Task<User?> GetProfile(int userId);
        Task<(User? user, string? error)> Register(string login, string email, string password, string phone);
        Task<(User? updatedUser, string? error)> UpdateAccountData(User existingUser, string? newLogin, string? newEmail, string? newPhone, string? newPass);
        Task<bool> UpdateUser(User user);
        Task<(bool IsValid, User? user)> ValidateUserCredentials(string login, string password);
    }
}