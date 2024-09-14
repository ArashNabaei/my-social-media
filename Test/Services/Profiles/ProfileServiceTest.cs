using Application.Services.Profiles;
using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Repositories;
using Microsoft.Extensions.Logging;
using Moq;
using Shared.Exceptions.Profiles;
using Test.Mocks;
using Xunit;

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

        [Fact]
        public async Task GetProfile_WhenProfileExists_ShouldReturnsProfile()
        {
            int userId = 1;

            var profile = ProfileMocks.ValidUser();

            _profileRepository.Setup(r => r.GetProfile(userId))
                .ReturnsAsync(profile);

            var result = await _profileService.GetProfile(userId);

            Assert.NotNull(result);
            Assert.Equal(profile.Id, result.Id);
            Assert.Equal(profile.ImageUrl, result.ImageUrl);
            Assert.Equal(profile.Username, result.Username);
            Assert.Equal(profile.Password, result.Password);
            Assert.Equal(profile.Bio, result.Bio);
            Assert.Equal(profile.PhoneNumber, result.PhoneNumber);
            Assert.Equal(profile.DateOfBirth, result.DateOfBirth);
            Assert.Equal(profile.FirstName, result.FirstName);
            Assert.Equal(profile.LastName, result.LastName);
            Assert.Equal(profile.Email, result.Email);
        }

        [Fact]
        public async Task GetProfile_WhenProfileDoesNotExist_ShouldThrowsProfileNotFoundException()
        {
            int userId = 1;

            _profileRepository.Setup(r => r.GetProfile(userId))
                .ReturnsAsync((User?)null);

            var exception = await Assert.ThrowsAsync<ProfileException>(() => _profileService.GetProfile(userId));

            Assert.Equal(2001, exception.Code);
        }

        [Fact]
        public async Task UpdateProfile_whenProfileExists_ShouldUpdateProfile()
        {
            int userId = 1;

            var profile = ProfileMocks.ValidUser();

            _profileRepository.Setup(r => r.GetProfile(userId))
                .ReturnsAsync(profile);

            var updatedProfile = ProfileMocks.UpdatedUser();

            await _profileService.UpdateProfile(userId, updatedProfile);

            _profileRepository.Verify(r => r.UpdateProfile(userId,
                It.Is<User>(p =>
                    p.Id == updatedProfile.Id &&
                    p.ImageUrl == updatedProfile.ImageUrl &&
                    p.FirstName == updatedProfile.FirstName &&
                    p.LastName == updatedProfile.LastName &&
                    p.Username == updatedProfile.Username &&
                    p.Password == updatedProfile.Password &&
                    p.Bio == updatedProfile.Bio &&
                    p.PhoneNumber == updatedProfile.PhoneNumber &&
                    p.Email == updatedProfile.Email &&
                    p.DateOfBirth == updatedProfile.DateOfBirth
                        )), Times.Once);
        }

    }
}
