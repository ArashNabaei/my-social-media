using Application.Dtos;

namespace Application.Services.Accounts
{
    public interface IAccountService
    {

        Task<IEnumerable<UserDto>> GetAllUsers();

        Task CreateUser(UserDto userDto);

        string GenerateToken(string username);

        Task<bool> ValidateUser(string username, string password);
    }
}
