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


        public async Task<IEnumerable<User>> GetAllUsers()
        {
            var query = "SELECT * FROM Users";

            var users = await _dapperContext.Connection.QueryAsync<User>(query);

            return users;
        }
    }
}
