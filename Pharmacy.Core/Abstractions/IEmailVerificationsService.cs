
namespace Pharmacy.Core.Abstractions
{
    public interface IEmailVerificationsService
    {
        Task SendVerificationToken(int id, string email);
        Task<bool> VerifyToken(Guid token);
    }
}