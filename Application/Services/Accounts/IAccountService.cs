using Application.Dtos;

namespace Application.Services.Accounts
{
    public interface IAccountService
    {
        Task CreateUser(UserDto userDto);

        string GenerateToken(int userId);

        Task<int?> ValidateUser(string username, string password);
    }
}
