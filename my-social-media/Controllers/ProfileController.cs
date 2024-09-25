using Application.Features.Command.Profiles;
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

        private readonly ISender _sender;

        public ProfileController(ISender sender)
        {
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
            await _sender.Send(new UpdateProfileCommand(UserId, user));

            return Ok();
        }

    }
}
