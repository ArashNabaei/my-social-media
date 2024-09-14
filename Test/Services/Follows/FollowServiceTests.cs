using Application.Dtos;
using Application.Services.Follows;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.Extensions.Logging;
using Moq;
using Shared.Exceptions.Follows;
using Test.Mocks;
using Xunit;

namespace Test.Services.Follows
{
    public class FollowServiceTests
    {

        private readonly Mock<IFollowRepository> _followRepository;

        private readonly Mock<ILogger<FollowService>> _logger;

        private readonly IFollowService _followService;

        public FollowServiceTests()
        {
            _followRepository = new Mock<IFollowRepository>();

            _logger = new Mock<ILogger<FollowService>>();

            _followService = new FollowService(
                _followRepository.Object,
                _logger.Object
                );
        }

        [Fact]
        public async Task GetAllFriends_WhenFriendsExist_ShouldReturnsFriends()
        {
            int userId = 2;

            var friends = new List<User>();
            friends.Add(FollowMocks.ValidUser());

            _followRepository.Setup(r => r.GetAllFriends(userId))
                .ReturnsAsync(friends);

            var expectedFriends = ConvertUserToUserDto(friends);

            var result = await _followService.GetAllFriends(userId);

            Assert.NotNull(result);
            Assert.Equal(expectedFriends.Count(), result.Count());
            Assert.Equal(expectedFriends.First().Id, result.First().Id);
            Assert.Equal(expectedFriends.First().FirstName, result.First().FirstName);
            Assert.Equal(expectedFriends.First().LastName, result.First().LastName);
        }

        [Fact]
        public async Task GetAllFollowers_WhenFollowersExist_ShouldReturnsFollowers()
        {
            int userId = 2;

            var followers = new List<User>();
            followers.Add(FollowMocks.ValidUser());

            _followRepository.Setup(r => r.GetAllFollowers(userId))
                .ReturnsAsync(followers);

            var expectedFollowers = ConvertUserToUserDto(followers);

            var result = await _followService.GetAllFollowers(userId);

            Assert.NotNull(result);
            Assert.Equal(expectedFollowers.Count(), result.Count());
            Assert.Equal(expectedFollowers.First().Id, result.First().Id);
            Assert.Equal(expectedFollowers.First().FirstName, result.First().FirstName);
            Assert.Equal(expectedFollowers.First().LastName, result.First().LastName);
        }

        [Fact]
        public async Task GetAllFollowings_WhenFollowingsExist_ShouldReturnsFollowings()
        {
            int userId = 2;

            var followings = new List<User>();
            followings.Add(FollowMocks.ValidUser());

            _followRepository.Setup(r => r.GetAllFollowings(userId))
                .ReturnsAsync(followings);

            var expectedFollowings = ConvertUserToUserDto(followings);

            var result = await _followService.GetAllFollowings(userId);

            Assert.NotNull(result);
            Assert.Equal(expectedFollowings.Count(), result.Count());
            Assert.Equal(expectedFollowings.First().Id, result.First().Id);
            Assert.Equal(expectedFollowings.First().FirstName, result.First().FirstName);
            Assert.Equal(expectedFollowings.First().LastName, result.First().LastName);
        }

        [Fact]
        public async Task GetAllFriends_WhenNoFriendsExist_ShouldReturnsNoFriendsFoundException()
        {
            int userId = 1;

            _followRepository.Setup(r => r.GetAllFriends(userId))
                .ReturnsAsync((IEnumerable<User>?)null);

            var exception = await Assert.ThrowsAsync<FollowException>(() => _followService.GetAllFriends(userId));

            Assert.Equal(5003, exception.Code);
        }

        [Fact]
        public async Task GetAllFollowers_WhenNoFollowersExist_ShouldReturnsNoFollowersFoundException()
        {
            int userId = 1;

            _followRepository.Setup(r => r.GetAllFollowers(userId))
                .ReturnsAsync((IEnumerable<User>?)null);

            var exception = await Assert.ThrowsAsync<FollowException>(() => _followService.GetAllFollowers(userId));

            Assert.Equal(5004, exception.Code);
        }

        [Fact]
        public async Task GetAllFollowings_WhenNoFollowingsExist_ShouldReturnsNoFollowingsFoundException()
        {
            int userId = 1;

            _followRepository.Setup(r => r.GetAllFollowings(userId))
                .ReturnsAsync((IEnumerable<User>?)null);

            var exception = await Assert.ThrowsAsync<FollowException>(() => _followService.GetAllFollowings(userId));

            Assert.Equal(5005, exception.Code);
        }

        [Fact]
        public async Task GetFollowerById_WhenFollowerExists_ShouldReturnsFollower()
        {
            int userId = 1;

            var follower = FollowMocks.ValidUser();

            _followRepository.Setup(r => r.GetFollowerById(userId, follower.Id))
                .ReturnsAsync(follower);

            var result = await _followService.GetFollowerById(userId, follower.Id);

            Assert.Equal(follower.Id, result.Id);
            Assert.Equal(follower.FirstName, result.FirstName);
            Assert.Equal(follower.LastName, result.LastName);
            Assert.Equal(follower.ImageUrl, result.ImageUrl);
            Assert.Equal(follower.Bio, result.Bio);
        }

        [Fact]
        public async Task GetFollowingById_WhenFollowingExists_ShouldReturnsFollowing()
        {
            int userId = 1;

            var following = FollowMocks.ValidUser();

            _followRepository.Setup(r => r.GetFollowingById(userId, following.Id))
                .ReturnsAsync(following);

            var result = await _followService.GetFollowingById(userId,following.Id);

            Assert.Equal(following.Id, result.Id);
            Assert.Equal(following.FirstName, result.FirstName);
            Assert.Equal(following.LastName, result.LastName);
            Assert.Equal(following.ImageUrl, result.ImageUrl);
            Assert.Equal(following.Bio, result.Bio);
        }

        [Fact]
        public async Task GetFollowerById_WhenFollowerDoesNotExist_ShouldReturnsFollowerNotFoundException()
        {
            int userId = 1, followerId = 2;

            _followRepository.Setup(r => r.GetFollowerById(userId, followerId))
                .ReturnsAsync((User?)null);

            var exception = await Assert.ThrowsAsync<FollowException>(() => _followService.GetFollowerById(userId, followerId));

            Assert.Equal(5001, exception.Code);
        }

        [Fact]
        public async Task GetFollowingById_WhenFollowingDoesNotExist_ShouldReturnsFollowingNotFoundException()
        {
            int userId = 1;
            int followingId = 2;

            _followRepository.Setup(r => r.GetFollowingById(userId, followingId))
                .ReturnsAsync((User?)null);

            var exception = await Assert.ThrowsAsync<FollowException>(() => _followService.GetFollowingById(userId, followingId));

            Assert.Equal(5002, exception.Code);
        }

        [Fact]
        public async Task RemoveFollower_WhenFollowerExists_ShouldRemoveFollower()
        {
            int userId = 1;

            var follower = FollowMocks.ValidUser();

            _followRepository.Setup(r => r.GetFollowerById(userId, follower.Id))
                .ReturnsAsync(follower);

            await _followService.RemoveFollower(userId, follower.Id);

            _followRepository.Verify(r => r.RemoveFollower(userId, follower.Id), Times.Once);
        }

        [Fact]
        public async Task RemoveFollowing_WhenFollowingExists_ShouldRemoveFollowing()
        {
            int userId = 1;

            var following = FollowMocks.ValidUser();

            _followRepository.Setup(r => r.GetFollowingById(userId, following.Id))
                .ReturnsAsync(following);

            await _followService.RemoveFollowing(userId, following.Id);

            _followRepository.Verify(r => r.RemoveFollowing(userId, following.Id), Times.Once);
        }

        [Fact]
        public async Task Follow_ShouldFollowUser()
        {
            int userId = 1;

            var user = FollowMocks.ValidUser();

            await _followService.Follow(userId, user.Id);

            _followRepository.Verify(r => r.Follow(userId, user.Id), Times.Once);
        }

        private static IEnumerable<UserDto> ConvertUserToUserDto(List<User> friends)
        {
            return friends.Select(f => new UserDto
            {
                Id = f.Id,
                FirstName = f.FirstName,
                LastName = f.LastName,
                Bio = f.Bio,
                ImageUrl = f.ImageUrl,
            });
        }
    }
}
