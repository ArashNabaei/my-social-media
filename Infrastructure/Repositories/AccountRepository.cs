using Dapper;
using Domain.Entities;
using Domain.Repositories;

namespace Infrastructure.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly DapperContext _dapperContext;

        public AccountRepository(DapperContext dapperContext)
        {
            _dapperContext = dapperContext;
        }

        public async Task CreateUser(string username, string password)
        {
            var parameters = new DynamicParameters();
            parameters.Add("username", username);
            parameters.Add("password", password);

            var query = "INSERT INTO Users (Username, Password) VALUES (@username, @password)";

            await _dapperContext.Connection.ExecuteAsync(query, parameters);
        }

        public async Task<User?> GetUserByUsernameAndPassword(string username, string password)
        {
            var parameters = new DynamicParameters();
            parameters.Add("username", username);
            parameters.Add("password", password);

            var query = "SELECT * FROM Users WHERE Username = @username AND Password = @password";

            var user = await _dapperContext.Connection.QueryFirstOrDefaultAsync<User>(query, parameters);

            return user;
        }
    }
}
