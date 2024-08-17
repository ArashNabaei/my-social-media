using Domain.Entities;

namespace Domain.Repositories
{
    public interface IAccountRepository
    {
        Task<IEnumerable<User>> GetAllUsers();

        Task CreateUser(string username, string password);

        Task<User> GetUserByUsernameAndPassword(string username, string password);
    }
}
