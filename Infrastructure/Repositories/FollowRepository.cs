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
                "ON Users.Id = Follows.FollowingId AND FollowerId = @userId " +
                "WHERE IsDeleted = 0";

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
                "ON Users.Id = Follows.FollowerId AND FollowingId = @userId " +
                "WHERE IsDeleted = 0";

            var users = await _dapperContext.Connection.QueryAsync<User>(query, parameters);

            return users;
        }

        public async Task<IEnumerable<User>> GetAllFollowings(int userId)
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
                "ON Users.Id = Follows.FollowingId AND FollowerId = @userId " +
                "WHERE IsDeleted = 0";

            var users = await _dapperContext.Connection.QueryAsync<User>(query, parameters);

            return users;
        }

        public async Task<User> GetFollowerById(int userId, int followerId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("userId", userId);
            parameters.Add("followerId", followerId);

            var query = "SELECT Id, " +
                "FirstName, " +
                "LastName, " +
                "Bio, " +
                "ImageUrl " +
                "FROM Users " +
                "INNER JOIN Follows " +
                "ON Users.Id = Follows.FollowerId AND FollowingId = @userId " +
                "WHERE Follows.Id = @followerId AND IsDeleted = 0";

            var users = await _dapperContext.Connection.QueryFirstOrDefaultAsync<User>(query, parameters);

            return users;
        }

        public async Task<User> GetFollowingById(int userId, int followingId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("userId", userId);
            parameters.Add("followingId", followingId);

            var query = "SELECT Id, " +
                "FirstName, " +
                "LastName, " +
                "Bio, " +
                "ImageUrl " +
                "FROM Users " +
                "INNER JOIN Follows " +
                "ON Users.Id = Follows.FollowingId AND FollowerId = @userId " +
                "WHERE Follows.Id = @followingId AND IsDeleted = 0";

            var users = await _dapperContext.Connection.QueryFirstOrDefaultAsync<User>(query, parameters);

            return users;
        }

        public async Task RemoveFollower(int userId, int FollowerId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("userId", userId);
            parameters.Add("FollowerId", FollowerId);

            var query = "UPDATE Follows " +
                "SET IsDeleted = 1 " +
                "WHERE FollowingId = @userId AND @FollowerId = @followerId";

            await _dapperContext.Connection.ExecuteAsync(query, parameters);
        }

    }
}
