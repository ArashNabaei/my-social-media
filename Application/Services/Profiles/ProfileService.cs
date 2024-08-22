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

        public async Task<string> GetBio(int id)
        {
            var bio = await _profileRepository.GetBio(id);

            if (bio == null)
                throw ProfileException.ProfileNotFound();

            return bio;
        }

        public async Task<DateTime> GetDateOfBirth(int id)
        {
            var dateOfBirth = await _profileRepository.GetDateOfBirth(id);

            if (dateOfBirth == DateTime.MinValue)
                throw ProfileException.ProfileNotFound();

            return dateOfBirth;
        }

        public async Task<string> GetEmail(int id)
        {
            var email = await _profileRepository.GetEmail(id);

            if (email == null)
                throw ProfileException.ProfileNotFound();

            return email;
        }

        public async Task<string> GetFirstName(int id)
        {
            var firstName = await _profileRepository.GetFirstName(id);

            if (firstName == null)
                throw ProfileException.ProfileNotFound();

            return firstName;
        }

        public async Task<string> GetImageUrl(int id)
        {
            var imageUrl = await _profileRepository.GetImageUrl(id);

            if (imageUrl == null)
                throw ProfileException.ProfileNotFound();

            return imageUrl;
        }

        public async Task<string> GetLastName(int id)
        {
            var lastName = await _profileRepository.GetLastName(id);

            if (lastName == null)
                throw ProfileException.ProfileNotFound();

            return lastName;
        }

        public async Task<string> GetPhoneNumber(int id)
        {
            var phoneNumber = await _profileRepository.GetPhoneNumber(id);

            if (phoneNumber == null)
                throw ProfileException.ProfileNotFound();

            return phoneNumber;
        }

        public async Task UpdateBio(int id, string bio)
        {
            if (string.IsNullOrWhiteSpace(bio))
                throw ProfileException.InvalidProfileData();

            await _profileRepository.UpdateBio(id, bio);
        }

        public async Task UpdateDateOfBirth(int id, DateTime dateOfBirth)
        {
            if (dateOfBirth == DateTime.MinValue)
                throw ProfileException.InvalidProfileData();

            await _profileRepository.UpdateDateOfBirth(id, dateOfBirth);
        }

        public async Task UpdateEmail(int id, string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw ProfileException.InvalidProfileData();

            await _profileRepository.UpdateEmail(id, email);
        }

        public async Task UpdateFirstName(int id, string firstName)
        {
            if (string.IsNullOrWhiteSpace(firstName))
                throw ProfileException.InvalidProfileData();

            await _profileRepository.UpdateFirstName(id, firstName);
        }

        public async Task UpdateImageUrl(int id, string imageUrl)
        {
            if (string.IsNullOrWhiteSpace(imageUrl))
                throw ProfileException.InvalidProfileData();

            await _profileRepository.UpdateImageUrl(id, imageUrl);
        }

        public async Task UpdateLastName(int id, string lastName)
        {
            if (string.IsNullOrWhiteSpace(lastName))
                throw ProfileException.InvalidProfileData();

            await _profileRepository.UpdateLastName(id, lastName);
        }

        public async Task UpdatePhoneNumber(int id, string phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber))
                throw ProfileException.InvalidProfileData();

            await _profileRepository.UpdatePhoneNumber(id, phoneNumber);
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
