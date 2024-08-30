using Domain.Entities;
using Domain.Repositories;

namespace Application.Services.Chats
{
    public class ChatService : IChatService
    {

        private readonly IChatRepository _chatRepository;

        public ChatService(IChatRepository chatRepository)
        {
            _chatRepository = chatRepository;
        }

        public async Task SendMessage(int senderId, int receiverId, string message)
        {
            await _chatRepository.SendMessage(senderId, receiverId, message);
        }

        public async Task<IEnumerable<Message>> GetAllMessages(int userId, int id)
        {
            var messages = await _chatRepository.GetAllMessages(userId, id);

            return messages;
        }

    }
}
