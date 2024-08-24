using Domain.Entities;

namespace Domain.Repositories
{
    public interface IFollowRepository
    {
        Task<IEnumerable<User>> GetAllFriends(int id);

        Task<IEnumerable<User>> GetAllFollowers();
        
        Task<IEnumerable<User>> GetAllFollowings();

        Task<User> GetFollowerById(int id);

        Task<User> GetFollowingById(int id);

        Task RemoveFollower(int id);

        Task RemoveFollowing(int id);

        Task Follow(int id);
    }
}
