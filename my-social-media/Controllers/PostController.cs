using Application.Dtos;
using Application.Services.Posts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace my_social_media.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]

    public class PostController : BaseController
    {
        private readonly IPostService _postService;

        public PostController(IPostService postService)
        {
            _postService = postService;
        }

        [HttpGet("GetAllPosts")]
        public async Task<IActionResult> GetAllPosts()
        {
            var posts = await _postService.GetAllPosts(UserId);

            return Ok(posts);
        }

        [HttpGet("GetPostById")]
        public async Task<IActionResult> GetPostById(int postId)
        {
            var post = await _postService.GetPostById(UserId, postId);

            return Ok(post);
        }

        [HttpPost("CreatePost")]
        public async Task<IActionResult> CreatePost([FromBody] PostDto postDto)
        {
            await _postService.CreatePost(UserId, postDto);

            return Ok("Post registered successfully.");
        }

        [HttpDelete("DeletePost")]
        public async Task<IActionResult> DeletePost(int postId)
        {
            await _postService.DeletePost(UserId, postId);

            return Ok("Post Deleted successfully.");
        }

        [HttpPut("UpdatePost")]
        public async Task<IActionResult> UpdatePost(int postId, [FromBody] PostDto postDto)
        {
            await _postService.UpdatePost(UserId,postId, postDto);

            return Ok("Post updated successfully.");
        }

        [HttpPost("LikePost")]
        public async Task<IActionResult> LikePost(int postId)
        {
            await _postService.LikePost(UserId, postId);

            return Ok("Post liked successfully.");
        }

        [HttpGet("GetLikesOfPost")]
        public async Task<IActionResult> GetLikesOfPost(int postId)
        {
            var likes = await _postService.GetLikesOfPost(UserId, postId);

            return Ok(likes);
        }

        [HttpGet("GetFriendsPosts")]
        public async Task<IActionResult> GetFriendsPosts(int friendId)
        {
            var posts = await _postService.GetFriendsPosts(UserId, friendId);

            return Ok(posts);
        }

        [HttpPost("LeaveCommentOnPost")]
        public async Task<IActionResult> LeaveCommentOnPost(int postId, [FromBody] string comment)
        {
            await _postService.LeaveCommentOnPost(UserId, postId, comment);

            return Ok("Comment added to post successfully.");
        }

        [HttpGet("GetCommentsOfPost")]
        public async Task<IActionResult> GetCommentsOfPost(int postId)
        {
            var comments = await _postService.GetCommentsOfPost(UserId, postId);

            return Ok(comments);
        }

    }
}
