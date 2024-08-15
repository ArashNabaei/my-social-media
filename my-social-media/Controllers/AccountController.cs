using Application.Dtos;
using Application.Services.Accounts;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace my_social_media.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("signup")]
        public async Task<IActionResult> SignUp(UserDto user)
        {
            if (users.Any(u => u.Username == user.Username))
                return BadRequest("User already exists.");

            await _accountService.CreateUser(user);
            
            return Ok(user);
        }

        [HttpPost("signin")]
        public async Task<IActionResult> SignIn(UserDto userDto)
        {
            var user = users.SingleOrDefault(u => u.Username == userDto.Username && u.Password == userDto.Password);
            
            if (user == null)
                return Unauthorized();

            var generatedToken = await _accountService.GenerateToken(user.Username);

            return Ok(new { token = generatedToken });
        }

    }
}
