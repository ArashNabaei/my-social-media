using Application.Services.Accounts;
using Domain.Repositories;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;
using Domain.Entities;
using Shared.Exceptions.Accounts;
using Application.Dtos;

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
            var username = "invaliduser";
            var password = "invalidpassword";

            _accountRepository.Setup(r => r.GetUserByUsernameAndPassword(username, password))
                              .ReturnsAsync((User?)null);

            var exception = await Assert.ThrowsAsync<AccountException>(() => _accountService.ValidateUser(username, password));

            Assert.Equal(1002, exception.Code);
        }

        [Fact]
        public async Task ValidateUser_WithValidCredentials_ShouldReturnsUserId()
        {
            var username = "validuser";
            var password = "validpassword";
            var user = new User { Id = 1, Username = username, Password = password };

            _accountRepository.Setup(r => r.GetUserByUsernameAndPassword(username, password))
                              .ReturnsAsync(user);

            var result = await _accountService.ValidateUser(username, password);

            Assert.Equal(user.Id, result);
        }

        [Fact]
        public async Task CreateUser_WithExistingUser_ShouldThrowsUserAlreadyExistsException()
        {
            var userDto = new AccountDto { Username = "existinguser", Password = "password123" };
            
            var existingUser = new User { Id = 1, Username = "existinguser", Password = "password123" };

            _accountRepository.Setup(r => r.GetUserByUsernameAndPassword(userDto.Username, userDto.Password))
                              .ReturnsAsync(existingUser);

            var exception = await Assert.ThrowsAsync<AccountException>(() => _accountService.CreateUser(userDto));

            Assert.Equal(1001, exception.Code);

            _accountRepository.Verify(r => r.CreateUser(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        }

    }
}
