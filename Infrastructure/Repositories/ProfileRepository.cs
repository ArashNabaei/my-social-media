using Dapper;
using Domain.Entities;
using Domain.Repositories;

namespace Infrastructure.Repositories
{
    public class ProfileRepository : IProfileRepository
    {

        private readonly DapperContext _dapperContext;

        public ProfileRepository(DapperContext dapperContext)
        {
            _dapperContext = dapperContext;
        }

        public async Task<User> GetProfile(int id)
        {
            var parameters = new DynamicParameters();
            parameters.Add("id", id);

            var query = "SELECT * FROM Users WHERE Id = @id";

            var user = await _dapperContext.Connection.QueryFirstAsync<User>(query, parameters);

            return user;
        }

        public async Task UpdateProfile(int id, User user)
        {
            var parameters = new DynamicParameters();
            parameters.Add("id", id);
            parameters.Add("firstName", user.FirstName);
            parameters.Add("lastName", user.LastName);
            parameters.Add("bio", user.Bio);
            parameters.Add("email", user.Email);
            parameters.Add("imageUrl", user.ImageUrl);
            parameters.Add("phoneNumber", user.PhoneNumber);
            parameters.Add("dateOfBirth", user.DateOfBirth);

            var query = "UPDATE Users SET FirstName = @firstName, " +
                "LastName = @lastName, Bio = @bio, " +
                "Email = @email, ImageUrl = @imageUrl, " +
                "PhoneNumber = @phoneNumber, DateOfBirth = @dateOfBirth " +
                "WHERE Id = @id";

            await _dapperContext.Connection.ExecuteAsync(query, parameters);
        }

    }
}
