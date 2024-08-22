using Application.Services.Profiles;
using Domain.Entities;
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

        [HttpGet("Profile")]
        public async Task<IActionResult> GetProfile()
        {
            var profile = await _profileService.GetProfile(UserId);

            return Ok(new { Profile = profile});
        }

        [HttpPut("Profile")]
        public async Task<IActionResult> UpdateProfile([FromBody] User user)
        {
            await _profileService.UpdateProfile(UserId, user);

            return Ok();
        }

    }
}
