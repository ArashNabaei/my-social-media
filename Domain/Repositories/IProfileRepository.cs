
namespace Domain.Repositories
{
    public interface IProfileRepository
    {
        Task<string> GetFirstName(int id);

        Task<string> GetLastName(int id);

        Task<string> GetBio(int id);

        Task<string> GetEmail(int id);

        Task<string> GetPhoneNumber(int id);

        Task<string> GetImageUrl(int id);

        Task<string> GetDateOfBirth(int id);

        Task UpdateFirstName(int id, string firstName);

        Task UpdateLastName(int id, string lastName);

        Task UpdateBio(int id, string bio);

        Task UpdateEmail(int id, string email);

        Task UpdatePhoneNumber(int id, int phoneNumber);

        Task UpdateImageUrl(int id, string imageUrl);

        Task UpdateDateOfBirth(int id, DateTime dateOfBirth);

    }
}
