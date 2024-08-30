using Domain.Entities;
using Domain.Repositories;
using Shared.Exceptions.Chats;

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

        public async Task DeleteMessage(int userId, int messageId)
        {
            var message = await _chatRepository.GetMessagebyId(userId, messageId);

            if (message == null)
                throw ChatException.MessageNotFound();

            await _chatRepository.DeleteMessage(userId, messageId);
        }

        public async Task UpdateMessage(int userId, int messageId, string message)
        {
            var foundedMessage = await _chatRepository.GetMessagebyId(userId, messageId);

            if (foundedMessage == null)
                throw ChatException.MessageNotFound();

            await _chatRepository.UpdateMessage(userId, messageId, message);
        }

    }
}
