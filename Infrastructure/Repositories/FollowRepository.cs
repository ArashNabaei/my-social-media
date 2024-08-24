using Dapper;
using Domain.Entities;
using Domain.Repositories;

namespace Infrastructure.Repositories
{
    public class FollowRepository : IFollowRepository
    {

        private readonly DapperContext _dapperContext;

        public FollowRepository(DapperContext dapperContext)
        {
            _dapperContext = dapperContext;
        }

        public async Task<IEnumerable<User>> GetAllFriends(int userId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("userId", userId);

            var query = "SELECT Id, " +
                "FirstName, " +
                "LastName, " +
                "Bio, " +
                "ImageUrl " +
                "FROM Users " +
                "INNER JOIN Follows " +
                "ON Users.Id = Follows.FollowerId AND FollowingId = @userId " +
                "INNER JOIN Follows " +
                "ON Users.Id = Follows.FollowingId AND FollowerId = @userId";

            var users = await _dapperContext.Connection.QueryAsync<User>(query, parameters);

            return users;
        }

        public async Task<IEnumerable<User>> GetAllFollowers(int userId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("userId", userId);

            var query = "SELECT Id, " +
                "FirstName, " +
                "LastName, " +
                "Bio, " +
                "ImageUrl " +
                "FROM Users " +
                "INNER JOIN Follows " +
                "ON Users.Id = Follows.FollowerId AND FollowingId = @userId ";

            var users = await _dapperContext.Connection.QueryAsync<User>(query, parameters);

            return users;
        }

    }
}
