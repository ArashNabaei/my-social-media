using Application.Dtos;

namespace Application.Services.Follows
{
    public interface IFollowService
    {
        Task<IEnumerable<UserDto>> GetAllFriends(int userId);

        Task<IEnumerable<UserDto>> GetAllFollowers(int userId);

        Task<IEnumerable<UserDto>> GetAllFollowings(int userId);

        Task<UserDto> GetFollowerById(int userId, int followerId);

        Task<UserDto> GetFollowingById(int userId, int followingId);

        Task RemoveFollower(int userId, int FollowerId);

        Task RemoveFollowing(int userId, int FollowingId);

        Task Follow(int userId, int id);

    }
}
