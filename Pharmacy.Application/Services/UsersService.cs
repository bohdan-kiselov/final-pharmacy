using Pharmacy.Core.Models;
using Pharmacy.DataAccess.Repositories;
using BCrypt.Net;
using Pharmacy.Core.Abstractions;
using System.Numerics;

namespace Pharmacy.Application.Services
{
    public class UsersService : IUsersService
    {

        private readonly IUsersRepository _userRepository;
        private readonly IEmailVerificationsService _emailVerificationService;
        private readonly int _workFactor = 12;

        public UsersService(IUsersRepository repository, IEmailVerificationsService emailVerificationService)
        {
            _userRepository = repository ?? throw new ArgumentNullException(nameof(repository));
            _emailVerificationService = emailVerificationService ?? throw new ArgumentNullException(nameof(emailVerificationService));
 
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

            if (await _userRepository.FindPhone(phone))
                return (null, "Номер телефону вже зайнятий.");

            var validationError = User.ValidateUserInput(login, email, password, phone);
            if (!string.IsNullOrEmpty(validationError))
                return (null, validationError);

            var hashedPassword = HashPassword(password);

            var user = new User (login, email, hashedPassword, phone );
            var createdUser = await _userRepository.Create(user);

            await _emailVerificationService.SendVerificationToken(createdUser.Id, createdUser.Email);

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
    string? newPhone, string? newPass)
        {
            string? validationError1;

            string updatedLogin = existingUser.Login;
            if (!string.IsNullOrWhiteSpace(newLogin))
            {
                if (await _userRepository.FindLogin(newLogin))
                {
                    return (null, "Логін вже зайнятий.");
                }
                updatedLogin = newLogin;
            }

            string updatedEmail = existingUser.Email;
            if (!string.IsNullOrWhiteSpace(newEmail))
            {
                if (await _userRepository.FindEmail(newEmail))
                {
                    return (null, "Електронна пошта вже зайнята.");
                }
                await _userRepository.SwitchIsVerified(existingUser.Id);

                await _emailVerificationService.SendVerificationToken(existingUser.Id, newEmail);
                updatedEmail = newEmail;
            }

            string updatedPhone = existingUser.PhoneNumber;
            if (!string.IsNullOrWhiteSpace(newPhone))
            {
                if (await _userRepository.FindPhone(newPhone))
                {
                    return (null, "Номер телефону вже зайнятий.");
                }
                updatedPhone = newPhone;
            }

            string updatedPass = existingUser.Password;
            if (!string.IsNullOrWhiteSpace(newPass))
            {
                validationError1 = User.ValidateUserInput(updatedLogin, updatedEmail, newPass, updatedPhone);
                if (!string.IsNullOrEmpty(validationError1))
                {
                    return (null, validationError1);
                }
                updatedPass = HashPassword(newPass);
            }
            else 
            {
                validationError1 = User.ValidateProfileData(updatedLogin, updatedEmail, updatedPhone);
                if (!string.IsNullOrEmpty(validationError1))
                {
                    return (null, validationError1);
                }
            }

            var updatedUser = new User (existingUser.Id, updatedLogin, updatedEmail, 
                updatedPass, updatedPhone );

            return (updatedUser, null);
        }

        public async Task<bool> UpdateUser(User user)
        {
            return await _userRepository.Update(user);
        }

    }
}
