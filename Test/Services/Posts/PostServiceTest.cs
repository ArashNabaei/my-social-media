﻿using Application.Dtos;
using Application.Services.Posts;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.Extensions.Logging;
using Moq;
using Shared.Exceptions.Posts;
using Test.Mocks;
using Xunit;

namespace Test.Services.Posts
{
    public class PostServiceTest
    {

        private readonly Mock<IPostRepository> _postRepository;

        private readonly Mock<ILogger<PostService>> _logger;

        private readonly PostService _postService;

        public PostServiceTest()
        {
            _postRepository = new Mock<IPostRepository>();

            _logger = new Mock<ILogger<PostService>>();

            _postService = new PostService(
                _postRepository.Object, 
                _logger.Object
                );
        }

        [Fact]
        public async Task GetAllPosts_WhenPostsExist_ShouldReturnPosts()
        {
            int userId = 1;

            var posts = new List<Post>();
            posts.Add(PostMocks.ValidPost());

            _postRepository.Setup(r => r.GetAllPosts(userId))
                .ReturnsAsync(posts);

            var result = await _postService.GetAllPosts(userId);

            var expectedPosts = ConvertPostListToPostDtoList(posts);

            Assert.NotNull(result);
            Assert.Equal(expectedPosts.Count(), result.Count());
            Assert.Equal(expectedPosts.First().Id, result.First().Id);
            Assert.Equal(expectedPosts.First().ImageUrl, result.First().ImageUrl);
            Assert.Equal(expectedPosts.First().Caption, result.First().Caption);
            Assert.Equal(expectedPosts.First().CreatedAt, result.First().CreatedAt);
        }

        [Fact]
        public async Task GetAllPosts_WhenNoPostsExist_ShouldThrowsNoPostsFoundException()
        {
            int userId = 1;

            _postRepository.Setup(r => r.GetAllPosts(userId))
                .ReturnsAsync((IEnumerable<Post>?) null);

            var exception = await Assert.ThrowsAsync<PostException>(() => _postService.GetAllPosts(userId));

            Assert.Equal(3006, exception.Code);
        }

        [Fact]
        public async Task GetPostById_WhenPostsExists_ShouldReturnsPost()
        {
            int userId = 1;

            var post = PostMocks.ValidPost();

            _postRepository.Setup(r => r.GetPostById(userId, post.Id))
                .ReturnsAsync(post);

            var result = await _postService.GetPostById(userId, post.Id);

            PostDto expectedPost = ConvertPostToPostDto(post);

            Assert.NotNull(result);
            Assert.Equal(expectedPost.Id, result.Id);
            Assert.Equal(expectedPost.ImageUrl, result.ImageUrl);
            Assert.Equal(expectedPost.Caption, result.Caption);
            Assert.Equal(expectedPost.CreatedAt, result.CreatedAt);
        }

        [Fact]
        public async Task GetPostById_WhenPostDoesNotExit_ShouldThrowsPostNotFoundException()
        {
            int userId = 1;
            int postId = 2;

            _postRepository.Setup(r => r.GetPostById(userId, postId))
                .ReturnsAsync((Post?)null);

            var exception = await Assert.ThrowsAsync<PostException>(() => _postService.GetPostById(userId, postId));

            Assert.Equal(3001, exception.Code);
        }

        private static PostDto ConvertPostToPostDto(Post post)
        {
            return new PostDto
            {
                Id = post.Id,
                ImageUrl = post.ImageUrl,
                Caption = post.Caption,
                CreatedAt = post.CreatedAt,
            };
        }

        private static IEnumerable<PostDto> ConvertPostListToPostDtoList(List<Post> posts)
        {
            return posts.Select(p => new PostDto
            {
                Id = p.Id,
                ImageUrl = p.ImageUrl,
                Caption = p.Caption,
                CreatedAt = p.CreatedAt,
            });
        }

    }
}
