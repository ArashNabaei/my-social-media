﻿using Application.Dtos;
using Domain.Repositories;

namespace Application.Services.Follows
{
    public class FollowService : IFollowService
    {
        private readonly IFollowRepository _followRepository;

        public FollowService(IFollowRepository followRepository)
        {
            _followRepository = followRepository;
        }

        public async Task<IEnumerable<UserDto>> GetAllFriends(int userId) 
        {
            var friends = await _followRepository.GetAllFriends(userId);

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
            await _followRepository.RemoveFollower(userId, followerId);
        }

        public async Task RemoveFollowing(int userId, int followingId)
        {
            await _followRepository.RemoveFollowing(userId, followingId);
        }

        public async Task Follow(int userId, int id)
        {
            await _followRepository.Follow(userId, id);
        }

    }
}
