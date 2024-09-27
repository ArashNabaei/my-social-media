using Domain.Entities;
using MediatR;

namespace Application.Features.Query.Chats
{
    public record GetAllMessagesQuery(int UserId, int ChatId) : IRequest<IEnumerable<Message>>
    {
    }
}
