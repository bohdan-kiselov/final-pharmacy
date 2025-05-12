using Microsoft.AspNetCore.Mvc;
using Pharmacy.Core.Abstractions;
using PharmacyBack.Contracts;

namespace PharmacyBack.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmailTokenController : ControllerBase
    {
        private readonly IEmailVerificationsService _verificationService;

        public EmailTokenController(IEmailVerificationsService verificationsService) 
        {
            _verificationService = verificationsService;
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
    }
}
