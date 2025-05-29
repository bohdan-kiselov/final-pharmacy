using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;
using Pharmacy.Core.Abstractions;
using Pharmacy.Core.Models;
using Pharmacy.DataAccess.Repositories;

namespace Pharmacy.Application.Services
{
    public class EmailVerificationsService : IEmailVerificationsService
    {
        private readonly IEmailTokensRepository _tokenRepository;
        private readonly IConfiguration _config;

        public EmailVerificationsService(IEmailTokensRepository repository, IConfiguration config)
        {
            _tokenRepository = repository ?? throw new ArgumentNullException(nameof(repository));
            _config = config;
        }

        public async Task SendVerificationToken(int userId, string email)
        {
            var token = Guid.NewGuid();

            await _tokenRepository.Create(userId, token);

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(_config["Email:FromName"], _config["Email:FromAddress"]));
            message.To.Add(MailboxAddress.Parse(email));
            message.Subject = "Підтвердження пошти";

            var baseUrl = _config["Email:VerificationUrl"];
            var verificationLink = $"{baseUrl}/api/EmailToken/verify?token={token}";

            message.Body = new TextPart("html")
            {
                Text = $"<p>Для підтвердження пошти натисніть на посилання нижче:</p>" +
               $"<p><a href='{verificationLink}'>Підтвердити email</a></p>" +
               $"<p>Або скопіюйте це посилання у браузер: <br>{verificationLink}</p>"
            };

            using var smtp = new SmtpClient();
            try
            {
                var portString = _config["Email:Smtp:Port"];
                if (!int.TryParse(portString, out int port))
                    throw new InvalidOperationException("SMTP port is not configured correctly.");

                await smtp.ConnectAsync(_config["Email:Smtp:Host"], port, SecureSocketOptions.SslOnConnect);
                await smtp.AuthenticateAsync(_config["Email:Smtp:Username"], _config["Email:Smtp:Password"]);
                await smtp.SendAsync(message);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Помилка при відправці email", ex);
            }
            finally
            {
                await smtp.DisconnectAsync(true);
            }
        }

        public async Task<bool> VerifyToken(Guid token)
        {
            return await _tokenRepository.ConfirmEmail(token);
        }

        public async Task<bool> SwitchTokenIsUsed(String token)
        {
            return await _tokenRepository.SwitchIsUsed(token);
        }

        public async Task SendResetToken(int userId, string email)
        {
            var token = Guid.NewGuid();

            await _tokenRepository.Create(userId, token);

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(_config["Email:FromName"], _config["Email:FromAddress"]));
            message.To.Add(MailboxAddress.Parse(email));
            message.Subject = "Скидання паролю";

            var baseUrl = _config["Email:VerificationUrl"];
            var verificationLink = $"{baseUrl}/api/EmailToken/verify?token={token}"; // <- Змінити на посилання на фронт

            message.Body = new TextPart("html")
            {
                Text = $"<p>Для встановлення нового паролю натисніть на посилання нижче:</p>" +
               $"<p><a href='{verificationLink}'>Встановити новий пароль</a></p>" +
               $"<p>Або скопіюйте це посилання у браузер: <br>{verificationLink}</p>"
            };

            using var smtp = new SmtpClient();
            try
            {
                var portString = _config["Email:Smtp:Port"];
                if (!int.TryParse(portString, out int port))
                    throw new InvalidOperationException("SMTP port is not configured correctly.");

                await smtp.ConnectAsync(_config["Email:Smtp:Host"], port, SecureSocketOptions.SslOnConnect);
                await smtp.AuthenticateAsync(_config["Email:Smtp:Username"], _config["Email:Smtp:Password"]);
                await smtp.SendAsync(message);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Помилка при відправці email", ex);
            }
            finally
            {
                await smtp.DisconnectAsync(true);
            }
        }

    }
}
