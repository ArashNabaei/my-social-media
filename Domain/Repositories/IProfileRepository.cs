using Domain.Entities;

namespace Domain.Repositories
{
    public interface IProfileRepository
    {
        Task<User> GetProfile(int id);

        Task UpdateProfile(int id, User user);
    }
}
