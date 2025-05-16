namespace Pharmacy.Core.Models
{
    public class EmailVerificationToken
    {

        public int UserId { get; }
        public Guid Token { get; }
        public DateTime ExpiresAt { get;  }
        public bool Used { get; private set; } = false;

        public bool IsExpired() => DateTime.UtcNow > ExpiresAt;

        public void MarkAsUsed() => Used = true;

        public User? user { get; }

        public EmailVerificationToken(int userId, Guid token, DateTime expiresAt, User? user = null)
        {
            UserId = userId;
            Token = token;
            ExpiresAt = expiresAt;
            this.user = user;
        }

        public EmailVerificationToken(int userId, Guid token, DateTime expiresAt, bool used)
            : this (userId, token, expiresAt)
        {
            Used = used;
        }


    }
}
