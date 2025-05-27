
namespace Pharmacy.Core.Abstractions
{
    public interface IEmailVerificationsService
    {
        Task SendResetToken(int userId, string email);
        Task SendVerificationToken(int userId, string email);
        Task<bool> SwitchTokenIsUsed(String token);
        Task<bool> VerifyToken(Guid token);
    }
}