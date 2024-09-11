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

    }
}
