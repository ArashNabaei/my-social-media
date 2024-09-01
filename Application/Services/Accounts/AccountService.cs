using Application.Dtos;
using Domain.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Shared.Exceptions.Accounts;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Application.Services.Accounts
{
    public class AccountService : IAccountService
    {

        private readonly IAccountRepository _accountRepository;

        private readonly IConfiguration _configuration;

        private readonly ILogger<AccountService> _logger;

        public AccountService(IAccountRepository accountRepository, IConfiguration configuration, ILogger<AccountService> logger)
        {
            _accountRepository = accountRepository;
            _configuration = configuration;
            _logger = logger;
        }

        public async Task CreateUser(AccountDto user)
        {
            var username = user.Username;
            var password = user.Password;

            var existingUser = await _accountRepository.GetUserByUsernameAndPassword(username, password);

            if (existingUser != null)
                throw AccountException.UserAlreadyExists();

            await _accountRepository.CreateUser(username, password);

            _logger.LogInformation("New user signed up.");
        }

        public string GenerateToken(int userId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration.GetValue<string>("Jwt:Key"));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                new Claim(ClaimTypes.NameIdentifier, userId.ToString())
                }),
                NotBefore = DateTime.UtcNow,
                Expires = DateTime.UtcNow.AddMinutes(_configuration.GetValue<double>("Jwt:ExpiryMinutes")),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public async Task<int> ValidateUser(string username, string password)
        {
            var user = await _accountRepository.GetUserByUsernameAndPassword(username, password);
            
            if (user == null)
                throw AccountException.UserNotFound();

            _logger.LogInformation($"User with Id {user.Id} signed in.");

            return user.Id;
        }

    }
}
