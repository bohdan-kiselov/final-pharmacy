namespace Pharmacy.Core.Models
{
    public class Role
    {
        public const int MAX_NAME_LENGTH = 20;

        

        public int Id { get;  }
        public string RoleName { get; }

        public Role(int id, string roleName)
        {
            Id = id;
            RoleName = roleName;
        }
    }
}
