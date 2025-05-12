using Pharmacy.Application.Events;
using Pharmacy.Core.Models;
using Pharmacy.DataAccess.Repositories;
using BCrypt.Net;
using Pharmacy.Core.Abstractions;
using System.Numerics;

namespace Pharmacy.Application.Services
{
    public class UsersService : IUsersService
    {
        public event EventHandler<UserRegisteredEventArgs> UserRegistered;

        private readonly IUsersRepository _userRepository;
        private readonly IEmailVerificationsService _emailVerificationService;
        private readonly int _workFactor = 12;

        public UsersService(IUsersRepository repository, IEmailVerificationsService emailVerificationService)
        {
            _userRepository = repository ?? throw new ArgumentNullException(nameof(repository));
            _emailVerificationService = emailVerificationService ?? throw new ArgumentNullException(nameof(emailVerificationService));

            UserRegistered += async (sender, args) =>
            {
                await _emailVerificationService.SendVerificationToken(args.UserId, args.Email);
            };
        }

        private string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password, _workFactor);
        }

        public async Task<(User? user, string? error)> Register(string login, string email, string password, string phone)
        {
            if (await _userRepository.FindLogin(login))
                return (null, "Логін вже зайнятий.");

            if (await _userRepository.FindEmail(email))
                return (null, "Електронна пошта вже зайнята.");

            var validationError = User.ValidateUserInput(login, email, password, phone);
            if (!string.IsNullOrEmpty(validationError))
                return (null, validationError);

            var hashedPassword = HashPassword(password);

            var user = User.Create(login, email, hashedPassword, phone);
            var createdUser = await _userRepository.Create(user);

            UserRegistered?.Invoke(this, new UserRegisteredEventArgs(createdUser.Id, createdUser.Email));

            return (createdUser, null);
        }

        public async Task<User?> GetProfile(int userId)
        {
            var user = await _userRepository.GetUser(userId);
            if (user == null) return null;

            return user;
        }

        public async Task<(bool IsValid, User? user)> ValidateUserCredentials(string login, string password)
        {
            var user = await _userRepository.GetUserByLogin(login);
            if (user == null)
                return (false, null);

            var isValid = BCrypt.Net.BCrypt.Verify(password, user.Password);
            return (isValid, isValid ? user : null);
        }

        public async Task<(User? updatedUser, string? error)> UpdateAccountData(User existingUser, string? newLogin, string? newEmail,
    string? newPhone, string? newPass, string? currentPassword)
        {
            if (!string.IsNullOrEmpty(newLogin) || !string.IsNullOrEmpty(newEmail) || !string.IsNullOrEmpty(newPhone) 
                || !string.IsNullOrEmpty(newPass))
            {
                if (string.IsNullOrEmpty(currentPassword))
                {
                    return (null, "Для зміни даних користувача необхідно ввести поточний пароль.");
                }
                if (!BCrypt.Net.BCrypt.Verify(currentPassword, existingUser.Password))
                {
                    return (null, "Невірний поточний пароль");
                }
            }

            string updatedLogin = existingUser.Login;
            if (!string.IsNullOrEmpty(newLogin) && newLogin != existingUser.Login)
            {
                if (await _userRepository.FindLogin(newLogin))
                {
                    return (null, "Логін вже зайнятий.");
                }
                updatedLogin = newLogin;
            }

            string updatedEmail = existingUser.Email;
            if (!string.IsNullOrEmpty(newEmail) && newEmail != existingUser.Email)
            {
                if (await _userRepository.FindEmail(newEmail))
                {
                    return (null, "Електронна пошта вже зайнята.");
                }
                updatedEmail = newEmail;
            }

            string updatedPhone = existingUser.PhoneNumber;
            if (!string.IsNullOrEmpty(newPhone) && newPhone != existingUser.PhoneNumber)
            {
                updatedPhone = newPhone;
            }

            string updatedPassword = currentPassword!;
            if (!string.IsNullOrEmpty(newPass))
            {
                var validationError1 = User.ValidateUserInput(updatedLogin, updatedEmail, newPass, updatedPhone);
                if (!string.IsNullOrEmpty(validationError1))
                {
                    return (null, validationError1);
                }
                updatedPassword = HashPassword(newPass);
            }
            else
            {
                var validationError = User.ValidateUserInput(updatedLogin, updatedEmail, updatedPassword, updatedPhone);
                if (!string.IsNullOrEmpty(validationError))
                {
                    return (null, validationError);
                }
            }

            var updatedUser = new User(existingUser.Id, updatedLogin, updatedEmail, updatedPassword, updatedPhone, existingUser.RoleID ?? 1);

            return (updatedUser, null);
        }

        public async Task<bool> UpdateUser(User user)
        {
            return await _userRepository.Update(user);
        }

    }
}
