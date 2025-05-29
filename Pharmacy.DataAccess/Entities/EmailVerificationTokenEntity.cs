using System.ComponentModel.DataAnnotations;

namespace Pharmacy.DataAccess.Entities
{
    public class EmailVerificationTokenEntity
    {
        public int Id { get; set; }
        public required int UserId { get; set; }

        public required Guid Token { get; set; }
        public required DateTime ExpiresAt { get; set; }
        public bool Used { get; set; } = false;

        public UserEntity? User { get; set; }
    }


}
