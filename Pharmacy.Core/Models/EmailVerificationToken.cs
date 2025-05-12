namespace Pharmacy.Core.Models
{
    public class EmailVerificationToken
    {
        public int UserId { get; }
        public Guid Token { get; }
        public DateTime ExpiresAt { get; }
        public bool Used { get; private set; } = false;

        public EmailVerificationToken( int userId, Guid token, DateTime expiresAt)
        {
            UserId = userId;
            Token = token;
            ExpiresAt = expiresAt;
        }

        public bool IsExpired() => DateTime.UtcNow > ExpiresAt;

        public void MarkAsUsed() => Used = true;
    }
}
