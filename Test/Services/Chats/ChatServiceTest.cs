using Application.Services.Chats;
using Domain.Repositories;
using Microsoft.Extensions.Logging;
using Moq;

namespace Test.Services.Chats
{
    public class ChatServiceTest
    {

        private readonly Mock<IChatRepository> _chatRepository;

        private readonly Mock<ILogger<ChatService>> _logger;

        private readonly IChatService _chatService;

        public ChatServiceTest()
        {
            _chatRepository = new Mock<IChatRepository> ();
            _logger = new Mock<ILogger<ChatService>> ();

            _chatService = new ChatService(
                _chatRepository.Object,
                _logger.Object);
        }

    }
}
