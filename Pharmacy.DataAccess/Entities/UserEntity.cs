using System.ComponentModel.DataAnnotations;

namespace Pharmacy.DataAccess.Entities
{
    public class UserEntity
    {
        [Key]
        public int Id { get; set; }

        public required string Login { get; set; }
        public required string Email { get; set; }
        public required string HashPassword { get; set; }
        public required string PhoneNumber { get; set; }
        public bool IsVerified { get; set; }
        public DateTime CreatedAt { get; set; } // yyyy-MM-dd HH:mm
        public int RoleID { get; set; }

        public EmailVerificationTokenEntity? EmailVerificationToken { get; set; }
        public RoleEntity? Roles { get; set; }
    }
}
