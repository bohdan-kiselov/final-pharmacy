using System.ComponentModel.DataAnnotations;

namespace Pharmacy.DataAccess.Entities
{
    public class RoleEntity
    {
        public int Id { get; set; }

        public required string RoleName { get; set; }

        public ICollection<UserEntity> Users { get; set; } = new List<UserEntity>();
    }
}
