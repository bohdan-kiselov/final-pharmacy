using Microsoft.AspNetCore.Mvc;
using Pharmacy.Application.Services;
using Pharmacy.Core.Abstractions;
using PharmacyBack.Contracts;

namespace PharmacyBack.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmailTokenController : ControllerBase
    {
        private readonly IEmailVerificationsService _verificationService;
        private readonly IUsersService _usersService;

        public EmailTokenController(IEmailVerificationsService verificationsService, IUsersService usersService) 
        {
            _verificationService = verificationsService;
            _usersService = usersService;
        }

        [HttpGet("verify")]
        public async Task<IActionResult> VerifyEmail([FromQuery] Guid token)
        {
            var success = await _verificationService.VerifyToken(token);
            if (!success)
            {
                return BadRequest("Неправильний або прострочений токен.");
            }

            return Ok("Пошту успішно підтверджено!");
        }

        [HttpPost("send-reset")]
        public async Task<IActionResult> SendResetToken([FromBody] EmailRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Email))
                return BadRequest("Email не може бути порожнім.");

            var user = await _usersService.GetUserByEmail(request.Email);
            if (user == null)
                return NotFound("Користувача з такою поштою не знайдено.");

            await _verificationService.SendResetToken(user.Id, user.Email);

            return Ok("Посилання для скидання паролю надіслано на пошту.");
        }
    }
}
