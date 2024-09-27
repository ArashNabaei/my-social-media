using Application.Dtos;
using Application.Services.Chats;
using MediatR;

namespace Application.Features.Query.Chats
{
    public class SearchUserByNameQueryHandler : IRequestHandler<SearchUserByNameQuery, IEnumerable<UserDto>>
    {
        
        private readonly IChatService _chatService;

        public SearchUserByNameQueryHandler(IChatService chatService)
        {
            _chatService = chatService;
        }

        public async Task<IEnumerable<UserDto>> Handle(SearchUserByNameQuery request, CancellationToken cancellationToken)
        {
            var users = await _chatService.SearchUserByName(request.UserId, request.Pattern);

            return users;
        }

    }
}
