using Application.Services.Profiles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace my_social_media.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
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
            var userId = GetUserIdFromClaims();

            var firstName = await _profileService.GetFirstName(userId);

            return Ok(firstName);
        }

        [HttpGet("LastName")]
        public async Task<IActionResult> GetLastName()
        {
            var userId = GetUserIdFromClaims();

            var lastName = await _profileService.GetLastName(userId);

            return Ok(lastName);
        }

    }
}
