﻿using Application.Dtos;
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

            if (posts == null || !posts.Any())
            {
                _logger.LogError($"User with id {userId} tried to access their posts, but no posts were found.");
                
                throw PostException.NoPostsFound();
            }

            _logger.LogInformation($"User with id {userId} saw all his posts.");

            var result = posts.Select(post => new PostDto
            {
                Id = post.Id,
                Caption = post.Caption,
                ImageUrl = post.ImageUrl,
                CreatedAt = post.CreatedAt,
            });

            return result;
        }

        public async Task<PostDto> GetPostById(int userId, int postId)
        {
            var post = await _postRepository.GetPostById(userId, postId);

            if (post == null)
            {
                _logger.LogError($"User with id {userId} tried to access a non-existent post with id {postId}.");
                
                throw PostException.PostNotFound();
            }

            _logger.LogInformation($"User with id {userId} saw his post with id {postId}.");

            var result = new PostDto
            {
                Id = post.Id,
                Caption = post.Caption,
                ImageUrl = post.ImageUrl,
                CreatedAt = post.CreatedAt,
            };

            return result;
        }

        public async Task CreatePost(int userId, PostDto post)
        {
            var caption = post.Caption;
            var imageUrl = post.ImageUrl;
            var createdAt = DateTime.UtcNow;

            var result = new Post
            {
                ImageUrl = imageUrl,
                Caption = caption,
                CreatedAt = createdAt,
                UserId = userId,
            };

            await _postRepository.CreatePost(userId, result);

            _logger.LogInformation($"User with id {userId} created new post.");
        }

        public async Task DeletePost(int userId, int postId)
        {
            var post = await GetPostById(userId, postId);

            if (post == null)
            {
                _logger.LogError($"User with id {userId} tried to delete a non-existent post with id {postId}.");

                throw PostException.PostNotFound();
            }

            await _postRepository.DeletePost(userId, postId);

            _logger.LogInformation($"User with id {userId} deleted his post with id {postId}.");
        }

        public async Task UpdatePost(int userId, int postId, PostDto post)
        {
            var foundedPost = await GetPostById(userId, postId);

            if (foundedPost == null)
            {
                _logger.LogError($"User with id {userId} tried to update a non-existent post with id {postId}.");

                throw PostException.PostNotFound();
            }

            var caption = post.Caption;
            var imageUrl = post.ImageUrl;
            var createdAt = post.CreatedAt;

            var result = new Post
            {
                ImageUrl = imageUrl,
                Caption = caption,
                CreatedAt = createdAt,
                UserId = userId,
            };

            await _postRepository.UpdatePost(userId, postId, result);

            _logger.LogInformation($"User with id {userId} updated his post with id {postId}.");
        }

        public async Task LikePost(int userId, int postId)
        {
            var post = await _postRepository.GetOthersPostById(userId, postId);

            if (post == null)
            {
                _logger.LogError($"User with id {userId} tried to like a non-existent post with id {postId}.");

                throw PostException.PostNotFound();
            }

            await _postRepository.LikePost(userId, postId);

            _logger.LogInformation($"User with id {userId} liked post with id {postId}.");
        }

        public async Task<IEnumerable<Like>> GetLikesOfPost(int userId, int postId)
        {
            var likes = await _postRepository.GetLikesOfPost(userId, postId);

            if (likes == null || !likes.Any())
            {
                _logger.LogError($"User with id {userId} tried to access likes of post with id {postId}, but no likes were found.");
                
                throw PostException.NoLikesFound();
            }

            _logger.LogInformation($"User with id {userId} saw likes of post with id {postId}.");

            return likes;
        }

        public async Task<IEnumerable<Post>> GetFriendsPosts(int userId, int friendId)
        {
            var posts = await _postRepository.GetFriendsPosts(userId, friendId);

            if (posts == null || !posts.Any())
            {
                _logger.LogError($"User with id {userId} tried to access posts of friend with id {friendId}, but no posts were found.");
                
                throw PostException.NoFriendsPostsFound();
            }

            _logger.LogInformation($"User with id {userId} saw his friend's posts with id {friendId}.");

            return posts;
        }

        public async Task LeaveCommentOnPost(int userId, int postId, string comment)
        {
            var post = await _postRepository.GetOthersPostById(userId, postId);

            if (post == null)
            {
                _logger.LogError($"User with id {userId} tried to leave comment on a non-existent post with id {postId}.");

                throw PostException.PostNotFound();
            }

            await _postRepository.LeaveCommentOnPost(userId, postId, comment);

            _logger.LogInformation($"User with id {userId} left a comment on post with id {postId}.");
        }

        public async Task<IEnumerable<Comment>> GetCommentsOfPost(int userId, int postId)
        {
            var comments = await _postRepository.GetCommentsOfPost(userId, postId);

            if (comments == null || !comments.Any())
            {
                _logger.LogError($"User with id {userId} tried to access comments of post with id {postId}, but no comments were found.");
                
                throw PostException.NoCommentsFound();
            }

            _logger.LogInformation($"User with id {userId} saw all comments of post with id {postId}.");

            return comments;
        }

        public async Task ReportPost(int userId, int postId, string message)
        {
            var post = await _postRepository.GetPostById(postId);

            if (post == null)
            {
                _logger.LogError($"User with id {userId} tried to access a non-existent post.");

                throw PostException.PostNotFound();
            }

            _logger.LogInformation($"User with id {userId}" +
                $" reported post with id {postId}" +
                $" with this message {message}");

            await _postRepository.ReportPost(userId, postId, message);
        }

    }
}
