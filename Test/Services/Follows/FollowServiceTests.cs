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
