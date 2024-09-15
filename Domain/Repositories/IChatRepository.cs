using Domain.Entities;

namespace Domain.Repositories
{
    public interface IChatRepository
    {
        Task SendMessage(int senderId, int receiverId,  string message);

        Task<IEnumerable<Message>?> GetAllMessages(int userId, int id);

        Task<Message?> GetMessagebyId(int userId, int messageId);

        Task DeleteMessage(int userId, int messageId);

        Task UpdateMessage(int userId, int messageId, string message);

        Task<User?> SearchUserByName(string pattern);
    }
}
