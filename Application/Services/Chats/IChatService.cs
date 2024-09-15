using Application.Dtos;
using Domain.Entities;

namespace Application.Services.Chats
{
    public interface IChatService
    {
        Task SendMessage(int senderId, int receiverId, string message);

        Task<IEnumerable<Message>> GetAllMessages(int userId, int id);

        Task DeleteMessage(int userId, int messageId);

        Task UpdateMessage(int userId, int messageId, string message);

        Task<UserDto?> SearchUserByName(int userId, string pattern);
    }
}
