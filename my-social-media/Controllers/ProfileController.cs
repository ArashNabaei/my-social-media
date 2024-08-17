using Application.Services.Profiles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace my_social_media.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ProfileController : BaseController
    {
        private readonly IProfileService _profileService;

        public ProfileController(IProfileService profileService)
        {
            _profileService = profileService;
        }

        [HttpGet("FirstName")]
        public async Task<IActionResult> GetFirstName()
        {
            var firstName = await _profileService.GetFirstName(UserId);
            return Ok(new { FirstName = firstName });
        }

        [HttpGet("LastName")]
        public async Task<IActionResult> GetLastName()
        {
            var lastName = await _profileService.GetLastName(UserId);
            return Ok(new { LastName = lastName });
        }

    }
}
