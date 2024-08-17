using Application.Dtos;

namespace Application.Services.Accounts
{
    public interface IAccountService
    {

        Task<IEnumerable<UserDto>> GetAllUsers();

        Task CreateUser(UserDto userDto);

        string GenerateToken(string username);

        Task<int?> ValidateUser(string username, string password);
    }
}
