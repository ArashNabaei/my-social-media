
using Application.Dtos;
using Domain.Repositories;

namespace Application.Services.Posts
{
    public class PostService : IPostService
    {

        private readonly IPostRepository _postRepository;

        public PostService(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public async Task<IEnumerable<PostDto>> GetAllPosts(int userId)
        {
            var posts = await _postRepository.GetAllPosts(userId);

            var result = posts.Select(post => new PostDto
            {
                Caption = post.Caption,
                ImageUrl = post.ImageUrl,
                CreationTime = post.CreationTime,
            });

            return result;
        }

    }
}
