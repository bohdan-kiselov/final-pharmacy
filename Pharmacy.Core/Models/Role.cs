namespace Pharmacy.Core.Models
{
    public class Role
    {
        public int Id { get;}
        public string RoleName { get;}

        public Role(int id, string name) 
        {
            Id = id;
            RoleName = name;
        }
    }
}
