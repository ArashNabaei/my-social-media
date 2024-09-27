using Application.Services.Chats;
using Domain.Entities;
using MediatR;

namespace Application.Features.Query.Chats
{
    public class GetAllMessagesQueryHandler : IRequestHandler<GetAllMessagesQuery, IEnumerable<Message>>
    {

        private readonly IChatService _chatService;

        public GetAllMessagesQueryHandler(IChatService chatService)
        {
            _chatService = chatService;
        }

        public async Task<IEnumerable<Message>> Handle(GetAllMessagesQuery request, CancellationToken cancellationToken)
        {
            var messages = await _chatService.GetAllMessages(request.UserId, request.ChatId);

            return messages;
        }

    }
}
