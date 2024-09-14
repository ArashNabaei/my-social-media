using Application.Dtos;
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

            var expectedPosts = ConvertPostToPostDto(posts);

            Assert.NotNull(result);
            Assert.Equal(expectedPosts.Count(), result.Count());
            Assert.Equal(expectedPosts.First().Id, result.First().Id);
            Assert.Equal(expectedPosts.First().ImageUrl, result.First().ImageUrl);
            Assert.Equal(expectedPosts.First().Caption, result.First().Caption);
            Assert.Equal(expectedPosts.First().CreatedAt, result.First().CreatedAt);
        }

        [Fact]
        public async Task GetAllPosts_WhenNoPostsExist_ShouldReturnNoPostsFoundException()
        {
            int userId = 1;

            _postRepository.Setup(r => r.GetAllPosts(userId))
                .ReturnsAsync((IEnumerable<Post>?) null);

            var exception = await Assert.ThrowsAsync<PostException>(() => _postService.GetAllPosts(userId));

            Assert.Equal(3006, exception.Code);
        }

        private static IEnumerable<PostDto> ConvertPostToPostDto(List<Post> posts)
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
