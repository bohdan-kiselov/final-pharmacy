using MailKit.Net.Smtp;
using MimeKit;
using Pharmacy.Core.Abstractions;
using Pharmacy.Core.Models;
using Pharmacy.DataAccess.Repositories;

namespace Pharmacy.Application.Services
{
    public class EmailVerificationsService : IEmailVerificationsService
    {
        private readonly IEmailTokensRepository _tokenRepository;

        public EmailVerificationsService(IEmailTokensRepository repository)
        {
            _tokenRepository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task SendVerificationToken(int id, string email)
        {
            var token = Guid.NewGuid();

            await _tokenRepository.Create(id, token);

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Pharmacy", "noreply@pharmacy.com"));
            message.To.Add(MailboxAddress.Parse(email));
            message.Subject = "Підтвердження пошти";


            var verificationLink = $"http://localhost:7035/EmailToken/verify?token={token}"; // Test

            message.Body = new TextPart("html")
            {
                Text = $"<p>Для підтвердження пошти натисніть на посилання нижче:</p>" +
               $"<p><a href='{verificationLink}'>Підтвердити email</a></p>" +
               $"<p>Або скопіюйте це посилання у браузер: <br>{verificationLink}</p>"
            };

            using var smtp = new SmtpClient();
            await smtp.ConnectAsync("sandbox.smtp.mailtrap.io", 587, MailKit.Security.SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync("f3488b0f5b0572", "f26caac327b15f");
            await smtp.SendAsync(message);
            await smtp.DisconnectAsync(true);
        }

        public async Task<bool> VerifyToken(Guid token)
        {
            return await _tokenRepository.ConfirmEmail(token);
        }


    }
}
