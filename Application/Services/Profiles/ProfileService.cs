using Domain.Entities;
using Domain.Repositories;
using Microsoft.Extensions.Logging;
using Shared.Exceptions.Profiles;

namespace Application.Services.Profiles
{
    public class ProfileService : IProfileService
    {

        private readonly IProfileRepository _profileRepository;

        private readonly ILogger<ProfileService> _logger;

        public ProfileService(IProfileRepository profileRepository, ILogger<ProfileService> logger)
        {
            _profileRepository = profileRepository;
            _logger = logger;
        }

        public async Task<User> GetProfile(int id)
        {
            var user = await _profileRepository.GetProfile(id);

            if (user == null)
                throw ProfileException.ProfileNotFound();

            _logger.LogInformation($"User with id {id} saw his profile.");

            return user;
        }

        public async Task UpdateProfile(int id, User user)
        {
            if (user == null)
                throw ProfileException.InvalidProfileData();

            await _profileRepository.UpdateProfile(id, user);

            _logger.LogInformation($"User with id {id} updated his profile.");
        }

    }
}
