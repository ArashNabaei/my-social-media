using Application.Dtos;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.Extensions.Logging;
using Shared.Exceptions.Posts;

namespace Application.Services.Posts
{
    public class PostService : IPostService
    {

        private readonly IPostRepository _postRepository;

        private readonly ILogger<PostService> _logger;

        public PostService(IPostRepository postRepository, ILogger<PostService> logger)
        {
            _postRepository = postRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<PostDto>> GetAllPosts(int userId)
        {
            var posts = await _postRepository.GetAllPosts(userId);

            _logger.LogInformation($"User with id {userId} saw all his posts.");

            var result = posts.Select(post => new PostDto
            {
                Id = post.Id,
                Caption = post.Caption,
                ImageUrl = post.ImageUrl,
                CreationTime = post.CreationTime,
            });

            return result;
        }

        public async Task<PostDto> GetPostById(int userId, int postId)
        {
            var post = await _postRepository.GetPostById(userId, postId);

            _logger.LogInformation($"User with id {userId} saw his post with id {postId}.");

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
            var creationTime = DateTime.UtcNow;

            var result = new Post
            {
                ImageUrl = imageUrl,
                Caption = caption,
                CreationTime = creationTime,
                UserId = userId,
            };

            await _postRepository.CreatePost(userId, result);

            _logger.LogInformation($"User with id {userId} created new post.");
        }


        public async Task DeletePost(int userId, int postId)
        {
            var post = await GetPostById(userId, postId);

            if (post == null)
                throw PostException.PostNotFound();

            await _postRepository.DeletePost(userId, postId);

            _logger.LogInformation($"User with id {userId} deleted his post with id {postId}.");
        }

        public async Task UpdatePost(int userId, int postId, PostDto post)
        {
            var foundedPost = await GetPostById(userId, postId);

            if (foundedPost == null)
                throw PostException.PostNotFound();

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

            _logger.LogInformation($"User with id {userId} updated his post with id {postId}.");
        }

        public async Task LikePost(int userId, int postId)
        {
            var post = await _postRepository.GetOthersPostById(userId, postId);

            if (post == null)
                throw PostException.PostNotFound();

            await _postRepository.LikePost(userId, postId);

            _logger.LogInformation($"User with id {userId} likes post with id {postId}.");
        }

        public async Task<IEnumerable<Like>> GetLikesOfPost(int userId, int postId)
        {
            var likes = await _postRepository.GetLikesOfPost(userId, postId);

            _logger.LogInformation($"User with id {userId} saw likes of post with id {postId}.");

            return likes;
        }

        public async Task<IEnumerable<Post>> GetFriendsPosts(int userId, int friendId)
        {
            var posts = await _postRepository.GetFriendsPosts(userId, friendId);

            return posts;
        }

        public async Task LeaveCommentOnPost(int userId, int postId, string comment)
        {
            var post = await _postRepository.GetOthersPostById(userId, postId);

            if (post == null)
                throw PostException.PostNotFound();

            await _postRepository.LeaveCommentOnPost(userId, postId, comment);
        }

        public async Task<IEnumerable<Comment>> GetCommentsOfPost(int userId, int postId)
        {
            var comments = await _postRepository.GetCommentsOfPost(userId, postId);

            return comments;
        }

    }
}
