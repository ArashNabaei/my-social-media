using Application.Dtos;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.Extensions.Logging;
using Shared.Exceptions.Follows;

namespace Application.Services.Follows
{
    public class FollowService : IFollowService
    {
        private readonly IFollowRepository _followRepository;

        private readonly ILogger<FollowService> _logger;

        public FollowService(IFollowRepository followRepository, ILogger<FollowService> logger)
        {
            _followRepository = followRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<UserDto>> GetAllFriends(int userId) 
        {
            var friends = await _followRepository.GetAllFriends(userId);

            _logger.LogInformation($"User with id {userId} saw all his friends.");

            var result = friends.Select(friend => new UserDto
            {
                Id = friend.Id,
                FirstName = friend.FirstName,
                LastName = friend.LastName,
                Bio = friend.Bio,
                ImageUrl = friend.ImageUrl,
            });

            return result;
        }

        public async Task<IEnumerable<UserDto>> GetAllFollowers(int userId)
        {
            var followers = await _followRepository.GetAllFollowers(userId);

            _logger.LogInformation($"User with id {userId} saw all his followers.");

            var result = followers.Select(follower => new UserDto
            {
                Id = follower.Id,
                FirstName = follower.FirstName,
                LastName = follower.LastName,
                Bio = follower.Bio,
                ImageUrl = follower.ImageUrl,
            });

            return result;
        }

        public async Task<IEnumerable<UserDto>> GetAllFollowings(int userId)
        {
            var followings = await _followRepository.GetAllFollowings(userId);

            _logger.LogInformation($"User with id {userId} saw all his followings.");

            var result = followings.Select(following => new UserDto
            {
                Id = following.Id,
                FirstName = following.FirstName,
                LastName = following.LastName,
                Bio = following.Bio,
                ImageUrl = following.ImageUrl,
            });

            return result;
        }

        public async Task<UserDto> GetFollowerById(int userId, int followerId)
        {
            var follower = await _followRepository.GetFollowerById(userId, followerId);

            if (follower == null)
            {
                _logger.LogError($"User with id {userId} tried to access a non-existent follower with id {followerId}.");

                throw FollowException.FollowerNotFound();
            }

            _logger.LogInformation($"User with id {userId} saw his follower with id {followerId}.");

            var result = new UserDto
            {
                Id = follower.Id,
                FirstName = follower.FirstName,
                LastName = follower.LastName,
                Bio = follower.Bio,
                ImageUrl = follower.ImageUrl,
            };

            return result;
        }

        public async Task<UserDto> GetFollowingById(int userId, int followingId)
        {
            var following = await _followRepository.GetFollowingById(userId, followingId);

            if (following == null)
            {
                _logger.LogError($"User with id {userId} tried to access a non-existent following with id {followingId}.");

                throw FollowException.FollowingNotFound();
            }

            _logger.LogInformation($"User with id {userId} saw his following with id {followingId}.");

            var result = new UserDto
            {
                Id = following.Id,
                FirstName = following.FirstName,
                LastName = following.LastName,
                Bio = following.Bio,
                ImageUrl = following.ImageUrl,
            };

            return result;
        }

        public async Task RemoveFollower(int userId, int followerId)
        {
            var follower = await _followRepository.GetFollowerById(userId, followerId);

            if (follower == null)
            {
                _logger.LogError($"User with id {userId} tried to remove a non-existent follower with id {followerId}.");

                throw FollowException.FollowerNotFound();
            }

            await _followRepository.RemoveFollower(userId, followerId);

            _logger.LogInformation($"User with id {userId} removed his follower with id {followerId}.");
        }

        public async Task RemoveFollowing(int userId, int followingId)
        {
            var following = await _followRepository.GetFollowingById(userId, followingId);

            if (following == null)
            {
                _logger.LogError($"User with id {userId} tried to remove a non-existent following with id {followingId}.");

                throw FollowException.FollowingNotFound();
            }

            await _followRepository.RemoveFollowing(userId, followingId);

            _logger.LogInformation($"User with id {userId} removed his following with id {followingId}.");
        }

        public async Task Follow(int userId, int id)
        {
            await _followRepository.Follow(userId, id);

            _logger.LogInformation($"User with id {userId} followed user with id {id}.");
        }

    }
}
