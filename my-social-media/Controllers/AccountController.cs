using Application.Dtos;
using Application.Services.Accounts;
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
            await _accountService.CreateUser(user);

            return Ok("User registered successfully.");
        }

        [HttpPost("signin")]
        public async Task<IActionResult> SignIn(UserDto userDto)
        {
            var userId = await _accountService.ValidateUser(userDto.Username, userDto.Password);

            var token = _accountService.GenerateToken(userId);

            return Ok(new { token });
        }

    }
}
