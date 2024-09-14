using Application.Services.Accounts;
using Domain.Repositories;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;
using Domain.Entities;
using Shared.Exceptions.Accounts;
using Application.Dtos;
using Test.Mocks;

namespace Test.Services.Accounts
{
    public class AccountServiceTests
    {

        private readonly Mock<IAccountRepository> _accountRepository;

        private readonly Mock<ILogger<AccountService>> _logger;

        private readonly IAccountService _accountService;

        public AccountServiceTests()
        {
            _accountRepository = new Mock<IAccountRepository>();

            _logger = new Mock<ILogger<AccountService>>();

            _accountService = new AccountService(
                _accountRepository.Object,
                null,
                _logger.Object);
        }

        [Fact]
        public async Task ValidateUser_WithInvalidCredentials_ShouldThrowsUserNotFoundException()
        {
            var user = AccountMocks.InvalidUser();

            _accountRepository.Setup(r => r.GetUserByUsernameAndPassword(user.Username, user.Password))
                              .ReturnsAsync((User?)null);

            var exception = await Assert.ThrowsAsync<AccountException>(() => _accountService.ValidateUser(user.Username, user.Password));

            Assert.Equal(1002, exception.Code);
        }

        [Fact]
        public async Task ValidateUser_WithValidCredentials_ShouldReturnsUserId()
        {
            var user = AccountMocks.ValidUser();

            _accountRepository.Setup(r => r.GetUserByUsernameAndPassword(user.Username, user.Password))
                              .ReturnsAsync(user);

            var result = await _accountService.ValidateUser(user.Username, user.Password);

            Assert.Equal(user.Id, result);
        }

        [Fact]
        public async Task CreateUser_WithExistingUser_ShouldThrowsUserAlreadyExistsException()
        {
            var userDto = new AccountDto { Username = "ExistingUsername", Password = "ExistingPassword" };

            var user = AccountMocks.ExistingUser();

            _accountRepository.Setup(r => r.GetUserByUsernameAndPassword(userDto.Username, userDto.Password))
                              .ReturnsAsync(user);

            var exception = await Assert.ThrowsAsync<AccountException>(() => _accountService.CreateUser(userDto));

            Assert.Equal(1001, exception.Code);

            _accountRepository.Verify(r => r.CreateUser(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        }

    }
}
