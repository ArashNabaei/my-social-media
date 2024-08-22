using Domain.Entities;
using Domain.Repositories;
using Shared.Exceptions.Profiles;

namespace Application.Services.Profiles
{
    public class ProfileService : IProfileService
    {

        private readonly IProfileRepository _profileRepository;

        public ProfileService(IProfileRepository profileRepository)
        {
            _profileRepository = profileRepository;
        }

        public async Task<User> GetProfile(int id)
        {
            var user = await _profileRepository.GetProfile(id);

            if (user == null)
                throw ProfileException.ProfileNotFound();

            return user;
        }

        public async Task UpdateProfile(int id, User user)
        {
            if (user == null)
                throw ProfileException.InvalidProfileData();

            await _profileRepository.UpdateProfile(id, user);
        }

    }
}
