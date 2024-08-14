using Application.Dtos;
using Domain.Entities;

namespace Application.Services.Accounts
{
    public interface IAccountService
    {

        Task<IEnumerable<User>> GetAllUsers();

        Task CreateUser(UserDto user);
    }
}
