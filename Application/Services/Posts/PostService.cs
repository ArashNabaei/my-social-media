using Application.Dtos;
using Domain.Entities;
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

        public async Task<PostDto> GetPostById(int userId, int postId)
        {
            var post = await _postRepository.GetPostById(userId, postId);

            var result = new PostDto
            {
                Id = post.Id,
                Caption = post.Caption,
                ImageUrl = post.ImageUrl,
                CreationTime = post.CreationTime,
            };

            return result;
        }

        public async Task CreatePost(int userId, PostDto post)
        {
            var caption = post.Caption;
            var imageUrl = post.ImageUrl;
            var creationTime = post.CreationTime;

            var result = new Post
            {
                ImageUrl = imageUrl,
                Caption = caption,
                CreationTime = creationTime,
                UserId = userId,
            };

            await _postRepository.CreatePost(userId, result);
        }


        public async Task DeletePost(int userId, int postId)
        {
            await _postRepository.DeletePost(userId, postId);
        }

        public async Task UpdatePost(int userId, int postId, PostDto post)
        {
            var caption = post.Caption;
            var imageUrl = post.ImageUrl;
            var creationTime = post.CreationTime;

            var result = new Post
            {
                ImageUrl = imageUrl,
                Caption = caption,
                CreationTime = creationTime,
                UserId = userId,
            };

            await _postRepository.UpdatePost(userId, postId, result);
        }

    }
}
