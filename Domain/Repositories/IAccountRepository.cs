using Domain.Entities;

namespace Domain.Repositories
{
    public interface IAccountRepository
    {
        Task CreateUser(string username, string password);

        Task<User?> GetUserByUsernameAndPassword(string username, string password);
    }
}
