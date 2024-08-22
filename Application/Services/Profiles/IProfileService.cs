using Domain.Entities;

namespace Application.Services.Profiles
{
    public interface IProfileService
    {
        Task<User> GetProfile(int id);

        Task UpdateProfile(int id, User user);
    }
}
