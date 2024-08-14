
namespace Application.Services.Profiles
{
    public interface IProfileService
    {

        Task<string> GetBio(int id);

        Task<DateTime> GetDateOfBirth(int id);

        Task<string> GetEmail(int id);

        Task<string> GetFirstName(int id);

        Task<string> GetLastName(int id);

        Task<string> GetImageUrl(int id);

        Task<string> GetPhoneNumber(int id);

        Task UpdateBio(int id, string bio);

        Task UpdateDateOfBirth(int id, DateTime dateOfBirth);

        Task UpdateEmail(int id, string email);

        Task UpdateFirstName(int id, string firstName);

        Task UpdateImageUrl(int id, string imageUrl);

        Task UpdatePhoneNumber(int id, string phoneNumber);

    }
}
