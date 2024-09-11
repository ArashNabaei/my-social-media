﻿using Application.Dtos;
using Application.Services.Follows;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.Extensions.Logging;
using Moq;
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
