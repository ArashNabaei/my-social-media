using Application.Dtos;
using Domain.Entities;
using Domain.Repositories;

namespace Application.Services.Accounts
{
    public class AccountService : IAccountService
    {

        private readonly IAccountRepository _accountRepository;

        public AccountService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task CreateUser(UserDto userDto)
        {
            var username = userDto.Username;
            var password = userDto.Password;

            await _accountRepository.CreateUser(username, password);
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            var users = await _accountRepository.GetAllUsers();

            var result = users.Select(user => new User
            {
                Id = user.Id,
                Username = user.Username,
                Password = user.Password
            });

            return result;
        }

    }
}
