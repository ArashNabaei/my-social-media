using Application.Features.Query.Profiles;
using Application.Services.Profiles;
using Domain.Entities;
using MediatR;
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

        private readonly ISender _sender;

        public ProfileController(IProfileService profileService, ISender sender)
        {
            _profileService = profileService;
            _sender = sender;
        }

        [HttpGet("Profile")]
        public async Task<IActionResult> GetProfile()
        {
            var profile = await _sender.Send(new GetProfileQuery(UserId));

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
