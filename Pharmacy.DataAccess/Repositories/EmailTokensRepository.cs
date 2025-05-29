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
            var oldTokens = _context.EmailVerificationTokens.Where(t => t.UserId == userId);

            _context.EmailVerificationTokens.RemoveRange(oldTokens);

            var tokenEntity = new EmailVerificationTokenEntity
            {
                UserId = userId,
                Token = token,
                ExpiresAt = DateTime.UtcNow.AddMinutes(2)
            };

            await _context.EmailVerificationTokens.AddAsync(tokenEntity);
            await _context.SaveChangesAsync();
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
            tokenEntity.Used = true;

            await _context.SaveChangesAsync();
            return true;
        }

    }
}
