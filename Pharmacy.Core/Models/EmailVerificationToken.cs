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
        

        public EmailVerificationToken(int userId, Guid token, DateTime expiresAt)
        {
            UserId = userId;
            Token = token;
            ExpiresAt = expiresAt;
        }

        public EmailVerificationToken(int userId, Guid token, DateTime expiresAt, bool used)
            : this (userId, token, expiresAt)
        {
            Used = used;
        }


    }
}
