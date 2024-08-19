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

        [HttpPut("UpdatePost")]
        public async Task<IActionResult> UpdatePost(int postId, [FromBody] PostDto postDto)
        {
            await _postService.UpdatePost(UserId,postId, postDto);

            return Ok("Post updated successfully.");
        }

    }
}
