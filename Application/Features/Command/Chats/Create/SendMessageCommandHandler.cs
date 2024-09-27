using Application.Services.Chats;
using MediatR;

namespace Application.Features.Command.Chats.Create
{
    public class SendMessageCommandHandler : IRequestHandler<SendMessageCommand>
    {

        private readonly IChatService _chatService;

        public SendMessageCommandHandler(IChatService chatService)
        {
            _chatService = chatService;
        }

        public async Task Handle(SendMessageCommand request, CancellationToken cancellationToken)
        {
            await _chatService.SendMessage(request.SenderId, request.ReceiverId, request.Message);
        }

    }
}
