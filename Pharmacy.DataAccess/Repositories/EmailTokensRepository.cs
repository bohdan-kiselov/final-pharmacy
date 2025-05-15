using Microsoft.EntityFrameworkCore;
using Pharmacy.Core.Models;
using Pharmacy.DataAccess.Entities;

namespace Pharmacy.DataAccess.Repositories
{
    public class EmailTokensRepository : IEmailTokensRepository
    {
        private readonly PharmacyDBContext _context;

        public EmailTokensRepository(PharmacyDBContext context)
        {
            _context = context;
        }

        public async Task Create(int userId, Guid token)
        {

            var tokenEntity = new EmailVerificationTokenEntity
            {
                UserId = userId,
                Token = token,
                ExpiresAt = DateTime.UtcNow.AddMinutes(2)
            };

            await _context.EmailVerificationTokens.AddAsync(tokenEntity);
            await _context.SaveChangesAsync();
        }

        public async Task<EmailVerificationToken?> GetValidToken(Guid token)
        {

            var tokenEntity = await _context.EmailVerificationTokens
                .FirstOrDefaultAsync(t =>
                    t.Token == token &&
                    !t.Used &&
                    t.ExpiresAt > DateTime.UtcNow);

            if (tokenEntity == null)
                return null;

            return new EmailVerificationToken(tokenEntity.UserId, tokenEntity.Token, tokenEntity.ExpiresAt);
        }

        public async Task MarkTokenAsUsed(Guid token)
        {
            var tokenEntity = await _context.EmailVerificationTokens
                .FirstOrDefaultAsync(t => t.Token == token);

            if (tokenEntity != null)
            {
                tokenEntity.Used = true;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ConfirmEmail(Guid token)
        {
            var tokenEntity = await _context.EmailVerificationTokens
                .Include(t => t.User)
                .FirstOrDefaultAsync(t =>
                    t.Token == token &&
                    !t.Used &&
                    t.ExpiresAt > DateTime.UtcNow);

            if (tokenEntity == null || tokenEntity.User == null)
                return false;

            tokenEntity.User.IsVerified = true;
            await MarkTokenAsUsed(token);

            await _context.SaveChangesAsync();
            return true;
        }

    }
}
