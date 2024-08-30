using Domain.Entities;

namespace Domain.Repositories
{
    public interface IChatRepository
    {
        Task SendMessage(int senderId, int receiverId,  string message);

        Task<IEnumerable<Message>> GetAllMessages(int userId, int id);

        Task DeleteMessage(int userId, int messageId);

        Task UpdateMessage(int userId, int messageId, string message);
    }
}
