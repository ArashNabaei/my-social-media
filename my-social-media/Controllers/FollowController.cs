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

        [HttpGet("GetFollowerById")]
        public async Task<IActionResult> GetFollowerById(int followerId)
        {
            var follower = await _followService.GetFollowerById(UserId, followerId);

            return Ok(follower);
        }

        [HttpGet("GetFollowingById")]
        public async Task<IActionResult> GetFollowingById(int followingId)
        {
            var following = await _followService.GetFollowingById(UserId, followingId);

            return Ok(following);
        }

        [HttpDelete("RemoveFollowerById")]
        public async Task<IActionResult> RemoveFollower(int followerId)
        {
            await _followService.RemoveFollower(UserId, followerId);

            return Ok();
        }

        [HttpDelete("RemoveFollowingById")]
        public async Task<IActionResult> RemoveFollowing(int followinId)
        {
            await _followService.RemoveFollowing(UserId, followinId);

            return Ok();
        }

        [HttpPost("Follow")]
        public async Task<IActionResult> Follow(int id)
        {
            await _followService.Follow(UserId, id);

            return Ok();
        }

    }
}
