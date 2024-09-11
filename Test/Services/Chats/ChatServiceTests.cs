using Application.Services.Chats;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.Extensions.Logging;
using Moq;
using Shared.Exceptions.Chats;
using Test.Mocks;
using Xunit;

namespace Test.Services.Chats
{
    public class ChatServiceTests
    {

        private readonly Mock<IChatRepository> _chatRepository;

        private readonly Mock<ILogger<ChatService>> _logger;

        private readonly IChatService _chatService;

        public ChatServiceTests()
        {
            _chatRepository = new Mock<IChatRepository> ();
            _logger = new Mock<ILogger<ChatService>> ();

            _chatService = new ChatService(
                _chatRepository.Object,
                _logger.Object);
        }

        [Fact]
        public async Task GetAllMessages_WhenMessagesExist_ShouldReturnsMessages()
        {
            int userId = 1;
            int chatId = 2;

            var messages = new List<Message>();
            messages.Add(ChatMocks.ValidMessage());

            _chatRepository.Setup(r => r.GetAllMessages(userId, chatId))
                           .ReturnsAsync(messages);

            var result = await _chatService.GetAllMessages(userId, chatId);

            Assert.Equal(messages, result);
        }

        [Fact]
        public async Task GetAllMessages_WhenNoMessagesExist_ShouldThrowsNoMessagesFoundException()
        {
            int userId = 1;
            int chatId = 2;

            _chatRepository.Setup(r => r.GetAllMessages(userId, chatId))
                           .ReturnsAsync((IEnumerable<Message>?)null);

            var exception = await Assert.ThrowsAsync<ChatException>(() => _chatService.GetAllMessages(userId, chatId));

            Assert.Equal(4002, exception.Code);
        }

        [Fact]
        public async Task SendMessage_ShouldSendMessageSent()
        {
            var message = ChatMocks.ValidMessage();

            await _chatService.SendMessage(message.SenderId, message.ReceiverId, message.Content);

            _chatRepository.Verify(r => r.SendMessage(message.SenderId, message.ReceiverId, message.Content), Times.Once);
        }

        [Fact]
        public async Task DeleteMessage_WhenMessageExists_ShouldDeletesMessageSuccessfully()
        {
            var message = ChatMocks.ValidMessage();
            int messageId = message.Id;
            int userId = message.SenderId;

            _chatRepository.Setup(r => r.GetMessagebyId(userId, messageId))
                           .ReturnsAsync(message);

            await _chatService.DeleteMessage(userId, messageId);

            _chatRepository.Verify(r => r.DeleteMessage(userId, messageId), Times.Once);
        }

        [Fact]
        public async Task DeleteMessage_WhenMessageDoesNotExist_ShouldThrowsMessageNotFoundException()
        {
            int userId = 1;
            int messageId = 1;

            _chatRepository.Setup(r => r.GetMessagebyId(userId, messageId))
                           .ReturnsAsync((Message?)null);

            var exception = await Assert.ThrowsAsync<ChatException>(() => _chatService.DeleteMessage(userId, messageId));

            Assert.Equal(4001, exception.Code);
        }

        [Fact]
        public async Task UpdateMessage_WhenMessageExists_ShouldUpdatesMessageSuccessfully()
        {
            var message = ChatMocks.ValidMessage();
            int messageId = message.Id;
            int userId = message.SenderId;

            _chatRepository.Setup(r => r.GetMessagebyId(userId, messageId))
                           .ReturnsAsync(message);

            await _chatService.UpdateMessage(userId, messageId, "updated message");

            _chatRepository.Verify(r => r.UpdateMessage(userId, messageId, "updated message"), Times.Once);
        }

        [Fact]
        public async Task UpdateMessage_WhenMessageDoesNotExist_ShouldThrowsMessageNotFoundException()
        {
            int userId = 1;
            int messageId = 1;

            _chatRepository.Setup(r => r.GetMessagebyId(userId, messageId))
                           .ReturnsAsync((Message?)null);

            var exception = await Assert.ThrowsAsync<ChatException>(() => _chatService.UpdateMessage(userId, messageId, "updated message"));

            Assert.Equal(4001, exception.Code);
        }

    }
}
