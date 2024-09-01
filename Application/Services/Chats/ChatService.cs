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

            _logger.LogInformation($"User with Id {senderId} sent message to user with Id {receiverId}.");
        }

        public async Task<IEnumerable<Message>> GetAllMessages(int userId, int id)
        {
            var messages = await _chatRepository.GetAllMessages(userId, id);

            _logger.LogInformation($"User with id {userId} saw all messages of chat with user with id {id}.");

            return messages;
        }

        public async Task DeleteMessage(int userId, int messageId)
        {
            var message = await _chatRepository.GetMessagebyId(userId, messageId);

            if (message == null)
            {
                _logger.LogError($"User with id {userId} tried to delete a non-existent message with id {messageId}.");

                throw ChatException.MessageNotFound();
            }

            await _chatRepository.DeleteMessage(userId, messageId);

            _logger.LogInformation($"User with id {userId} deleted message with id {messageId}.");
        }

        public async Task UpdateMessage(int userId, int messageId, string message)
        {
            var foundedMessage = await _chatRepository.GetMessagebyId(userId, messageId);

            if (foundedMessage == null)
            {
                _logger.LogError($"User with id {userId} tried to update a non-existent message with id {messageId}.");

                throw ChatException.MessageNotFound();
            }

            await _chatRepository.UpdateMessage(userId, messageId, message);

            _logger.LogInformation($"User with id {userId} updated message with id {messageId}.");
        }

    }
}
