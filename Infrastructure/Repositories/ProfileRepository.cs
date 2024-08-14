using Dapper;
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

        public async Task<string> GetBio(int id)
        {
            var parameters = new DynamicParameters();
            parameters.Add("id", id);

            var query = "SELECT Bio FROM Users WHERE Id = @id";

            var bio = await _dapperContext.Connection.QueryFirstAsync(query, parameters);

            _dapperContext.Dispose();

            return bio;
        }

        public async Task<DateTime> GetDateOfBirth(int id)
        {
            var parameters = new DynamicParameters();
            parameters.Add("id", id);

            var query = "SELECT DateOfBirth FROM Users WHERE Id = @id";

            var dateOfBirth = await _dapperContext.Connection.QueryFirstAsync(query, parameters);

            _dapperContext.Dispose();

            return dateOfBirth;
        }

        public async Task<string> GetEmail(int id)
        {
            var parameters = new DynamicParameters();
            parameters.Add("id", id);

            var query = "SELECT Email FROM Users WHERE Id = @id";

            var email = await _dapperContext.Connection.QueryFirstAsync(query, parameters);

            _dapperContext.Dispose();

            return email;
        }

        public async Task<string> GetFirstName(int id)
        {
            var parameters = new DynamicParameters();
            parameters.Add("id", id);

            var query = "SELECT FirstName FROM Users WHERE Id = @id";

            var firstName = await _dapperContext.Connection.QueryFirstAsync(query, parameters);

            _dapperContext.Dispose();

            return firstName;
        }

        public async Task<string> GetImageUrl(int id)
        {
            var parameters = new DynamicParameters();
            parameters.Add("id", id);

            var query = "SELECT ImageUrl FROM Users WHERE Id = @id";

            var imageUrl = await _dapperContext.Connection.QueryFirstAsync(query, parameters);

            _dapperContext.Dispose();

            return imageUrl;
        }

        public async Task<string> GetLastName(int id)
        {
            var parameters = new DynamicParameters();
            parameters.Add("id", id);

            var query = "SELECT LastName FROM Users WHERE Id = @id";

            var lastName = await _dapperContext.Connection.QueryFirstAsync(query, parameters);

            _dapperContext.Dispose();

            return lastName;
        }

        public async Task<string> GetPhoneNumber(int id)
        {
            var parameters = new DynamicParameters();
            parameters.Add("id", id);

            var query = "SELECT PhoneNumber FROM Users WHERE Id = @id";

            var phoneNumber = await _dapperContext.Connection.QueryFirstAsync(query, parameters);

            _dapperContext.Dispose();

            return phoneNumber;
        }

        public async Task UpdateBio(int id, string bio)
        {
            var parameters = new DynamicParameters();
            parameters.Add("id", id);
            parameters.Add("bio", bio);

            var query = "Update Users SET Bio = @bio WHERE Id = @id";

            await _dapperContext.Connection.ExecuteAsync(query, parameters);

            _dapperContext.Dispose();
        }

        public async Task UpdateDateOfBirth(int id, DateTime dateOfBirth)
        {
            var parameters = new DynamicParameters();
            parameters.Add("id", id);
            parameters.Add("dateOfBirth", dateOfBirth);

            var query = "Update Users SET DateOfBirth = @dateOfBirth WHERE Id = @id";

            await _dapperContext.Connection.ExecuteAsync(query, parameters);

            _dapperContext.Dispose();
        }

        public async Task UpdateEmail(int id, string email)
        {
            var parameters = new DynamicParameters();
            parameters.Add("id", id);
            parameters.Add("email", email);

            var query = "Update Users SET Email = @email WHERE Id = @id";

            await _dapperContext.Connection.ExecuteAsync(query, parameters);

            _dapperContext.Dispose();
        }

        public async Task UpdateFirstName(int id, string firstName)
        {
            var parameters = new DynamicParameters();
            parameters.Add("id", id);
            parameters.Add("firstName", firstName);

            var query = "Update Users SET FirstName = @firstName WHERE Id = @id";

            await _dapperContext.Connection.ExecuteAsync(query, parameters);

            _dapperContext.Dispose();
        }

        public async Task UpdateImageUrl(int id, string imageUrl)
        {
            var parameters = new DynamicParameters();
            parameters.Add("id", id);
            parameters.Add("imageUrl", imageUrl);

            var query = "Update Users SET ImageUrl = @imageUrl WHERE Id = @id";

            await _dapperContext.Connection.ExecuteAsync(query, parameters);

            _dapperContext.Dispose();
        }

        public async Task UpdateLastName(int id, string lastName)
        {
            var parameters = new DynamicParameters();
            parameters.Add("id", id);
            parameters.Add("lastName", lastName);

            var query = "Update Users SET LastName = @lastName WHERE Id = @id";

            await _dapperContext.Connection.ExecuteAsync(query, parameters);

            _dapperContext.Dispose();
        }

        public async Task UpdatePhoneNumber(int id, string phoneNumber)
        {
            var parameters = new DynamicParameters();
            parameters.Add("id", id);
            parameters.Add("phoneNumber", phoneNumber);

            var query = "Update Users SET PhoneNumber = @phoneNumber WHERE Id = @id";

            await _dapperContext.Connection.ExecuteAsync(query, parameters);

            _dapperContext.Dispose();
        }
    }
}
