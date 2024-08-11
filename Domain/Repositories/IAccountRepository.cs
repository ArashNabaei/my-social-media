
namespace Domain.Repositories
{
    public interface IAccountRepository
    {
        Task<string> GenerateToekn();
    }
}
