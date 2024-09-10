using Application.Services.Accounts;
using Microsoft.Extensions.Configuration;
using Domain.Repositories;
using Microsoft.Extensions.Logging;
using Moq;

namespace Test.Services.Accounts
{
    public class AccountServiceTests
    {

        private readonly Mock<IAccountRepository> _accountRepository;

        private readonly Mock<IConfiguration> _configuration;

        private readonly Mock<ILogger<AccountService>> _logger;

        private readonly IAccountService _accountService;

        public AccountServiceTests()
        {
            _accountRepository = new Mock<IAccountRepository>();
            _configuration = new Mock<IConfiguration>();
            _logger = new Mock<ILogger<AccountService>>();

            _configuration.Setup(config => config.GetValue<string>("Jwt:Key")).Returns("here_is_my_secret_key_that_is_used_for_Jwt");
            _configuration.Setup(config => config.GetValue<double>("Jwt:ExpiryMinutes")).Returns(30);

            _accountService = new AccountService(
                _accountRepository.Object,
                _configuration.Object,
                _logger.Object);
        }



    }
}
