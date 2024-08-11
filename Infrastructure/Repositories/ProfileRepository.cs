using Domain.Repositories;

namespace Infrastructure.Repositories
{
    public class ProfileRepository : IProfileRepository
    {
        public Task<string> GetBio(int id)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetDateOfBirth(int id)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetEmail(int id)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetFirstName(int id)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetImageUrl(int id)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetLastName(int id)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetPhoneNumber(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateBio(int id, string bio)
        {
            throw new NotImplementedException();
        }

        public Task UpdateDateOfBirth(int id, DateTime dateOfBirth)
        {
            throw new NotImplementedException();
        }

        public Task UpdateEmail(int id, string email)
        {
            throw new NotImplementedException();
        }

        public Task UpdateFirstName(int id, string firstName)
        {
            throw new NotImplementedException();
        }

        public Task UpdateImageUrl(int id, string imageUrl)
        {
            throw new NotImplementedException();
        }

        public Task UpdateLastName(int id, string lastName)
        {
            throw new NotImplementedException();
        }

        public Task UpdatePhoneNumber(int id, int phoneNumber)
        {
            throw new NotImplementedException();
        }
    }
}
