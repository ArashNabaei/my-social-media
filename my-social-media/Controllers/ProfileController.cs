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

        [HttpGet("Bio")]
        public async Task<IActionResult> GetBio()
        {
            var bio = await _profileService.GetBio(UserId);
            return Ok(new { Bio = bio });
        }

        [HttpGet("Email")]
        public async Task<IActionResult> GetEmail()
        {
            var email = await _profileService.GetEmail(UserId);
            return Ok(new { Email = email });
        }

        [HttpGet("ImageUrl")]
        public async Task<IActionResult> GetImageUrl()
        {
            var imageUrl = await _profileService.GetImageUrl(UserId);
            return Ok(new { ImageUrl = imageUrl });
        }

        [HttpGet("PhoneNumber")]
        public async Task<IActionResult> GetPhoneNumber()
        {
            var phoneNumber = await _profileService.GetPhoneNumber(UserId);
            return Ok(new { PhoneNumber = phoneNumber });
        }

        [HttpGet("DateOfBirth")]
        public async Task<IActionResult> GetDateOfBirth()
        {
            var dateOfBirth = await _profileService.GetDateOfBirth(UserId);
            return Ok(new { DateOfBirth = dateOfBirth });
        }

        [HttpPut("FirstName")]
        public async Task<IActionResult> UpdateFirstName([FromBody] string firstName)
        {
            await _profileService.UpdateFirstName(UserId, firstName);

            return Ok();
        }

        [HttpPut("LastName")]
        public async Task<IActionResult> UpdateLastName([FromBody] string lastName)
        {
            await _profileService.UpdateLastName(UserId, lastName);

            return Ok();
        }

        [HttpPut("Bio")]
        public async Task<IActionResult> UpdateBio([FromBody] string bio)
        {
            await _profileService.UpdateBio(UserId, bio);

            return Ok();
        }

        [HttpPut("ImageUrl")]
        public async Task<IActionResult> UpdateImageUrl([FromBody] string imageUrl)
        {
            await _profileService.UpdateImageUrl(UserId, imageUrl);

            return Ok();
        }

        [HttpPut("PhoneNumber")]
        public async Task<IActionResult> UpdatePhoneNumber([FromBody] string phoneNumber)
        {
            await _profileService.UpdatePhoneNumber(UserId, phoneNumber);

            return Ok();
        }

        [HttpPut("DateOfBirth")]
        public async Task<IActionResult> UpdateDateOfBirth([FromBody] DateTime dateOfBirth)
        {
            await _profileService.UpdateDateOfBirth(UserId, dateOfBirth);

            return Ok();
        }

    }
}
