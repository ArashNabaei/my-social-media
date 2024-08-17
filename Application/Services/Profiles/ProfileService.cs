

using Domain.Repositories;

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

            return bio;
        }

        public async Task<DateTime> GetDateOfBirth(int id)
        {
            var dateOfBirth = await _profileRepository.GetDateOfBirth(id);

            return dateOfBirth;
        }

        public async Task<string> GetEmail(int id)
        {
            var email = await _profileRepository.GetEmail(id);

            return email;
        }

        public async Task<string> GetFirstName(int id)
        {
            var firstName = await _profileRepository.GetFirstName(id);

            return firstName;
        }

        public async Task<string> GetImageUrl(int id)
        {
            var imageUrl = await _profileRepository.GetImageUrl(id);

            return imageUrl;
        }

        public async Task<string> GetLastName(int id)
        {
            var lastName = await _profileRepository.GetLastName(id);

            return lastName;
        }

        public async Task<string> GetPhoneNumber(int id)
        {
            var phoneNumber = await _profileRepository.GetPhoneNumber(id);

            return phoneNumber;
        }

        public async Task UpdateBio(int id, string bio)
        {

            await _profileRepository.UpdateBio(id, bio);
        }

        public async Task UpdateDateOfBirth(int id, DateTime dateOfBirth)
        {
            await _profileRepository.UpdateDateOfBirth(id, dateOfBirth);
        }

        public async Task UpdateEmail(int id, string email)
        {
            await _profileRepository.UpdateEmail(id, email);
        }

        public async Task UpdateFirstName(int id, string firstName)
        {
            await _profileRepository.UpdateFirstName(id, firstName);
        }

        public async Task UpdateImageUrl(int id, string imageUrl)
        {
            await _profileRepository.UpdateImageUrl(id, imageUrl);
        }

        public async Task UpdatePhoneNumber(int id, string phoneNumber)
        {
            await _profileRepository.UpdatePhoneNumber(id, phoneNumber);
        }
    }
}
