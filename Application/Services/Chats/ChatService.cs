using Application.Dtos;
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

            if (messages == null)
            {
                _logger.LogError($"User with id {userId} tried to access a chat with user with id {id}, but no messages were found.");

                throw ChatException.NoMessagesFound();
            }

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

        public async Task<IEnumerable<UserDto?>> SearchUserByName(int userId, string pattern)
        {
            var users = await _chatRepository.SearchUserByName(userId, pattern);

            if (users == null)
            {
                _logger.LogError($"User with id {userId} tried to access non-existent user with name like '{pattern}'.");

                throw ChatException.UserNotFound();
            }

            var result = ConvertUserToUserDto(users);

            _logger.LogInformation($"User with id {userId} searched users with name like '{pattern}'.");

            return result;
        }

        private static IEnumerable<UserDto> ConvertUserToUserDto(IEnumerable<User?> users)
        {
            return users.Select(u => new UserDto
            {
                Id = u.Id,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Bio = u.Bio,
                ImageUrl = u.ImageUrl,
            });
        }

    }
}
