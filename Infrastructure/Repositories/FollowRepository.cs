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

            var query = 
                "SELECT " +
                "u.Id, " +
                "u.FirstName, " +
                "u.LastName, " +
                "u.Bio, " +
                "u.ImageUrl " +
                "FROM Users u " +
                "INNER JOIN Follows f1 " +
                "ON u.Id = f1.FollowerId AND f1.FollowingId = @userId " +
                "INNER JOIN Follows f2 " +
                "ON u.Id = f2.FollowingId AND f2.FollowerId = @userId " +
                "WHERE f1.IsDeleted = 0 AND f2.IsDeleted = 0";

            var users = await _dapperContext.Connection.QueryAsync<User>(query, parameters);

            return users;
        }

        public async Task<IEnumerable<User>> GetAllFollowers(int userId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("userId", userId);

            var query = "SELECT u.Id, " +
                "u.FirstName, " +
                "u.LastName, " +
                "u.Bio, " +
                "u.ImageUrl " +
                "FROM Users u " +
                "INNER JOIN Follows f " +
                "ON u.Id = f.FollowerId AND f.FollowingId = @userId " +
                "WHERE f.IsDeleted = 0";

            var users = await _dapperContext.Connection.QueryAsync<User>(query, parameters);

            return users;
        }

        public async Task<IEnumerable<User>> GetAllFollowings(int userId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("userId", userId);

            var query = "SELECT u.Id, " +
                "u.FirstName, " +
                "u.LastName, " +
                "u.Bio, " +
                "u.ImageUrl " +
                "FROM Users u " +
                "INNER JOIN Follows f " +
                "ON u.Id = f.FollowingId AND f.FollowerId = @userId " +
                "WHERE f.IsDeleted = 0";

            var users = await _dapperContext.Connection.QueryAsync<User>(query, parameters);

            return users;
        }

        public async Task<User> GetFollowerById(int userId, int followerId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("userId", userId);
            parameters.Add("followerId", followerId);

            var query = "SELECT u.Id, " +
                "u.FirstName, " +
                "u.LastName, " +
                "u.Bio, " +
                "u.ImageUrl " +
                "FROM Users u " +
                "INNER JOIN Follows f " +
                "ON u.Id = f.FollowerId AND f.FollowingId = @userId " +
                "WHERE f.FollowerId = @followerId AND f.IsDeleted = 0";

            var users = await _dapperContext.Connection.QueryFirstOrDefaultAsync<User>(query, parameters);

            return users;
        }

        public async Task<User> GetFollowingById(int userId, int followingId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("userId", userId);
            parameters.Add("followingId", followingId);

            var query = "SELECT u.Id, " +
                "u.FirstName, " +
                "u.LastName, " +
                "u.Bio, " +
                "u.ImageUrl " +
                "FROM Users u " +
                "INNER JOIN Follows f " +
                "ON u.Id = f.FollowingId AND f.FollowerId = @userId " +
                "WHERE f.FollowingId = @followingId AND f.IsDeleted = 0";

            var users = await _dapperContext.Connection.QueryFirstOrDefaultAsync<User>(query, parameters);

            return users;
        }

        public async Task RemoveFollower(int userId, int followerId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("userId", userId);
            parameters.Add("followerId", followerId);

            var query = "UPDATE Follows " +
                "SET IsDeleted = 1 " +
                "WHERE FollowingId = @userId AND FollowerId = @followerId";

            await _dapperContext.Connection.ExecuteAsync(query, parameters);
        }

        public async Task RemoveFollowing(int userId, int followingId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("userId", userId);
            parameters.Add("followingId", followingId);

            var query = "UPDATE Follows " +
                "SET IsDeleted = 1 " +
                "WHERE FollowerId = @userId AND FollowingId = @followingId";

            await _dapperContext.Connection.ExecuteAsync(query, parameters);
        }

        public async Task Follow(int userId, int id)
        {
            var createdAt = DateTime.UtcNow;

            var parameters = new DynamicParameters();
            parameters.Add("userId", userId);
            parameters.Add("id", id);
            parameters.Add("createdAt", createdAt);

            var query = "UPDATE Follows " +
                    "SET IsDeleted = 0 " +
                    "WHERE FollowerId = @userId AND FollowingId = @id";

            if (ValidateFollow(userId, id))
                query = "INSERT INTO Follows (FollowerId, FollowingId, CreatedAt) " +
                "VALUES (@userId, @id, @createdAt)";

            await _dapperContext.Connection.ExecuteAsync(query, parameters);
        }

        private bool ValidateFollow(int userId, int id)
        {
            var parameters = new DynamicParameters();
            parameters.Add("userId", userId);
            parameters.Add("id", id);

            var query = "SELECT COUNT(*) FROM Follows " +
                "WHERE FollowingId = @id AND FollowerId = @userId";

            var follow = _dapperContext.Connection.QueryFirstOrDefault<int>(query, parameters);

            if (follow == 0)
                return true;

            return false;
        }

    }
}
