using System.Text.RegularExpressions;

namespace Pharmacy.Core.Models
{
    public class User
    {
        public const int MAX_PASS_LENGTH = 16;
        public const int MIN_PASS_LENGTH = 8;
        public const int MAX_PHONE_LENGTH = 13;
        public const int MAX_EMAIL_LENGTH = 254;
        public const int MAX_LOGIN_LENGTH = 16;
        public const int MIN_LOGIN_LENGTH = 3;

        private User(string login, string email, string pass, string phone)
        {
            Login = login;
            Email = email;
            Password = pass;
            PhoneNumber = phone;
        }

        public User(int id, string login, string email, string pass, string phone, int roleId)
            : this(login, email, pass, phone)
        {
            Id = id;
            RoleID = roleId;
        }

        public int Id { get; set; }
        public string Login { get; }
        public string Email { get; }
        public string Password { get; }
        public string PhoneNumber { get; }
        public bool IsVerified { get; private set; } = false;
        public int? RoleID { get; }


        public static string? ValidateUserInput(string login, string email, string pass, string phone)
        {
            if (string.IsNullOrWhiteSpace(login) ||
                login.Length < MIN_LOGIN_LENGTH || login.Length > MAX_LOGIN_LENGTH ||
                !Regex.IsMatch(login, @"^[a-zA-Z0-9]+$"))
            {
                return "Логін повинен бути довжиною від 3 до 16 символів і містити лише латинські літери та цифри.";
            }

            if (string.IsNullOrWhiteSpace(email) ||
                email.Length > MAX_EMAIL_LENGTH ||
                !email.EndsWith("@gmail.com") ||
                !Regex.IsMatch(email, @"^[^@\s]+@gmail\.com$"))
            {
                return "Електронна пошта має бути дійсною адресою Gmail і містити не більше 254 символів.";
            }

            if (string.IsNullOrWhiteSpace(pass) ||
                pass.Length < MIN_PASS_LENGTH || pass.Length > MAX_PASS_LENGTH ||
                !Regex.IsMatch(pass, @"^(?=.*[A-Za-z])(?=.*\d).+$"))
            {
                return "Пароль повинен складатися з 8-16 символів і містити принаймні одну літеру та одну цифру.";
            }

            if (string.IsNullOrWhiteSpace(phone) ||
                phone.Length != MAX_PHONE_LENGTH ||
                !phone.StartsWith("+380") ||
                !Regex.IsMatch(phone, @"^\+380\d{9}$"))
            {
                return "Номер телефону повинен починатися з +380 і містити рівно 13 символів (наприклад, +380XXXXXXXXX).";
            }

            return null;
        }

        public static User Create(string login, string email, string hashedPassword, string phone)
        {
            return new User(login, email, hashedPassword, phone);
        }

    }
}
