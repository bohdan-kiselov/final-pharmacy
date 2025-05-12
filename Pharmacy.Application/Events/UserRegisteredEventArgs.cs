namespace Pharmacy.Application.Events
{
    public class UserRegisteredEventArgs : EventArgs
    {
        public int UserId { get; }
        public string Email { get; }

        public UserRegisteredEventArgs(int userId, string email)
        {
            UserId = userId;
            Email = email;
        }
    }
}
