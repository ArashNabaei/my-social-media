using Domain.Entities;
using Domain.Repositories;
using Microsoft.Extensions.Logging;
using Shared.Exceptions.Chats;

namespace Application.Services.Chats
{
    public class ChatService : IChatService
    {

        private readonly IChatRepository _chatRepository;

        private readonly ILogger<ChatService> _logger;

        public ChatService(IChatRepository chatRepository, ILogger<ChatService> logger)
        {
            _chatRepository = chatRepository;
            _logger = logger;
        }

        public async Task SendMessage(int senderId, int receiverId, string message)
        {
            await _chatRepository.SendMessage(senderId, receiverId, message);

            _logger.LogInformation($"User with Id {senderId} sent message to user with Id {receiverId}");
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
