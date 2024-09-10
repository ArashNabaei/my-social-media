using Application.Services.Accounts;
using Microsoft.Extensions.Configuration;
using Domain.Repositories;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;
using Application.Dtos;
using Domain.Entities;
using Shared.Exceptions.Accounts;

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

    }
}
