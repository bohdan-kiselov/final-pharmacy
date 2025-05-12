using Pharmacy.Core.Models;

namespace Pharmacy.DataAccess.Repositories
{
    public interface IEmailTokensRepository
    {
        Task<bool> ConfirmEmail(Guid token);
        Task Create(int userId, Guid token);
        Task<EmailVerificationToken?> GetValidToken(Guid token);
        Task MarkTokenAsUsed(Guid tokenId);
    }
}