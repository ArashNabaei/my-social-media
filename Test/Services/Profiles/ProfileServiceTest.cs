using Application.Services.Profiles;
using Domain.Repositories;
using Microsoft.Extensions.Logging;
using Moq;

namespace Test.Services.Profiles
{
    public class ProfileServiceTest
    {

        private readonly Mock<IProfileRepository> _profileRepository;

        private readonly Mock<ILogger<ProfileService>> _logger;

        private readonly IProfileService _profileService;

        public ProfileServiceTest()
        {
            _profileRepository = new Mock<IProfileRepository>();

            _logger = new Mock<ILogger<ProfileService>>();

            _profileService = new ProfileService(
                _profileRepository.Object,
                _logger.Object
                );
        }

    }
}
