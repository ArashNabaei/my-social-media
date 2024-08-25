using Application.Services.Follows;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace my_social_media.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class FollowController : BaseController
    {
        private readonly IFollowService _followService;

        public FollowController(IFollowService followService)
        {
            _followService = followService;
        }

        [HttpGet("GetAllfriends")]
        public async Task<IActionResult> GetAllfriends()
        {
            var friends = await _followService.GetAllFriends(UserId);

            return Ok(friends);
        }

    }
}
