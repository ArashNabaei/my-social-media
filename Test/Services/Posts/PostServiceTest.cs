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

        [Fact]
        public async Task CreatePost_ShouldBeCalledOnce()
        {
            int userId = 1;

            var post = PostMocks.ValidPost();

            var postDto = ConvertPostToPostDto(post);

            await _postService.CreatePost(userId, postDto);

            _postRepository.Verify(r => r.CreatePost(userId,
                It.Is<Post>(p =>
                    p.Caption == postDto.Caption &&
                    p.ImageUrl == postDto.ImageUrl &&
                    p.UserId == userId &&
                    p.CreatedAt != default)), Times.Once);
        }

        [Fact]
        public async Task DeletePost_WhenPostExists_ShouldDeletePost()
        {
            int userId = 1;

            var post = PostMocks.ValidPost();

            _postRepository.Setup(r => r.GetPostById(userId, post.Id))
                .ReturnsAsync(post);

            await _postService.DeletePost(userId, post.Id);

            _postRepository.Verify(r => r.DeletePost(userId, post.Id), Times.Once);
        }

        [Fact]
        public async Task UpdatePost_WhenPostExists_ShouldUpdatePost()
        {
            int userId = 1;

            var post = PostMocks.ValidPost();

            _postRepository.Setup(r => r.GetPostById(userId, post.Id))
                .ReturnsAsync(post);

            var postDto = PostMocks.UpdatedPost(); ;

            await _postService.UpdatePost(userId, post.Id, postDto);

            _postRepository.Verify(r => r.UpdatePost(userId, post.Id, 
                It.Is<Post>(p =>
                    p.Caption == postDto.Caption &&
                    p.ImageUrl == postDto.ImageUrl &&
                    p.CreatedAt == postDto.CreatedAt &&
                    p.UserId == userId)), Times.Once);
        }

        [Fact]
        public async Task LikePost_WhenPostExists_ShouldLikesPost()
        {
            int userId = 1;

            var post = PostMocks.ValidPost();

            _postRepository.Setup(r => r.GetOthersPostById(userId, post.Id))
                .ReturnsAsync(post);

            await _postService.LikePost(userId, post.Id);

            _postRepository.Verify(r => r.LikePost(userId, post.Id), Times.Once);
        }

        [Fact]
        public async Task GetLikesOfPost_WhenPostsExist_ShouldReturnsLikesOfPost()
        {
            int userId = 1;
            int postId = 1;

            var likes = new List<Like>();
            likes.Add(PostMocks.ValidLike());

            _postRepository.Setup(r => r.GetLikesOfPost(userId, postId))
                .ReturnsAsync(likes);

            var result = await _postService.GetLikesOfPost(userId, postId);

            Assert.Equal(likes, result);
        }

        [Fact]
        public async Task GetLikesOfPost_WhenPostDoesNotExist_ShouldThrowsPostNotFoundException()
        {
            int userId = 1;
            int postId = 1;

            _postRepository.Setup(r => r.GetLikesOfPost(userId, postId))
                .ReturnsAsync((IEnumerable<Like>?)null);

            var exception = await Assert.ThrowsAsync<PostException>(() => _postService.GetLikesOfPost(userId, postId));

            Assert.Equal(3005, exception.Code);
        }

        [Fact]
        public async Task GetFriendsPosts_WhenPostsExist_ShouldReturnsPosts()
        {
            int userId = 1;
            int friendId = 2;

            var posts = new List<Post>();
            posts.Add(PostMocks.ValidPost());

            _postRepository.Setup(r => r.GetFriendsPosts(userId, friendId))
                .ReturnsAsync(posts);

            var result = await _postService.GetFriendsPosts(userId, friendId);

            Assert.NotNull(result);
            Assert.Equal(posts.Count(), result.Count());
            Assert.Equal(posts.First().Id, result.First().Id);
            Assert.Equal(posts.First().ImageUrl, result.First().ImageUrl);
            Assert.Equal(posts.First().Caption, result.First().Caption);
            Assert.Equal(posts.First().CreatedAt, result.First().CreatedAt);
        }

        [Fact]
        public async Task GetFriendsPosts_WhenNoPostExists_ShouldThrowsNoPostsFoundException()
        {
            int userId = 1;
            int friendId = 2;

            _postRepository.Setup(r => r.GetFriendsPosts(userId, friendId))
                .ReturnsAsync((IEnumerable<Post>?)null);

            var exception = await Assert.ThrowsAsync<PostException>(() => _postService.GetFriendsPosts(userId, friendId));

            Assert.Equal(3004, exception.Code);
        }

        [Fact]
        public async Task LeaveCommentOnPost_ShouldBeCalledOnce()
        {
            int userId = 1;

            var post = PostMocks.ValidPost();

            _postRepository.Setup(r => r.GetOthersPostById(userId, post.Id))
                .ReturnsAsync(post);

            await _postService.LeaveCommentOnPost(userId, post.Id, "comment");

            _postRepository.Verify(r => r.LeaveCommentOnPost(userId, post.Id, "comment"), Times.Once);
        }

        [Fact]
        public async Task GetCommentsOfPost_WhenCommentsExist_ShouldReturnsComments()
        {
            int userId = 1;
            int postId = 1;

            var comments = new List<Comment> ();
            comments.Add(PostMocks.ValidComment());

            _postRepository.Setup(r => r.GetCommentsOfPost(userId, postId))
                .ReturnsAsync(comments);

            var result = await _postService.GetCommentsOfPost(userId, postId);

            Assert.Equal(comments, result);
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
