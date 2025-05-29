namespace Pharmacy.Core.Models
{
    public class EmailVerificationToken
    {
        public int Id { get; }
        public int UserId { get; }
        public Guid Token { get; }
        public DateTime ExpiresAt { get;  }
        public bool Used { get; private set; } = false;

        public bool IsExpired() => DateTime.UtcNow > ExpiresAt;

        public void MarkAsUsed() => Used = true;

        public User? user { get; }

        public EmailVerificationToken(int id, int userId, Guid token, DateTime expiresAt, bool used = false, User? user = null)
        {
            Id = id;
            UserId = userId;
            Token = token;
            ExpiresAt = expiresAt;
            Used = used;
            this.user = user;
        }

    }
}
