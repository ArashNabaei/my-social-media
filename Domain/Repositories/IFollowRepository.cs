using Domain.Entities;

namespace Domain.Repositories
{
    public interface IFollowRepository
    {
        Task<IEnumerable<User>?> GetAllFriends(int userId);

        Task<IEnumerable<User>?> GetAllFollowers(int userId);
        
        Task<IEnumerable<User>?> GetAllFollowings(int userId);

        Task<User?> GetFollowerById(int userId, int followerId);

        Task<User?> GetFollowingById(int userId, int followingId);

        Task RemoveFollower(int userId, int FollowerId);

        Task RemoveFollowing(int userId, int FollowingId);

        Task Follow(int userId, int id);
    }
}
