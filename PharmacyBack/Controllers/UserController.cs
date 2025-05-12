using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Pharmacy.Application.Services;
using Pharmacy.Core.Abstractions;
using Pharmacy.DataAccess.Repositories;
using PharmacyBack.Contracts;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PharmacyBack.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUsersService _usersService;

        private readonly IJWTService _jwtService;

        public UserController(IUsersService usersService, IJWTService jwtservice) 
        { 
            _usersService = usersService;
            _jwtService = jwtservice;
        }

        [HttpPost("api/register")]
        public async Task<IActionResult> Register([FromBody] UsersRequest request)
        {
            var (user, error) = await _usersService.Register(
                request.Login,
                request.Email,
                request.Password,
                request.Phone
            );

            if (error != null)
                return BadRequest(new { error });

            if (user == null)
                return BadRequest(new { error = error ?? "Unknown error" });

            var response = new UsersResponse(user.Login, user.Email, user.PhoneNumber);
            return Ok(response);
        }

        [Authorize]
        [HttpGet("api/profile")]
        public async Task<IActionResult> GetProfile()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!int.TryParse(userId, out var id))
                return Unauthorized();

            var user = await _usersService.GetProfile(id);
            if (user == null)
                return NotFound();

            return Ok(new UsersResponse(user.Login, user.Email, user.PhoneNumber));

        }

        [HttpPost("api/login")]
        public async Task<IActionResult> Login([FromBody] UsersRequest request)
        {
            var (isValid, user) = await _usersService.ValidateUserCredentials(request.Login, request.Password);

            if (!isValid)
                return Unauthorized("Invalid credentials");

            var token = _jwtService.Generate(user!);

            return Ok(new { Token = token });
        }


        [Authorize]
        [HttpPatch("api/profile")]
        public async Task<IActionResult> UpdatePersonalAccount([FromBody] UpdatePersonalAccountRequest request)
        {
            var userIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!int.TryParse(userIdStr, out var userId))
            {
                return Unauthorized();
            }

            var user = await _usersService.GetProfile(userId); 
            if (user == null)
            {
                return NotFound();
            }

            var (updatedUser, error) = await _usersService.UpdateAccountData(user, request.newLogin, request.newEmail, 
                 request.newPhone, request.NewPass, request.CurrentPassword);

            if (error != null)
            {
                return BadRequest(new { error });
            }

            if (updatedUser == null)
            {
                return BadRequest("Не вдалося оновити дані.");
            }

            var updateResult = await _usersService.UpdateUser(updatedUser);
            if (updateResult)
            {
                return Ok("Профіль оновлено.");
            }
            else
            {
                return StatusCode(500, "Не вдалося оновити профіль.");
            }
        }


    }
}
