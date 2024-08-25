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

        [HttpGet("GetAllFriends")]
        public async Task<IActionResult> GetAllFriends()
        {
            var friends = await _followService.GetAllFriends(UserId);

            return Ok(friends);
        }

        [HttpGet("GetAllFollowers")]
        public async Task<IActionResult> GetAllFollowers()
        {
            var followers = await _followService.GetAllFollowers(UserId);

            return Ok(followers);
        }

        [HttpGet("GetAllFollowings")]
        public async Task<IActionResult> GetAllFollowings()
        {
            var followings = await _followService.GetAllFollowings(UserId);

            return Ok(followings);
        }

    }
}
